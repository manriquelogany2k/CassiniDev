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
using System.Threading;
using CassiniDev.Testing;
using NUnit.Framework;

namespace CassiniDev.FixtureExamples.NUnit.FixtureTests
{
    [TestFixture]
    public class FixtureTests
    {
        [Test]
        public void TestTimeOutWithNoRequest()
        {
            Fixture f = new Fixture();
            bool stopped = false;
            f.ServerStopped += ((sender, args) => { stopped = true; });
            Thread t = new Thread(() =>
            {
                f.StartServer(@"..\..\..\CassiniDev.FixtureExamples.TestWeb",
                              IPAddress.Loopback,
                              Fixture.GetPort(8080, 9000, IPAddress.Loopback), "/",
                              "localhost", false, 0, 5000);

                while (!stopped)
                {
                    Thread.Sleep(0);
                }
            });

            t.Start();
            t.Join(50000);
            Assert.IsTrue(stopped, "server stopped event not fired");
        }
        [Test]
        public void TestTimeOutWithRequest()
        {
            Fixture f = new Fixture();
            bool stopped = false;
            f.ServerStopped += ((sender, args) => { stopped = true; });
            Thread t = new Thread(() =>
                                      {
                                          f.StartServer(@"..\..\..\CassiniDev.FixtureExamples.TestWeb",
                                                        IPAddress.Loopback,
                                                        Fixture.GetPort(8080, 9000, IPAddress.Loopback), "/",
                                                        "localhost", false, 0, 5000);

                                          HttpRequestHelper.Get(new Uri(f.RootUrl), null, null);
                                          while (!stopped)
                                          {
                                              Thread.Sleep(0);
                                          }
                                      });

            t.Start();
            t.Join(50000);
            Assert.IsTrue(stopped, "server stopped event not fired");
        }

        [Test]
        public void TestEvents()
        {
            Fixture f = new Fixture();
            bool started = false;
            bool stopped = false;
            bool begin = false;
            bool complete = false;
            f.ServerStarted += ((sender, args) => { started = true; });
            f.RequestBegin += ((sender, args) => { begin = true; });
            f.RequestComplete += ((sender, args) => { complete = true; });
            f.ServerStopped += ((sender, args) => { stopped = true; });

            //Thread t = new Thread(() =>
            //                          {
            //                              f.StartServer(@"..\..\..\CassiniDev.FixtureExamples.TestWeb",
            //                                            IPAddress.Loopback,
            //                                            Fixture.GetPort(8080, 9000, IPAddress.Loopback), "/",
            //                                            "localhost", false, 0, 0);

            //                              HttpRequestHelper.Get(new Uri(f.RootUrl), null, null);
            //                              f.StopServer();
            //                              while (!stopped)
            //                              {
            //                                  Thread.Sleep(0);
            //                              }
            //                          });

            //t.Start();
            //t.Join(50000);

            f.StartServer(@"..\..\..\CassiniDev.FixtureExamples.TestWeb",
              IPAddress.Loopback,
              Fixture.GetPort(8080, 9000, IPAddress.Loopback), "/",
              "localhost", false, 0, 0);

            HttpRequestHelper.Get(new Uri(f.RootUrl), null, null);
            f.StopServer();

            Assert.IsTrue(started, "server started event not fired");

            Assert.IsTrue(begin, "request begin event not fired");
            Assert.IsTrue(complete, "request complete event not fired");
            Assert.IsTrue(stopped, "server stopped event not fired");
        }
    }
}