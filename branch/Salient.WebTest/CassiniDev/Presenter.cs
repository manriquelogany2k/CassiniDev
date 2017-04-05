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
using System.IO;
using System.Net;

namespace CassiniDev
{
    ///<summary>
    ///</summary>
    public class Presenter : IPresenter
    {
        private CommandLineArguments _args;
        private bool _disposed;
        private IServer _server;
        //TODO: eliminate view reference - use events
        private IView _view;

        #region IPresenter Members

        ///<summary>
        ///</summary>
        public event EventHandler<RequestEventArgs> RequestComplete;

        ///<summary>
        ///</summary>
        public event EventHandler<RequestEventArgs> RequestBegin;

        ///<summary>
        ///</summary>
        public event EventHandler<ServerEventArgs> ServerStarted;

        ///<summary>
        ///</summary>
        public event EventHandler<ServerEventArgs> ServerStopped;

        public void InitializeView(IView view, CommandLineArguments args)
        {
            _view = view;
            _view.Presenter = this;
            _view.RunState = RunState.Idle;

            _view.AddHost = args.AddHost;
            if (!string.IsNullOrEmpty(args.ApplicationPath))
            {
                args.ApplicationPath = args.ApplicationPath.Trim('\"').TrimEnd('\\');
            }

            _view.ApplicationPath = args.ApplicationPath;
            if (!string.IsNullOrEmpty(args.VirtualPath))
            {
                args.VirtualPath = args.VirtualPath.Trim('\"');
            }
            _view.VirtualPath = args.VirtualPath;
            _view.HostName = args.HostName;
            _view.IPAddress = args.IPAddress;
            _view.IPMode = args.IPMode;
            _view.IPv6 = args.IPv6;
            _view.Port = args.Port;
            _view.PortMode = args.PortMode;
            _view.PortRangeEnd = args.PortRangeEnd;
            _view.PortRangeStart = args.PortRangeStart;
            _view.RootUrl = string.Empty;
            _view.WaitForPort = args.WaitForPort;
            _view.TimeOut = args.TimeOut;

            try
            {
                ServiceFactory.Rules.ValidateArgs(args);
                // if an app path was passed, user wanted to start server
                if (!string.IsNullOrEmpty(args.ApplicationPath))
                {
                    Start(args);
                }
            }
            catch (CassiniException ex)
            {
                _view.SetError(ex.Field, ex.Message);
            }
        }

        public void Start(CommandLineArguments args)
        {
            _args = null;
            if (_view != null)
            {
                _view.ClearError();
            }

            try
            {
                ServiceFactory.Rules.ValidateArgs(args);
            }
            catch (CassiniException ex)
            {
                if (_view != null)
                {
                    _view.SetError(ex.Field, ex.Message);
                    return;
                }
                throw;
            }

            if (string.IsNullOrEmpty(args.ApplicationPath) || !Directory.Exists(args.ApplicationPath))
            {
                if (_view != null)
                {
                    _view.SetError(ErrorField.ApplicationPath, "Invalid Application Path");
                    return;
                }
                throw new CassiniException("Invalid Application Path", ErrorField.ApplicationPath);
            }

            // prepare arguments
            IPAddress ip = ServiceFactory.Rules.ParseIP(args.IPMode, args.IPv6, args.IPAddress);

            if (_view != null)
            {
                _view.IPAddress = ip.ToString();
            }

            ushort port = args.Port;
            if (args.PortMode == PortMode.FirstAvailable)
            {
                port = ServiceFactory.Rules.GetAvailablePort(args.PortRangeStart, args.PortRangeEnd, ip, true);
            }

            if (_view != null)
            {
                _view.Port = port;
            }


            if (_view != null)
            {
                _view.HostName = args.HostName;
            }

            _server = ServiceFactory.CreateServer(new ServerArguments
                                                      {
                                                          Port = port,
                                                          VirtualPath = args.VirtualPath,
                                                          ApplicationPath = args.ApplicationPath,
                                                          IPAddress = ip,
                                                          Hostname = args.HostName,
                                                          TimeOut = args.TimeOut
                                                      });

            WireServerEvents();

            if (args.AddHost)
            {
                ServiceFactory.Rules.AddHostEntry(_server.IPAddress.ToString(), _server.HostName);
            }

            try
            {
                
                _server.Start();
                _args = args;

                if (_view != null)
                {
                    _view.RootUrl = _server.RootUrl;
                    _view.RunState = RunState.Running;
                }
            }
            catch (Exception ex)
            {
                if (_view != null)
                {
                    _view.SetError(ErrorField.None, ex.Message);
                }

                Stop();
            }
        }

        public void Stop()
        {
            if (_view != null)
            {
                //TODO: set view to listen for events - remove this 
                _view.RunState = RunState.Idle;
            }

            if (_args.AddHost)
            {
                ServiceFactory.Rules.RemoveHostEntry(_server.IPAddress.ToString(), _server.HostName);
            }


            if (_server != null)
            {
                _server.Stop();
                UnWireServerEvents();
                _server = null;
            }
            
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_server != null)
                {
                    _server.Stop();
                }
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Event invocation

        private void WireServerEvents()
        {
            _server.ServerStarted += OnServerStarted;
            _server.ServerStopped += OnServerStopped;
            _server.RequestBegin += OnRequestBegin;
            _server.RequestComplete += OnRequestComplete;
        }

        private void UnWireServerEvents()
        {
            _server.ServerStarted -= OnServerStarted;
            _server.RequestBegin -= OnRequestBegin;
            _server.RequestComplete -= OnRequestComplete;
        }

        protected virtual void OnServerStarted(ServerEventArgs e)
        {
            EventHandler<ServerEventArgs> handler = ServerStarted;
            if (handler != null) handler(null, e);
        }
        
        protected virtual void OnServerStopped(ServerEventArgs e)
        {
            EventHandler<ServerEventArgs> handler = ServerStopped;
            if (handler != null)
            {
                handler(null, e);
            }
        }
        
        protected virtual void OnRequestComplete(RequestEventArgs e)
        {
            EventHandler<RequestEventArgs> complete = RequestComplete;
            if (complete != null) complete(this, e);
        }

        protected virtual void OnRequestBegin(RequestEventArgs e)
        {
            EventHandler<RequestEventArgs> handler = RequestBegin;
            if (handler != null) handler(this, e);
        }
        #region Server event handlers

        private void OnServerStarted(object sender, ServerEventArgs e)
        {
            OnServerStarted(e);
        }

        private void OnServerStopped(object sender, ServerEventArgs e)
        {
            OnServerStopped(e);
        }

        private void OnRequestComplete(object sender, RequestEventArgs e)
        {
            OnRequestComplete(e);
        }

        private void OnRequestBegin(object sender, RequestEventArgs e)
        {
            OnRequestBegin(e);
        }

        #endregion
        #endregion



    }
}