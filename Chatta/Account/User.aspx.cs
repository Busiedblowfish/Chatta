using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace Chatta.Account
{
    public partial class User : System.Web.UI.Page
    {
        //Redirect User to Login page if they are not logged in
        private void RedirectOnFail()
        {
            Response.Redirect((User.Identity.IsAuthenticated) ? "~/Account/User" : "~/Account/Login");
        }

        bool isLoggedIn = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!isLoggedIn)
            {
                RedirectOnFail();
                return;
            }
            Page.Validate();
            if(Page.IsValid)
            {
                try
                {
                }

                catch
                {

                }
            }
        }
    }
}