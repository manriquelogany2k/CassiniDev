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
using System.IO;
using System.Net;
using CassiniDev.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mshtml;

namespace CassiniDev.FixtureExamples.VSTS
{

    /// <summary>
    /// All tests in one file spinning up one server.
    /// </summary>
    [TestClass]
    public class AllTests :TestAppFixture
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

        /// <summary>
        /// Inline javascript execution verified.
        /// 
        /// event driven script results not verified... assertion is expecting no result
        /// please break this test if you can. lol.
        /// 
        /// If reliable JS execution is required you will need to use a UX testing framework like
        /// Watin.
        /// </summary>
        [TestMethod]
        public void GetHtmlWithHttpRequestHelperAndParseWithMSHTMLAndVerifyJavascriptExecution()
        {
            Uri jsUri = NormalizeUri("TestJavascript.htm");

            string responseText = HttpRequestHelper.Get(jsUri, null, null);
            using (HttpRequestHelper browser = new HttpRequestHelper())
            {
                // parse the html in internet explorer
                IHTMLDocument2 doc = browser.ParseHtml(responseText);

                // get the resultant html
                string content = doc.body.outerHTML;

                Assert.IsTrue(content.IndexOf("HEY IM IN A SCRIPT") > 0, "inline script document.write not executed");
                Assert.IsTrue(content.IndexOf("this is some text from javascript") == -1,
                              "if failed then event driven script document.appendChile executed!");
            }
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

        /// <summary>
        /// Demonstrate the use of cookies across one or more http requests.
        /// All request methods on HttpRequestHelper can accept a CookieContainer...
        /// e.g. can use cookies with WebForms, Html, WCF and XML services....
        /// </summary>
        [TestMethod]
        public void GetWebFormWithHttpRequestHelperWithCookies()
        {
            // pass the same instance of CookieContainer to each request
            // cookies (session id, membership cache etc) are read from and written to
            CookieContainer cookies = new CookieContainer();

            // page that sets a cookie on the response
            Uri requestUri = NormalizeUri("TestWebFormCookies1.aspx");

            // pass the cookie container with the requestUri
            HttpRequestHelper.Get(requestUri, null, cookies);

            // get the cookies for this path from the container
            CookieCollection mycookies = cookies.GetCookies(requestUri);

            // find our cookie and test it
            Cookie cookie = mycookies["Cooookie"];
            Assert.IsNotNull(cookie);
            Assert.AreEqual("TestWebFormCookies1", cookie.Value);

            // page that reads cookie from request and ensures is present and
            // has expected value then writes it back out to the response with
            // a different value.
            requestUri = NormalizeUri("TestWebFormCookies2.aspx");

            // just pass the same CookieContainer
            HttpRequestHelper.Get(requestUri, null, cookies);

            //....
            mycookies = cookies.GetCookies(requestUri);
            cookie = mycookies["Cooookie"];
            Assert.IsNotNull(cookie);

            // bingo! 
            Assert.AreEqual("TestWebFormCookies2", cookie.Value);

            // now you can maintain state across requests
        }


        /// <summary>
        /// Just to demonstrate manual basic fetching - recommend using HttpRequestHelper
        /// </summary>
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

        [TestMethod]
        public void PostJsonGetJsonWithHttpRequestHelperWcfHelloWorld()
        {
            Uri uri = NormalizeUri("TestAjaxService.svc/HelloWorld");
            string json = HttpRequestHelper.AjaxTxt(uri, null, null);
            Assert.AreEqual("{\"d\":\"Hello World\"}", json);
        }

        [TestMethod]
        public void PostJsonGetJsonWithHttpRequestHelperWcfHelloWorldWithArgsInOut()
        {
            Uri uri = NormalizeUri("TestAjaxService.svc/HelloWorldWithArgsInOut");
            Dictionary<string, object> postData = new Dictionary<string, object>();

            // if you have a reference to the type expected you can serialize an instance
            // or just simply create an anonymous type that is shaped like the type expected...
            postData.Add("args", new {Message = "HI!"});

            string json = HttpRequestHelper.AjaxTxt(uri, postData, null);

            Assert.AreEqual(
                "{\"d\":{\"__type\":\"HelloWorldArgs:#CassiniDev.FixtureExamples.TestWeb\",\"Message\":\"you said: HI!\"}}",
                json);
        }

        [TestMethod]
        public void XmlWebServicePostFormGetXmlHelloWorld()
        {
            Uri uri = NormalizeUri("TestWebService.asmx/HelloWorld");
            string xml = HttpRequestHelper.Post(uri, null, null);
            //
            Assert.IsTrue(xml.IndexOf("<string xmlns=\"http://tempuri.org/\">Hello World</string>") > -1);
        }

        // no corresponding call with arguments for Form/Xml
        // cannot post complex objects as form data, only intrinsic scalar types.


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
            Assert.AreEqual(
                "{\"d\":{\"__type\":\"CassiniDev.FixtureExamples.TestWeb.HelloWorldArgs\",\"Message\":\"you said: HI!\"}}",
                json);
        }
    }
}