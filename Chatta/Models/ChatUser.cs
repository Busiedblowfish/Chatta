using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatta.Models
{
    public class ChatUser
    {
        public string Id { get; set; }  //connection ID of an authenticated user
        public string Email { get; set; }   //Email address associated with the user
        //public string Username { get; set; }    //Username associated with the user
      
    }
}