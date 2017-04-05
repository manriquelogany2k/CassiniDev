using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CassiniDev.FixtureExamples.TestWeb
{
    public partial class TestWebFormCookies1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies.Add(new HttpCookie("Cooookie", "TestWebFormCookies1"));
        }
    }
}
