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
using CassiniDev.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CassiniDev.FixtureExamples.VSTS
{
    /// <summary>
    /// See AllTests.cs for commentary
    /// </summary>
    [TestClass]
    public class XmlWebServicesTests : TestAppFixture
    {
        /// <summary>
        /// ClassInitialize inheritance does not work in MSTest. another FAIL
        /// </summary>
        /// <param name="ignored"></param>
        [ClassInitialize]
        public static void Initialize(TestContext ignored)
        {
            InitializeFixture();
        }
        [ClassCleanup]
        public static void Cleanup()
        {
            CleanupFixture();
        }

        [TestMethod]
        public void XmlWebServicePostFormGetXmlHelloWorld()
        {
            Uri uri = NormalizeUri("TestWebService.asmx/HelloWorld");
            string xml = HttpRequestHelper.Post(uri, null, null);
            //
            Assert.IsTrue(xml.IndexOf("<string xmlns=\"http://tempuri.org/\">Hello World</string>") > -1);
        }

        [TestMethod]
        public void XmlWebServicePostJsonGetJsonHelloWorld()
        {
            Uri uri = NormalizeUri("TestWebService.asmx/HelloWorld");
            string json = HttpRequestHelper.AjaxApp(uri, null, null);
            Assert.AreEqual("{\"d\":\"Hello World\"}", json);
        }

        [TestMethod]
        public void XmlWebServicePostJsonGetJsonHelloWorldWithArgsInOut()
        {
            Uri uri = NormalizeUri("TestWebService.asmx/HelloWorldWithArgsInOut");
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("args", new {Message = "HI!"});
            string json = HttpRequestHelper.AjaxApp(uri, postData, null);
            Assert.AreEqual("{\"d\":{\"__type\":\"CassiniDev.FixtureExamples.TestWeb.HelloWorldArgs\",\"Message\":\"you said: HI!\"}}", json);
        }
    }
}