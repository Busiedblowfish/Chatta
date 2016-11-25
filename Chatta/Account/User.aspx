<%@ Page Title="Chatta: Chat Hub" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Chatta.Account.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="whole-box-header">
        <div class="groupchat-header"></div>
        <div class="userbox-header"></div>
    </div>
    <div id="whole-box">
        <div class="groupChat-box">
                <div class="lower-left">
                <div class="msg-box">
                    <asp:TextBox ID="TextBox1" style="resize:none" runat="server" Height="100%" Width="100%" TextMode="MultiLine" Rows="4" Font-Names="Candara" Font-Size="Larger">
                    </asp:TextBox>
                </div>
                <div class="send-box">
                    <asp:Button ID="btnSend" class="btn btn-primary btn-lg" runat="server" Height="100%" Text="Send" ToolTip="Send Message" Width="100%"/>
                </div>
            </div>
        </div>
        <div class="user-box"></div>
    </div>
</asp:Content>
