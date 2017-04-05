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
using System.Net;
using CassiniDev.Testing;
using NUnit.Framework;

namespace CassiniDev.FixtureExamples.NUnit
{
    [TestFixture]
    public class TestAppFixture : Fixture
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            StartServer(@"..\..\..\CassiniDev.FixtureExamples.TestWeb", IPAddress.Loopback, GetPort(8080, 9000, IPAddress.Loopback), "/", "localhost", false, 20000, 5000);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            StopServer();
        }
    }
}