// /* **********************************************************************************
//  *
//  * Copyright (c) Sky Sanders. All rights reserved.
//  * 
//  * This source code is subject to terms and conditions of the Microsoft Public
//  * License (Ms-PL). A copy of the license can be found in the license.htm file
//  * included in this distribution.
//  *
//  * You must not remove this notice, or any other, from this software.
//  *
//  * **********************************************************************************/
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using CassiniDev;

namespace Cassini
{
    /// <summary>
    /// 
    /// Please put all but the most trivial changes and all additions to Server in partial files to
    /// reduce the code churn and pain of merging new releases of Cassini. If a method is to be significantly modified,
    /// comment it out, explain the modification/move in the header and put the modified version in this file.
    /// 
    /// 12/29/09 sky: Implemented IDisposable to help eliminate zombie ports
    /// 12/29/09 sky: Added instance properties for HostName and IPAddress and constructor to support them
    /// 12/29/09 sky: Extracted and implemented IServer interface to facilitate stubbing for tests
    /// 01/06/10 sky: added events
    /// </summary>
    public partial class Server : IServer
    {
        
        private readonly string _hostName;
        private readonly IPAddress _ipAddress;
        private int _requestCount;
        private readonly int _timeout;


        ///<summary>
        ///</summary>
        ///<param name="args"></param>
        public Server(ServerArguments args)
            : this(args.Port, args.VirtualPath, args.ApplicationPath)
        {
            _ipAddress = args.IPAddress;
            _hostName = args.Hostname;
            _timeout = args.TimeOut;
        }

        #region IServer Members

        ///<summary>
        ///</summary>
        public event EventHandler<RequestEventArgs> RequestComplete;

        ///<summary>
        ///</summary>
        public event EventHandler<ServerEventArgs> ServerStarted;

        ///<summary>
        ///</summary>
        public event EventHandler<ServerEventArgs> ServerStopped;

        ///<summary>
        ///</summary>
        public event EventHandler<RequestEventArgs> RequestBegin;

        public string HostName
        {
            get { return _hostName; }
        }

        public IPAddress IPAddress
        {
            get { return _ipAddress; }
        }

        #endregion

        #region Event invocation

        internal virtual void OnServerStarted(ServerEventArgs e)
        {
            EventHandler<ServerEventArgs> handler = ServerStarted;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        internal virtual void OnServerStopped(ServerEventArgs e)
        {
            EventHandler<ServerEventArgs> handler = ServerStopped;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        internal virtual void OnRequestBegin(RequestEventArgs e)
        {
            EventHandler<RequestEventArgs> handler = RequestBegin;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        internal virtual void OnRequestComplete(RequestEventArgs e)
        {
            EventHandler<RequestEventArgs> complete = RequestComplete;
            if (complete != null)
            {
                complete(null, e);
            }
        }

        #endregion

        #region IDisposable
        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                if (!_shutdownInProgress)
                {
                    Stop();
                    // just add a little slack for the socket transition to TIME_WAIT
                    Thread.Sleep(10);
                }
            }
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Idle shutdown implementation

        private Timer _timer;

        private void IncrementRequestCount()
        {
            _requestCount++;
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
            
        }

        private void TimedOut(object ignored)
        {
            if(_timer!=null)
            {
                _timer.Dispose();
                _timer = null;
                Stop();
            }
        }

        private void StartTimer()
        {
            if (_timeout > 0)
            {
                // start timer
                _timer = new Timer(TimedOut, null, _timeout, Timeout.Infinite);
            }
        }

        private void DecrementRequestCount()
        {
            _requestCount--;

            if (_requestCount < 1)
            {
                _requestCount = 0;

                StartTimer();

            }
        }

        #endregion
    }
}