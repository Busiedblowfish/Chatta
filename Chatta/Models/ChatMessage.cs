﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatta.Models
{
    public class ChatMessage
    {
        //Getters and setters for ChatMessage class
        public string Email { get; set; }           //Email address of an authenticated user
        //public string Username { get; set; }      //Username of an authenticated user
        public string Message { get; set; }         //Message sent by each client
        public DateTime Timestamp { get; set; }     //Time message was delivered/sent
    }
}