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
using NUnit.Framework;

namespace CassiniDev.FixtureExamples.NUnit
{
    /// <summary>
    /// See AllTests.cs for commentary
    /// </summary>
    [TestFixture, Category("Discrete")]
    public class WcfAjaxTests : TestAppFixture
    {
        [Test]
        public void PostJsonGetJsonWithHttpRequestHelperWcfHelloWorld()
        {
            Uri uri = NormalizeUri("TestAjaxService.svc/HelloWorld");
            string json = HttpRequestHelper.AjaxTxt(uri, null, null);
            Assert.AreEqual("{\"d\":\"Hello World\"}", json);
        }

        [Test]
        public void PostJsonGetJsonWithHttpRequestHelperWcfHelloWorldWithArgsInOut()
        {
            Uri uri = NormalizeUri("TestAjaxService.svc/HelloWorldWithArgsInOut");
            Dictionary<string, object> postData = new Dictionary<string, object>();

            // if you have a reference to the type expected you can serialize an instance
            // or just simply create an anonymous type that is shaped like the type expected...
            postData.Add("args", new {Message = "HI!"});

            string json = HttpRequestHelper.AjaxTxt(uri, postData, null);

            Assert.AreEqual("{\"d\":{\"__type\":\"HelloWorldArgs:#CassiniDev.FixtureExamples.TestWeb\",\"Message\":\"you said: HI!\"}}", json);
        }
    }
}