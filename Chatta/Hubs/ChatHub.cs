using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Chatta;
using Chatta.Models;
using Chatta.Account;

namespace Chatta.Hubs
{
    [HubName("chattaHub")]
    public class ChatHub : Hub
    {
        private InMemory repository;

        public ChatHub()
        {
            repository = InMemory.GetInstance();
        }

        /* Implementation of Iconnected and IDisconnect event handlers
           Will fire whenever a user loses connection or log off
           The user in question will be removed from the list of available/online users
        */


    }
}