//Namespace in camerlCase
var chatta = {};

//Use Chatta.Models

chatta.chatMessage = function (sender, message, dateSent)
{
    var self = this;
    //self.username = sender;  for username implementation
    self.email = sender;
    self.message = message;
    if (dateSent != null)
    {
        self.timestamp = dateSent;
    }
}

//Connected User
chatta.user = function (email, userId)
{
    var self = this;
    //self.username = username;
    self.email = email;
    self.id = userId;
}

// ViewModels
chatta.chatViewModel = function ()
{
    var self = this;
    self.messages = ko.observableArray();
}

chatta.connectedUsersViewModel = function ()
{
    var self = this;
    self.contacts = ko.observableArray();
    self.customRemove = function (userToRemove)
    {
        var userIdToRemove = userToRemove.id;
        self.contacts.remove(function (item)
        {
            return item.id === userIdToRemove;
        });
    }
}
