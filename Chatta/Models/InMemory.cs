using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatta.Models
{
    public class InMemory
    {
        private static ICollection<ChatUser> connectedUsers;
        private static Dictionary<string, string> mappings;
        private static InMemory instance = null;

        public static InMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new InMemory();
            }
            return instance;
        }

        private InMemory()
        {
            connectedUsers = new List<ChatUser>();
            mappings = new Dictionary<string, string>();
        }

        public void Add(ChatUser user)
        {
            connectedUsers.Add(user);
        }

        public void Remove(ChatUser user)
        {
            connectedUsers.Remove(user);
        }
 
        public IQueryable<ChatUser> Users
        { get
            { return connectedUsers.AsQueryable(); }
        }

        public void AddMapping(string connectionId, string userId)
        {
            if (!string.IsNullOrEmpty(connectionId) && !string.IsNullOrEmpty(userId))
            {
                mappings.Add(connectionId, userId);
            }
        }

        public string GetUserByConnectionId(string connectionId)
        {
            string userId = null;
            mappings.TryGetValue(connectionId, out userId);
            return userId;
        }
    }
}