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

namespace CassiniDev
{
    public class CassiniException : Exception
    {
        public CassiniException(string message, ErrorField field, Exception innerException)
            : base(message, innerException)
        {
            Field = field;
        }

        public CassiniException(string message, ErrorField field)
            : this(message, field, null)
        {
        }

        public ErrorField Field { get; set; }
    }
}