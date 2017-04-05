using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CassiniDev.FixtureExamples.TestWeb
{
    public partial class TestWebFormCookies2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Cooookie"] == null)
            {
                throw new NullReferenceException("cookie is null");
            }
            if (Request.Cookies["Cooookie"].Value != "TestWebFormCookies1")
            {
                throw new NullReferenceException("did not get cookie from TestWebFormCookies1");
            }

            Response.Cookies.Add(new HttpCookie("Cooookie", "TestWebFormCookies2"));

        }
    }
}
