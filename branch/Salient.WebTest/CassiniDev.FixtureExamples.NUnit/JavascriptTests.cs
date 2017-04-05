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
using CassiniDev.Testing;
using mshtml;
using NUnit.Framework;

namespace CassiniDev.FixtureExamples.NUnit
{
    /// <summary>
    /// See AllTests.cs for commentary
    /// </summary>
    [TestFixture, Category("Discrete")]
    public class JavascriptTests : TestAppFixture
    {
        [Test]
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
    }
}