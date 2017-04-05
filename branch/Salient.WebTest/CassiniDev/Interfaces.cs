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
using System.Net;

namespace CassiniDev
{
    ///<summary>
    ///</summary>
    public interface IPresenter : IDisposable
    {
 
        ///<summary>
        ///</summary>
        ///<param name="view"></param>
        ///<param name="args"></param>
        void InitializeView(IView view, CommandLineArguments args);
        ///<summary>
        ///</summary>
        ///<param name="args"></param>
        void Start(CommandLineArguments args);
        ///<summary>
        ///</summary>
        void Stop();
        ///<summary>
        ///</summary>
        event EventHandler<ServerEventArgs> ServerStarted;
        ///<summary>
        ///</summary>
        event EventHandler<ServerEventArgs> ServerStopped;
        ///<summary>
        ///</summary>
        event EventHandler<RequestEventArgs> RequestBegin;
        ///<summary>
        ///</summary>
        event EventHandler<RequestEventArgs> RequestComplete;
    }

    ///<summary>
    ///</summary>
    public interface IServer : IDisposable
    {
        ///<summary>
        ///</summary>
        event EventHandler<ServerEventArgs> ServerStarted;
        ///<summary>
        ///</summary>
        event EventHandler<ServerEventArgs> ServerStopped;
        ///<summary>
        ///</summary>
        event EventHandler<RequestEventArgs> RequestBegin;
        ///<summary>
        ///</summary>
        event EventHandler<RequestEventArgs> RequestComplete;
        ///<summary>
        ///</summary>
        string HostName { get; }
        ///<summary>
        ///</summary>
        IPAddress IPAddress { get; }
        ///<summary>
        ///</summary>
        string VirtualPath { get; }
        ///<summary>
        ///</summary>
        string PhysicalPath { get; }
        ///<summary>
        ///</summary>
        int Port { get; }
        ///<summary>
        ///</summary>
        string RootUrl { get; }
        
        //event EventHandler Stopped;
        ///<summary>
        ///</summary>
        void Start();
        ///<summary>
        ///</summary>
        void Stop();
    }

    ///<summary>
    /// TODO: implement start and stop events on presenter and get rid of start/stop on IView as it breaks
    /// the pattern
    ///</summary>
    public interface IView
    {
        IPresenter Presenter { get; set; }
        int TimeOut { get; set; }
        int WaitForPort { get; set; }
        string ApplicationPath { get; set; }
        string VirtualPath { get; set; }
        string HostName { get; set; }
        string IPAddress { get; set; }
        bool IPv6 { get; set; }
        bool AddHost { get; set; }
        ushort Port { get; set; }
        ushort PortRangeStart { get; set; }
        ushort PortRangeEnd { get; set; }
        string RootUrl { get; set; }
        PortMode PortMode { get; set; }
        IPMode IPMode { get; set; }
        RunState RunState { get; set; }
        void ClearError();
        void SetError(ErrorField field, string value);
        //void Start();
        //void Stop();
    }


    public interface IRules
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        int RemoveHostEntry(string ipAddress, string hostname);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        int AddHostEntry(string ipAddress, string hostname);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="addHost"></param>
        ///// <param name="ipAddress"></param>
        ///// <param name="hostname"></param>
        //void SetHostsEntry(bool addHost, string ipAddress, string hostname);
        /// <summary>
        /// Returns first available port on the specified IP address. The port scan excludes ports that are open on ANY loopback adapter. 
        /// If the address upon which a port is requested is an 'ANY' address all ports that are open on ANY IP are excluded.
        /// </summary>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <param name="ip">The IP address upon which to search for available port.</param>
        /// <param name="includeIdlePorts">If true includes ports in TIME_WAIT state in results. TIME_WAIT state is typically cool down period for recently released ports.</param>
        /// <returns></returns>
        ushort GetAvailablePort(UInt16 rangeStart, UInt16 rangeEnd, IPAddress ip, bool includeIdlePorts);

        IPAddress ParseIPString(string ipString);

        /// <summary>
        /// <para>
        /// Hostnames are composed of series of labels concatenated with dots, as are all domain names[1]. 
        /// For example, "en.wikipedia.org" is a hostname. Each label must be between 1 and 63 characters long, 
        /// and the entire hostname has a maximum of 255 characters.</para>
        /// <para>
        /// The Internet standards (Request for Comments) for protocols mandate that component hostname 
        /// labels may contain only the ASCII letters 'a' through 'z' (in a case-insensitive manner), the digits 
        /// '0' through '9', and the hyphen. The original specification of hostnames in RFC 952, mandated that 
        /// labels could not start with a digit or with a hyphen, and must not end with a hyphen. However, a 
        /// subsequent specification (RFC 1123) permitted hostname labels to start with digits. No other symbols, 
        /// punctuation characters, or blank spaces are permitted.</para>
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns></returns>
        /// http://en.wikipedia.org/wiki/Hostname#Restrictions_on_valid_host_names
        bool ValidateHostName(string hostname);

        /// <summary>
        /// Converts CommandLineArgument values to an IP address if possible.
        /// Throws Exception if not.
        /// </summary>
        /// <param name="ipmode"></param>
        /// <param name="v6"></param>
        /// <param name="ipString"></param>
        /// <returns></returns>
        /// <exception cref="CassiniException">If IPMode is invalid</exception>
        /// <exception cref="CassiniException">If IPMode is 'Specific' and ipString is invalid</exception>
        IPAddress ParseIP(IPMode ipmode, bool v6, string ipString);

        /// <summary>
        /// Validates all but application path
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="CassiniException">If vpath is null or does not begin with '/'</exception>
        /// <exception cref="CassiniException">If an invalid hostname is specified</exception>
        /// <exception cref="CassiniException">If AddHost is true and a null or invalid hostname is specified</exception>
        /// <exception cref="CassiniException">If either port range is less than 1</exception>
        /// <exception cref="CassiniException">If PortRangeStart is greater than PortRangeEnd</exception>
        /// <exception cref="CassiniException">If no available port within specified range is found.</exception>
        /// <exception cref="CassiniException">If specified port is in use.</exception>
        /// <exception cref="CassiniException">If PortMode is invalid</exception>
        /// <exception cref="CassiniException">If PortMode is invalid</exception>
        /// <exception cref="CassiniException">If IPMode is 'Specific' and IPAddress is invalid</exception>
        void ValidateArgs(CommandLineArguments args);
    }
}