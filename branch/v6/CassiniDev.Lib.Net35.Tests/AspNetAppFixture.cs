using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace CassiniDev.Lib.Net35.Tests
{

    [TestFixture]
    public class AspNetAppFixture : CassiniDevServer
    {

        #region Plumbing
        [TestFixtureSetUp]
        public void Start()
        {
            string content = new ContentLocator("TestContent/AspNetApp").LocateContent();
            StartServer(content);



        }
        [TestFixtureTearDown]
        public void Stop()
        {

            StopServer();
        }
        #endregion

        [Test]
        public void SessionTest()
        {
            var url = NormalizeUrl("default.aspx");
            WebClient client = new WebClient();
            var response = client.DownloadString(url);
            // 
            const string sessionRx = "SessionLabel\">(?<session>.*?)<";

            var session = Regex.Match(response, sessionRx, RegexOptions.ExplicitCapture).Groups["session"].Value;
            var session2 = Regex.Match(response, sessionRx, RegexOptions.ExplicitCapture).Groups["session"].Value;

            Assert.AreEqual(session, session2, "Sessions should be same");

        }


    }
}
