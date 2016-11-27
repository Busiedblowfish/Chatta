using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;

namespace Chatta
{
    public class ChatHub : Hub
    {
        public void Send(string email, string message)
        {
            //Broadcast the message to all clients including self
            Clients.All.broadcastMessage(email, message);
        }
    }
}