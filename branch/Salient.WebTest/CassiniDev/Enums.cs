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
namespace CassiniDev
{
    public enum RunState
    {
        Idle = 0,
        Running
    }

    public enum PortMode
    {
        FirstAvailable = 0,
        Specific
    }

    public enum ErrorField
    {
        None,
        ApplicationPath,
        VirtualPath,
        HostName,
        IsAddHost,
        IPAddress,
        IPAddressAny,
        IPAddressLoopBack,
        Port,
        PortRangeStart,
        PortRangeEnd,
        PortRange
    }

    public enum IPMode
    {
        Loopback = 0,
        Any,
        Specific
    }

    public enum RunMode
    {
        Server,
        Hostsfile
    }
}