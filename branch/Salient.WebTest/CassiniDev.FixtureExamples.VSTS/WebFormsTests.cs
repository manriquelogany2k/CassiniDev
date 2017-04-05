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
using CassiniDev.Testing;
using mshtml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CassiniDev.FixtureExamples.VSTS
{
    /// <summary>
    /// See AllTests.cs for commentary
    /// </summary>
    [TestClass]
    public class WebFormsTests : TestAppFixture
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
        public void GetWebFormWithHttpRequestHelper()
        {
            Uri requestUri = NormalizeUri("default.aspx");
            string responseText = HttpRequestHelper.Get(requestUri, null, null);
            Assert.IsTrue(responseText.IndexOf("This is the default document of the TestWebApp") > 0);
        }

        [TestMethod]
        public void GetWebFormWithHttpRequestHelperAndParseWithMSHTML()
        {
            Uri requestUri = NormalizeUri("default.aspx");

            string responseText = HttpRequestHelper.Get(requestUri, null, null);
            Assert.IsTrue(responseText.IndexOf("This is the default document of the TestWebApp") > 0);
            using (HttpRequestHelper browser = new HttpRequestHelper())
            {
                // parse the html in internet explorer
                IHTMLDocument2 doc = browser.ParseHtml(responseText);
                // get the resultant html
                string content = doc.body.outerHTML;
                Assert.IsTrue(content.IndexOf("This is the default document of the TestWebApp") > 0);
                IHTMLElement form = browser.FindElementByID("form1");
                Assert.IsNotNull(form);
            }
        }


        [TestMethod]
        public void GetWebFormWithHttpWebRequest()
        {
            Uri requestUri = NormalizeUri("default.aspx");
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(requestUri);
            string responseText;
            using (WebResponse response = req.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                responseText = reader.ReadToEnd();
                responseStream.Close();
                response.Close();
            }
            Assert.IsTrue(responseText.IndexOf("This is the default document of the TestWebApp") > 0);
        }
    }
}