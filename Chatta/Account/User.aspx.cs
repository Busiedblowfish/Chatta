using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Chatta.Models;
using System.Threading;

namespace Chatta.Account
{
    public partial class User : System.Web.UI.Page
    {
        /*
        //Redirect User to Login page if they are not logged in already
        private void RedirectOnFail()
        {
            Response.Redirect((User.Identity.IsAuthenticated) ? "~/Account/User" : "~/Account/Login");
        }

        bool IsLoggedIn = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            if (!IsLoggedIn)
            {
                RedirectOnFail();
                return;
            }
            //Contiue to push Javascripts on webpage

            */
            /*
            if (!User.Identity.IsAuthenticated) // if the user is not logged in
            {
                Response.Redirect("~/Account/Login");
            }
            */

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        Response.Redirect("~/Account/Login");
                    }
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}