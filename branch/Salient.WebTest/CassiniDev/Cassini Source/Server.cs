/* **********************************************************************************
 *
 * Copyright (c) Microsoft Corporation. All rights reserved.
 *
 * This source code is subject to terms and conditions of the Microsoft Public
 * License (Ms-PL). A copy of the license can be found in the license.htm file
 * included in this distribution.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * **********************************************************************************/

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using CassiniDev;

namespace Cassini
{
    /// <summary>
    /// 12/29/09 sky: changed visibility to public
    /// 12/29/09 sky: modified CreateSocketBindAndListen: added socket option ReuseAddress to prevent
    ///               false port conflicts with freshly released ports in TIME_WAIT state.
    ///               In a testing scenario we may be creating and disposing many servers in rapid succession.
    ///               Default socket behaviour will send the closed socket into the TIME_WAIT state, a sort of 
    ///               cooldown period, making it unavaible. By setting ReuseAddress we are telling winsock to 
    ///               accept the binding event if the socket is in TIME_WAIT. 
    /// 12/29/09 sky: modified Start: replaced hard coded reference to loopback with instance field
    ///               _ipAddress and removed the try/catch implicit rollover to ipv6.
    /// 12/29/09 sky: modified RootUrl: added support for hostname and arbitrary IP address
    /// 01/01/10 sky: added support for relative paths to constructor
    /// 
    /// </summary>
    public partial class Server : MarshalByRefObject
    {
        readonly int _port;
        readonly string _virtualPath;
        readonly string _physicalPath;
        bool _shutdownInProgress;
        Socket _socket;
        Host _host;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <param name="virtualPath"></param>
        /// <param name="physicalPath"></param>
        public Server(int port, string virtualPath, string physicalPath)
        {
            _port = port;
            _virtualPath = virtualPath;
            _physicalPath = Path.GetFullPath(physicalPath);
            _physicalPath = _physicalPath.EndsWith("\\", StringComparison.Ordinal) ? _physicalPath : _physicalPath + "\\";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            // never expire the license
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public string VirtualPath
        {
            get
            {
                return _virtualPath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PhysicalPath
        {
            get
            {
                return _physicalPath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Port
        {
            get
            {
                return _port;
            }
        }

        /// <summary>
        /// sky: modified to allow hostname and arbitrary IP
        /// </summary>
        public string RootUrl
        {
            get
            {
                string hostname = _hostName;
                if (string.IsNullOrEmpty(_hostName))
                {
                    if (_ipAddress.Equals(IPAddress.Loopback) || _ipAddress.Equals(IPAddress.IPv6Loopback) ||
                        _ipAddress.Equals(IPAddress.Any) || _ipAddress.Equals(IPAddress.IPv6Any))
                    {
                        hostname = "localhost";
                    }
                    else
                    {
                        hostname = _ipAddress.ToString();
                    }
                }

                return _port != 80 ?
                    String.Format("http://{0}:{1}{2}", hostname, _port, _virtualPath) :
                    string.Format("http://{0}.{1}", hostname, _virtualPath);

            }
        }


        /// <summary>
        /// Socket listening 
        /// </summary>
        /// <param name="family"></param>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private static Socket CreateSocketBindAndListen(AddressFamily family, IPAddress address, int port)
        {
            var socket = new Socket(family, SocketType.Stream, ProtocolType.Tcp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(address, port));
            socket.Listen((int)SocketOptionName.MaxConnections);
            return socket;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            _socket = CreateSocketBindAndListen(AddressFamily.InterNetwork, _ipAddress, _port);
            //start the timer
            StartTimer();
            

            ThreadPool.QueueUserWorkItem(delegate
            {
                while (!_shutdownInProgress)
                {
                    try
                    {
                        Socket acceptedSocket = _socket.Accept();

                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            if (!_shutdownInProgress)
                            {
                                var conn = new Connection(this, acceptedSocket);

                                // wait for at least some input
                                if (conn.WaitForRequestBytes() == 0)
                                {
                                    conn.WriteErrorAndClose(400);
                                    return;
                                }

                                // find or create host
                                Host host = GetHost();
                                if (host == null)
                                {
                                    conn.WriteErrorAndClose(500);
                                    return;
                                }

                                IncrementRequestCount();
                                
                                // process request in worker app domain
                                host.ProcessRequest(conn);

                                DecrementRequestCount();
                            }
                        });
                    }
                    catch
                    {
                        Thread.Sleep(100);
                    }
                }

            });

            OnServerStarted(new ServerEventArgs(RootUrl));
        }

        
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            _shutdownInProgress = true;
            OnServerStopped(new ServerEventArgs(RootUrl));

            try
            {
                if (_socket != null)
                {
                    _socket.Close();
                }
            }
            catch
            {
            }
            finally
            {
                _socket = null;
            }

            try
            {
                if (_host != null)
                {
                    _host.Shutdown();
                }

                while (_host != null)
                {
                    Thread.Sleep(100);
                }
            }
            catch
            {
            }
            finally
            {
                _host = null;
            }

            
            
        }

        /// <summary>
        /// Called at the end of request processing to disconnect the remoting proxy for Connection object
        /// and allow GC to pick it up.
        /// 
        /// 01/06/10 sky: modified signature to enable instrumentation
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="info"></param>
        public virtual void OnRequestEnd(Connection conn, RequestInfo info)
        {
            OnRequestComplete(new RequestEventArgs(info));

            RemotingServices.Disconnect(conn);
         
        }

        /// <summary>
        /// 
        /// </summary>
        public void HostStopped()
        {
            _host = null;
        }

        Host GetHost()
        {
            if (_shutdownInProgress)
                return null;

            Host host = _host;

            if (host == null)
            {
                lock (this)
                {
                    host = _host;
                    if (host == null)
                    {
                        host = (Host)CreateWorkerAppDomainWithHost(_virtualPath, _physicalPath, typeof(Host));
                        host.Configure(this, _port, _virtualPath, _physicalPath);
                        _host = host;
                    }
                }
            }

            return host;
        }

        static object CreateWorkerAppDomainWithHost(string virtualPath, string physicalPath, Type hostType)
        {
            // this creates worker app domain in a way that host doesn't need to be in GAC or bin
            // using BuildManagerHost via private reflection
            string uniqueAppString = string.Concat(virtualPath, physicalPath).ToLowerInvariant();
            string appId = (uniqueAppString.GetHashCode()).ToString("x", CultureInfo.InvariantCulture);

            // create BuildManagerHost in the worker app domain
            var appManager = ApplicationManager.GetApplicationManager();
            var buildManagerHostType = typeof(HttpRuntime).Assembly.GetType("System.Web.Compilation.BuildManagerHost");
            var buildManagerHost = appManager.CreateObject(appId, buildManagerHostType, virtualPath, physicalPath, false);

            // call BuildManagerHost.RegisterAssembly to make Host type loadable in the worker app domain
            buildManagerHostType.InvokeMember(
                "RegisterAssembly",
                BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
                null,
                buildManagerHost,
                new object[] { hostType.Assembly.FullName, hostType.Assembly.Location });

            // create Host in the worker app domain
            return appManager.CreateObject(appId, hostType, virtualPath, physicalPath, false);
        }
    }
}
