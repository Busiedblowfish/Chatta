using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using Chatta.Account;
using Chatta.Models;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Chatta
    {
        public class ChatHub : Hub
        {
            /* For testing message broadcast to all connected clients
            public void Send(string email, string message)
            {
                //Broadcast the message to all clients including self
                Clients.All.broadcastMessage(email, message);
            }
            */

            private InMemory repository;

            public ChatHub()
            {
                repository = InMemory.GetInstance();
            }

        #region IDisconnect and IConnected event handlers implementation

        /// <summary>
        /// Fired when a client disconnects from the system. The user associated with the client ID gets deleted from the list of currently connected users.
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
                string userId = repository.GetUserByConnectionId(Context.ConnectionId);
                if (userId != null)
                {
                    ChatUser user = repository.Users.Where(u => u.Id == userId).FirstOrDefault();
                    if (user != null)
                    {
                        repository.Remove(user);
                        return Clients.All.leaves(user.Id, user.Email, DateTime.Now);
                    }
                }
            return base.OnDisconnected(stopCalled);
        }

        //Extract Url links from chat messages using regex
        public string TextParser (string url)
        {
            url = Regex.Replace(url, @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)",
                                    "<a target='_blank' href='$1'>$1</a>");
            return url;
        }
            #endregion

            #region Chat event handlers

            /// <summary>
            /// Fired when a client send a message to the server.
            /// </summary>
            /// <param name="message"></param>
            public void Send(ChatMessage message)
            {
                if (!string.IsNullOrEmpty(message.Message))
                {
                    // Sanitize input
                    message.Message = HttpUtility.HtmlEncode(message.Message);
                    // Process URLs: Extract any URL
                    message.Message = TextParser(message.Message);
                    message.Timestamp = DateTime.Now;
                    Clients.All.onMessageReceived(message);
                }
            }

            /// <summary>
            /// Fired when a client joins the chat. Here round trip state is available and we can register the user in the list
            /// </summary>
            public void Joined()
            {
            ChatUser user = new ChatUser()
            {
                //Id = Context.ConnectionId,                
                Id = Guid.NewGuid().ToString(),
                Email = HttpContext.Current.User.Identity.GetUserId()
                };
                repository.Add(user);
                repository.AddMapping(Context.ConnectionId, user.Id);
                Clients.All.joins(user.Id, user.Email, DateTime.Now);
            }

            /// <summary>
            /// Invoked when a client connects. Retrieves the list of all currently connected users
            /// </summary>
            /// <returns></returns>
            public ICollection<ChatUser> GetConnectedUsers()
            {
                return repository.Users.ToList();
            }

            #endregion
        }
    }
