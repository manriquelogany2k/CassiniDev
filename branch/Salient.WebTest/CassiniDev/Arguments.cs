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
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using Cassini;
using Cassini.CommandLine;

namespace CassiniDev
{
    ///<summary>
    ///</summary>
    [Serializable]
    public class ServerEventArgs:EventArgs
    {
        ///<summary>
        ///</summary>
        ///<param name="rootUrl"></param>
        public ServerEventArgs(string rootUrl)
        {
            RootUrl = rootUrl;
        }
        ///<summary>
        ///</summary>
        public ServerEventArgs()
        {
        }
         
        ///<summary>
        ///</summary>
        public string RootUrl { get; set; }   
    }

    ///<summary>
    /// Don't want to marshal the request just for instrumentation
    /// 01/06/10 sky: added
    ///</summary>
    [Serializable]
    public class RequestInfo
    {
        ///<summary>
        ///</summary>
        public string Id = Guid.NewGuid().ToString("B");
        ///<summary>
        ///</summary>
        public string RemoteIP;

        ///<summary>
        ///</summary>
        public string LocalIP;

        ///<summary>
        ///</summary>
        public string RemoteName;


        ///<summary>
        ///</summary>
        public string Verb;
        ///<summary>
        ///</summary>
        public string Url;
        ///<summary>
        ///</summary>
        public string Protocol;

        ///<summary>
        ///</summary>
        public string Path;
        ///<summary>
        ///</summary>
        public string FilePath;
        ///<summary>
        ///</summary>
        public string PathInfo;
        ///<summary>
        ///</summary>
        public string PathTranslated;
        ///<summary>
        ///</summary>
        public string QueryString;
        ///<summary>
        ///</summary>
        public int ContentLength;
        ///<summary>
        ///</summary>
        public int ResponseStatus;

        ///<summary>
        ///</summary>
        public string UserAgent;

        ///<summary>
        ///</summary>
        ///<param name="encode"></param>
        ///<returns></returns>
        public string ToString(bool encode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("RequestInfo:");
            sb.AppendFormat("Id:{0},", encode ? HttpUtility.UrlEncode(Id) : Id);
            sb.AppendFormat("RemoteIP:{0},", encode ? HttpUtility.UrlEncode(RemoteIP) : RemoteIP);
            sb.AppendFormat("RemoteName:{0},", encode ? HttpUtility.UrlEncode(RemoteName) : RemoteName);
            sb.AppendFormat("LocalIP:{0},", encode ? HttpUtility.UrlEncode(LocalIP) : LocalIP);
            sb.AppendFormat("Verb:{0},", encode ? HttpUtility.UrlEncode(Verb) : Verb);
            sb.AppendFormat("Url:{0},", encode ? HttpUtility.UrlEncode(Url) : Url);
            sb.AppendFormat("Protocol:{0},", encode ? HttpUtility.UrlEncode(Protocol) : Protocol);
            sb.AppendFormat("Path:{0},", encode ? HttpUtility.UrlEncode(Path) : Path);
            sb.AppendFormat("FilePath:{0},", encode ? HttpUtility.UrlEncode(FilePath) : FilePath);
            sb.AppendFormat("PathInfo:{0},", encode ? HttpUtility.UrlEncode(PathInfo) : PathInfo);
            sb.AppendFormat("PathTranslated:{0},", encode ? HttpUtility.UrlEncode(PathTranslated) : PathTranslated);
            sb.AppendFormat("QueryString:{0},", encode ? HttpUtility.UrlEncode(QueryString) : QueryString);
            sb.AppendFormat("ContentLength:{0},", ContentLength);
            sb.AppendFormat("ResponseStatus:{0},", ResponseStatus);
            sb.AppendFormat("UserAgent:{0},", encode ? HttpUtility.UrlEncode(UserAgent) : UserAgent);
            //

            return sb.ToString();

        }

        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// TODO: typeconverter
        /// </summary>
        /// <param name="s"></param>
        public void ParseUrlEncoded(string s)
        {
            string tag = "RequestInfo:";
            if (!s.StartsWith(tag))
            {
                throw new ArgumentException("Invalid input argument", "s");
            }
            
            string line = s.Substring(tag.Length);

            string[] items = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
            {
                try
                {
                    string[] pair = items[i].Split(':');

                    switch (pair[0])
                    {
                        case "Id":
                            Id = HttpUtility.UrlDecode( pair[1]);
                            break;
                        case "RemoteIP":
                            RemoteIP = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "LocalIP":
                            LocalIP = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "RemoteName":
                            RemoteName = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "Verb":
                            Verb = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "Url":
                            Url = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "Protocol":
                            Protocol = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "Path":
                            Path = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "FilePath":
                            FilePath = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "PathInfo":
                            PathInfo = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "PathTranslated":
                            PathTranslated = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "QueryString":
                            QueryString = HttpUtility.UrlDecode(pair[1]);
                            break;
                        case "ContentLength":
                            ContentLength = Convert.ToInt32(pair[1]);
                            break;
                        case "ResponseStatus":
                            ResponseStatus = Convert.ToInt32(pair[1]);
                            break;
                        case "UserAgent":
                            UserAgent = HttpUtility.UrlDecode(pair[1]);
                            break;
                    }
                }
                catch { }
            }
        }
    }

    ///<summary>
    ///</summary>
    [Serializable]
    public class RequestEventArgs : EventArgs
    {
        private readonly RequestInfo _request;

        ///<summary>
        ///</summary>
        ///<param name="request"></param>
        public RequestEventArgs(RequestInfo request)
        {

            _request = request;
        }


        ///<summary>
        ///</summary>
        public RequestInfo Request
        {
            get { return _request; }
        }
    }

    /// <summary>
    /// Server Constructor arguments
    /// </summary>
    public class ServerArguments
    {
        public ushort Port { get; set; }
        public string VirtualPath { get; set; }
        public string ApplicationPath { get; set; }
        public IPAddress IPAddress { get; set; }
        public string Hostname { get; set; }
        public int TimeOut { get; set; }
    }


    /// <summary>
    /// Command line arguments
    /// 01/06/10 sky: added Quiet
    /// </summary>
    [DataContract]
    [Serializable]
    public class CommandLineArguments
    {
        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "ah", DefaultValue = false,
            HelpText = "If true add entry to Windows hosts file. Requires write permissions to hosts file.")]
        public bool AddHost;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "a", HelpText = "Physical location of content.")]
        public string ApplicationPath;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "h",
            HelpText = "Host name used for app root url. Optional unless AddHost is true.")]
        public string HostName;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "i",
            HelpText = "IP address to listen to. Ignored if IPMode != Specific")]
        public string IPAddress;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "im", DefaultValue = IPMode.Loopback, HelpText = "")]
        public IPMode IPMode;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "v6", DefaultValue = false,
            HelpText = "If IPMode 'Any' or 'LoopBack' are specified use the V6 address")]
        public bool IPv6;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "p",
            HelpText = "Port to listen to. Ignored if PortMode=FirstAvailable.")]
        public ushort Port;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "pm", DefaultValue = PortMode.FirstAvailable, HelpText = "")]
        public PortMode PortMode;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "pre", DefaultValue = (ushort)9000,
            HelpText = "End of port range. Ignored if PortMode != FirstAvailable")]
        public ushort PortRangeEnd = 9000;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "prs", DefaultValue = (ushort)8080,
            HelpText = "Start of port range. Ignored if PortMode != FirstAvailable")]
        public ushort PortRangeStart =8080;

        ///<summary>
        ///</summary>
        [DataMember]
        [DefaultArgument(ArgumentType.AtMostOnce, DefaultValue = RunMode.Server, HelpText = "[Server|Hostsfile]")]
        public RunMode RunMode;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "t", DefaultValue = 0,
            HelpText = "Length of time, in ms, to wait for a request before stopping the server. 0 = no timeout.")]
        public int TimeOut;

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "v", DefaultValue = "/", HelpText = "Optional. default value '/'")]
        public string VirtualPath = "/";

        ///<summary>
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "w", DefaultValue = 0,
            HelpText =
                "Length of time, in ms, to wait for a specific port before throwing an exception or exiting. 0 = don't wait."
            )]
        public int WaitForPort;

        ///<summary>
        /// 01/06/10 sky: added
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "q", DefaultValue = true,
    HelpText = "If false, echo request information to Console")]
        public bool Quiet;

        ///<summary>
        /// 01/06/10 sky: added
        ///</summary>
        [DataMember]
        [Argument(ArgumentType.AtMostOnce, ShortName = "headless", DefaultValue = false,
    HelpText = "Indicates that StandardIn has been redirected by test fixture")]
        public bool Headless;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (RunMode != RunMode.Server)
            {
                sb.AppendFormat("{0}", RunMode);
            }
            if (!string.IsNullOrEmpty(ApplicationPath))
            {
                sb.AppendFormat(" /a:{0}", ApplicationPath);
            }
            sb.AppendFormat(" /v:{0}", VirtualPath);

            if (!string.IsNullOrEmpty(HostName))
            {
                sb.AppendFormat(" /h:{0}", HostName);
            }
            if (AddHost)
            {
                sb.Append(" /ah+");
            }

            if (IPMode != IPMode.Loopback)
            {
                sb.AppendFormat(" /im:{0}", IPMode);
            }

            if (!string.IsNullOrEmpty(IPAddress))
            {
                sb.AppendFormat(" /i:{0}", IPAddress);
            }

            if (IPv6)
            {
                sb.Append(" /v6+");
            }

            if (PortMode != PortMode.FirstAvailable)
            {
                sb.AppendFormat(" /pm:{0}", PortMode);
            }

            if (Port != 0)
            {
                sb.AppendFormat(" /p:{0}", Port);
            }

            if (PortRangeStart != 8080)
            {
                sb.AppendFormat(" /prs:{0}", PortRangeStart);
            }
            if (PortRangeEnd != 9000)
            {
                sb.AppendFormat(" /pre:{0}", PortRangeEnd);
            }

            if (!Quiet)
            {
                sb.Append(" /q-");
            }

            if (Headless)
            {
                sb.Append(" /headless+");
            }

            if (TimeOut>0)
            {
                sb.Append(" /t:"+TimeOut);
            }

            return sb.ToString().Trim();
        }
    }
}