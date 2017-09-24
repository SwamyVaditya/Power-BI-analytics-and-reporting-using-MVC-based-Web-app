<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="BuilderWebApp3.Registration" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sign Up Form</title>
     <script src ="/Scripts/HelperScripts/registration.js"></script>
    <link rel="stylesheet" href="/css/registration.css">
    <link href='http://fonts.googleapis.com/css?family=Nunito:400,300' rel='stylesheet' type='text/css'>
</head>

<body>
    <form id="form" runat="server">
        <h1>Sign Up</h1>
        <fieldset>
            <legend><span class="number">1</span>Your basic info</legend>
            <label for="name">Name</label>
            <input type="text" id="name" name="user_name" runat="server" required></input>
            
            <label for="name">Login Name</label>
            <input type="text" id="loginname" name="login_name" runat="server" required></input>
            
            <label for="name">Organisation</label>
            <input type="text" id="organisation" name="organisation" runat="server" required></input>

            <label for="mail">Email</label>
            <input type="email" id="mail" name="user_email" runat="server" required></input>

            <label for="password">Password</label>
            <input type="password" id="password" name="user_password" runat="server" required></input>
        </fieldset>

        <fieldset>
            <legend><span class="number">2</span>Contact info</legend>
            <label for="name">Phone/Mobile</label>
            <input type="text" id="phone" name="phone" runat="server"></input>

            <label for="name">City</label>
            <input type="text" id="city" name="city" runat="server"></input>

            <label for="name">State</label>
            <input type="text" id="state" name="state" runat="server"></input>

            <label for="name">Country</label>
            <input type="text" id="country" name="country" runat="server"></input>
        </fieldset>
       
        <asp:Button CssClass="button" runat="server" type="submit" Onclick="ValidateAndAddUser" Text="Sign Up"></asp:Button>
    </form>

</body>
</html>
