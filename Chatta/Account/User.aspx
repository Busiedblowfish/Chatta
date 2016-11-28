﻿<%@ Page Title="Chatta: Chat Hub" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Chatta.Account.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- This is the title header for the chat hub -->
    <div id="whole-box-header">
        <div class="groupchat-header"></div>
        <div class="userbox-header"></div>
    </div>
    <!-- This is the whole chat box, split into individual divs -->
    <div id="whole-box">
        <div class="groupChat-box">
            <div id="chat-box">
                <!-- This will display the list of group chats -->
                <ul>
                    <li>
                        <div class="chat-property-email" data-bind="text: email">
                        </div>
                        <div class="chat-property-message" data-bind="text: message">
                        </div>
                        <div class="chat-property-timestamp" data-bind="text: timestamp.toLocaleTimeString()">
                        </div>
                    </li>
                </ul>
            </div>
            <!-- This is the Textbox1 and send div -->
            <div class="lower-left">
                <div class="msg-box">
                    <textarea id="Textbox1" maxlength="160" rows="4" cols="40" placeholder="Type your message here"></textarea>
                </div>
                <div class="send-box">
                    <!-- Some issues with running Button controller on the server side and firing javascript at the same time 
                    <asp:Button ID="btnSend" class="btn btn-primary btn-lg" runat="server" Height="100%" Text="Send" ToolTip="Send Message" Width="100%"/>
                    -->
                    <input id="sendButton" class="btn btn-primary btn-lg" style="height: 100%; width: 100%" type="submit" value="Send" />
                </div>
            </div>
        </div>
        <!-- This is the list of connected clients-->
        <div id="user-box">
            <ul data-bind="foreach: contacts" >
                <li class="user-list" data-bind="text: email"></li>
            </ul>
        </div>
    </div>
    <!--Script references. -->
    <!--Reference the jQuery library. -->
    <script src="/Scripts/jquery-3.1.1.min.js"></script>
    <!--Reference the SignalR library. -->
    <script src="/Scripts/jquery.signalR-2.2.1.js"></script>
    <!--Reference the autogenerated SignalR hub script. -->
    <script src="/signalr/hubs"></script>
    <!--Reference Chatta.js for Knockout javascript bindings on the chat page. -->
    <script src="/Scripts/knockout-3.4.0.js"></script>
    <script src="/Scripts/Chatta.js"></script>
    <!--Add script to update the page and send messages to connected clients.-->
    <script type="text/javascript">
        $(document).ready(function () {
            //Call the chatviewModel for page rendering
            var chat = new chatta.chatViewModel();
            var users = new chatta.connectedUsersViewModel();
            var currentUser = new chatta.user.email;

            // Declare a proxy to reference the hub(ChatHub will be reference using camelCase (chatHub)).
            var chattaHub = $.connection.chatHub;
            chattaHub.state.email = currentUser.email;

            //Call clientside event handlers from /ChatHub(userId, message, timestamp)
            //userId is the connectionID of individual connected client/user
            chattaHub.client.onMessageReceived = function (relay) {
                var date = new Date().toLocaleTimeString();         //Current local time
                //Push message to clients
                chattaHub.messages.push(new chatta.chatMessage(relay.Email, relay.Message, date));
                var objDiv = document.getElementById("#chat-box");
                objDiv.scrollTop = objDiv.scrollHeight - objDiv.clientHeight;
            }

            //A client has left the hub, remove user from user view
            chattaHub.client.leaves = function (userId, email, timestamp) {
                var disconnectedUser = new chatta.user(email, userId);
                users.customRemove(disconnectedUser);
            }

            //A new client has joined the hub, add new user to user view
            chattaHub.client.joins = function (userId, email, timestamp) {
                var connectedUser = new chatta.user(email, userId);
                users.contacts.push(connectedUser);
            }

            //Display message in #Textbox1 of user that composed it
            function sendMessageContent() {
                var content = $("#Textbox1").val();
                if (content != "" && content != null) {
                    var msg = new chatR.chatMessage(currentUser.email, content);
                    // Call the Send method on the hub(server).
                    chattaHub.server.send(msg).done(function () {
                        // Clear text box and reset focus for next comment. 
                        $("#Textbox1").val("").focus();
                    }).fail(function (e) {
                        //Check for errors
                        alert("Error connection to server");
                    });
                }
            }

            //Handles event for clicking the send button
            $("#sendButton").click(function () {
                sendMessageContent();
            });

            // Handles Enter keystroke press event
            $('#Textbox1').keypress(function (e) {
                if (e.which == 13) {
                    sendMessageContent();
                }
            });

            //Apply Knockout JS bindings on the chat area views
            ko.applyBindings(users, $("#user-box")[0]);
            ko.applyBindings(chat, $("#chat-area")[0]);

            // Step 1: Start the connection
            // Step 2: Initiate all currenlty connected users
            // Step 3: Join users to chat hub
            // Step 4: Notify all users of currenlty connected users
            $.connection.hub.start().done(function () {
                chattaHub.server.getConnectedUsers()
                            .done(function (connectedUsers) {
                                ko.utils.arrayForEach(connectedUsers, function (item) {
                                    users.contacts.push(new chatta.user(item.email, item.userId));
                                });
                            }).done(function () {
                                chatHub.server.joined();
                            });
            });

        });
    </script>
</asp:Content>
