using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Chatta.Account;

namespace Chatta.Models
{
    public class ChatUser
    {
        public string Id { get; set; }                  //connection ID of an authenticated user       
        //private string currentUser;
        public string Email
        {
            get;    // { return currentUser; }
            set;    //{ currentUser = HttpContext.Current.User.Identity.GetUserName();}
        }
    }
}