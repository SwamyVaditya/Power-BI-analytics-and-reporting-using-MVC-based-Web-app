<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BuilderWebApp3.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Sign in and client authorization-->
    <div class="loginDiv">
        <h1 style="text-align: center"> Visualize and analyze your data</h1>
        <img src="/Image/PowerBILogo.png" alt="">
        <h1 class="signintxt" >Sign in to your Power BI to get started </h1>
        <asp:Panel ID="signinPanel" runat="server" Visible="true" CssClass="signinpanel">
            <div id="center" align="center">
                <input type="text" id="nameBox" placeholder="Username" class="nameBox" runat="server" required=""/>
                <asp:Button ID="signInButton" runat="server" OnClick="signInButton_Click" Text="Sign in to Power BI" CssClass="signInButton" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>

