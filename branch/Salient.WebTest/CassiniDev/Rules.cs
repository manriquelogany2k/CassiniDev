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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace CassiniDev
{
    /// <summary>
    /// Business Rules Service - Needs to be broken into coherent classes
    /// </summary>
    public class Rules : IRules
    {
        private readonly string _executablePath = Assembly.GetAssembly(typeof (IServer)).Location;

        #region Hosts file

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public int RemoveHostEntry(string ipAddress, string hostname)
        {
            try
            {
                SetHostsEntry(false, ipAddress, hostname);
                return 0;
            }
            catch
            {
            }
            return StartElevated(_executablePath, string.Format("Hostsfile /ah- /h:{0} /i:{1}", hostname, ipAddress));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public int AddHostEntry(string ipAddress, string hostname)
        {
            try
            {
                SetHostsEntry(true, ipAddress, hostname);
                return 0;
            }
            catch
            {
            }
            return StartElevated(_executablePath, string.Format("Hostsfile /ah+ /h:{0} /i:{1}", hostname, ipAddress));
        }


        private void SetHostsEntry(bool addHost, string ipAddress, string hostname)
        {
            // limitation: while windows allows mulitple entries for a single host, we currently allow only one
            string windir = Environment.GetEnvironmentVariable("SystemRoot") ?? @"c:\windows";
            string hostsFilePath = Path.Combine(windir, @"system32\drivers\etc\hosts");
            string hostsFileContent = GetFileText(hostsFilePath);
            hostsFileContent = Regex.Replace(hostsFileContent,
                                             string.Format(@"\r\n^\s*[\d\w\.:]+\s{0}\s#\sadded\sby\scassini$",
                                                           hostname), "", RegexOptions.Multiline);

            if (addHost)
            {
                hostsFileContent += string.Format("\r\n{0} {1} # added by cassini", ipAddress, hostname);
            }
            SetFileText(hostsFilePath, hostsFileContent);
        }


        private static int StartElevated(string filename, string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = filename;
            startInfo.Arguments = args;
            startInfo.Verb = "runas";
            try
            {
                Process p = Process.Start(startInfo);
                if (p != null)
                {
                    p.WaitForExit();
                    return p.ExitCode;
                }
                return -2;
            }
            catch
            {
                return -2;
            }
        }

        // Testing Seam
        protected string GetFileText(string path)
        {
            return File.ReadAllText(path);
        }

        // Testing Seam
        protected void SetFileText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        #endregion

        #region Network

        /// <summary>
        /// Returns first available port on the specified IP address. The port scan excludes ports that are open on ANY loopback adapter. 
        /// If the address upon which a port is requested is an 'ANY' address all ports that are open on ANY IP are excluded.
        /// </summary>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <param name="ip">The IP address upon which to search for available port.</param>
        /// <param name="includeIdlePorts">If true includes ports in TIME_WAIT state in results. TIME_WAIT state is typically cool down period for recently released ports.</param>
        /// <returns></returns>
        public ushort GetAvailablePort(UInt16 rangeStart, UInt16 rangeEnd, IPAddress ip, bool includeIdlePorts)
        {
            IPGlobalProperties ipProps = IPGlobalProperties.GetIPGlobalProperties();

            // if the ip we want a port on is an 'any' or loopback port we need to exclude all ports that are active on any IP

            Func<IPAddress, bool> isIpAnyOrLoopBack = i => IPAddress.Any.Equals(i) ||
                                                           IPAddress.IPv6Any.Equals(i) ||
                                                           IPAddress.Loopback.Equals(i) ||
                                                           IPAddress.IPv6Loopback.
                                                               Equals(i);
            // get all active ports on specified IP. 

            List<ushort> excludedPorts = new List<ushort>();


            // if a port is open on an 'any' or 'loopback' interface then include it in the excludedPorts

            excludedPorts.AddRange(from n in ipProps.GetActiveTcpConnections()
                                   where
                                       n.LocalEndPoint.Port >= rangeStart
                                       && n.LocalEndPoint.Port <= rangeEnd
                                       &&
                                       (isIpAnyOrLoopBack(ip) || n.LocalEndPoint.Address.Equals(ip) ||
                                        isIpAnyOrLoopBack(n.LocalEndPoint.Address))
                                       && (!includeIdlePorts || n.State != TcpState.TimeWait)
                                   select (ushort) n.LocalEndPoint.Port);

            excludedPorts.AddRange(from n in ipProps.GetActiveTcpListeners()
                                   where n.Port >= rangeStart && n.Port <= rangeEnd
                                         &&
                                         (isIpAnyOrLoopBack(ip) || n.Address.Equals(ip) || isIpAnyOrLoopBack(n.Address))
                                   select (ushort) n.Port);

            excludedPorts.AddRange(from n in ipProps.GetActiveUdpListeners()
                                   where n.Port >= rangeStart && n.Port <= rangeEnd
                                         &&
                                         (isIpAnyOrLoopBack(ip) || n.Address.Equals(ip) || isIpAnyOrLoopBack(n.Address))
                                   select (ushort) n.Port);

            excludedPorts.Sort();


            for (ushort port = rangeStart; port <= rangeEnd; port++)
            {
                if (!excludedPorts.Contains(port))
                {
                    return port;
                }
            }

            return 0;
        }

        public IPAddress ParseIPString(string ipString)
        {
            if (string.IsNullOrEmpty(ipString))
            {
                ipString = "loopback";
            }
            ipString = ipString.Trim().ToLower();
            switch (ipString)
            {
                case "any":
                    return IPAddress.Any;
                case "loopback":
                    return IPAddress.Loopback;
                case "ipv6any":
                    return IPAddress.IPv6Any;
                case "ipv6loopback":
                    return IPAddress.IPv6Loopback;
                default:
                    IPAddress result;
                    IPAddress.TryParse(ipString, out result);
                    return result;
            }
        }

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
        public bool ValidateHostName(string hostname)
        {
            Regex hostnameRx =
                new Regex(
                    @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z]|[A-Za-z][A-Za-z0-9\-]*[A-Za-z0-9])$");
            return hostnameRx.IsMatch(hostname);
        }

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
        public IPAddress ParseIP(IPMode ipmode, bool v6, string ipString)
        {
            IPAddress ip;
            switch (ipmode)
            {
                case IPMode.Loopback:

                    ip = v6 ? IPAddress.IPv6Loopback : IPAddress.Loopback;
                    break;
                case IPMode.Any:
                    ip = v6 ? IPAddress.IPv6Any : IPAddress.Any;
                    break;
                case IPMode.Specific:

                    if (!IPAddress.TryParse(ipString, out ip))
                    {
                        throw new CassiniException("Invalid IP Address", ErrorField.IPAddress);
                    }
                    break;
                default:
                    throw new CassiniException("Invalid IPMode", ErrorField.None);
            }
            return ip;
        }

        #endregion

        #region Arguments

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
        public void ValidateArgs(CommandLineArguments args)
        {
            if (string.IsNullOrEmpty(args.VirtualPath) || !args.VirtualPath.StartsWith("/"))
            {
                throw new CassiniException("Invalid VPath", ErrorField.VirtualPath);
            }

            if (args.AddHost && (string.IsNullOrEmpty(args.HostName) || !ValidateHostName(args.HostName)))
            {
                throw new CassiniException("Invalid Hostname", ErrorField.HostName);
            }

            IPAddress ip = ParseIP(args.IPMode, args.IPv6, args.IPAddress);

            switch (args.PortMode)
            {
                case PortMode.FirstAvailable:

                    if (args.PortRangeStart < 1)
                    {
                        throw new CassiniException("Invalid port.", ErrorField.PortRangeStart);
                    }

                    if (args.PortRangeEnd < 1)
                    {
                        throw new CassiniException("Invalid port.", ErrorField.PortRangeEnd);
                    }

                    if (args.PortRangeStart > args.PortRangeEnd)
                    {
                        throw new CassiniException("Port range end must be equal or greater than port range start.",
                                                   ErrorField.PortRange);
                    }
                    if (GetAvailablePort(args.PortRangeStart, args.PortRangeEnd, ip, true) == 0)
                    {
                        throw new CassiniException("No available port found.", ErrorField.PortRange);
                    }
                    break;
                case PortMode.Specific:
                    // start waiting....
                    //TODO: design this hack away.... why am I waiting in a validation method?
                    int now = Environment.TickCount;

                    // wait until either 1) the specified port is available or 2) the specified amount of time has passed
                    while (Environment.TickCount < now + args.WaitForPort &&
                           GetAvailablePort(args.Port, args.Port, ip, true) != args.Port)
                    {
                        Thread.Sleep(100);
                    }

                    // is the port available?
                    if (GetAvailablePort(args.Port, args.Port, ip, true) != args.Port)
                    {
                        throw new CassiniException("Port is in use.", ErrorField.Port);
                    }
                    break;
                default:
                    throw new CassiniException("Invalid PortMode", ErrorField.None);
            }
        }

        #endregion
    }
}