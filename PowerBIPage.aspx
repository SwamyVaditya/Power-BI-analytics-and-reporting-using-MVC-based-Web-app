<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PowerBIPage.aspx.cs" Inherits="BuilderWebApp3.PowerBIPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Power BI </title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <link rel="stylesheet" href="/css/powerbi/powerbi.css">
</head>

<body>
    <form id="form1" runat="server">
        <script src="/Scripts/HelperScripts/powerbi.js"></script>

        <%--Top header div--%>
        <div id="header">
            <label runat="server" id="buildernamelbl"></label>
            <div id="headerleft">
                <label id="signinlbl">Signed In As : </label>
                <label id="orglbl">Organisation : </label>
                <label id="signinlbltxt" runat="server"></label>
                <label id="orglbltxt" runat="server"></label>
                <asp:Button ID="logoutbtn" runat="server" CssClass="logoutbtn" Text="Logout" OnClick="Logout" />
            </div>
        </div>

        <label id="projectlbl">Projects</label>
        <hr id="projectline" />

        <div id="loading" visible="true" runat="server">
            <img id="loadimg" src="Image/loadingicon.gif" alt="">
        </div>
        <button id="dummy" runat="server" onserverclick="DisplayProjects"></button>

        <button id="dummyViewPbiReport" runat="server" class="dummyViewPbiReport" enableviewstate="false" onserverclick="ViewPbiReport"></button>
        <asp:HiddenField ID="hidden" runat="server" ClientIDMode="Static" />

        <%--Embed report in iframe--%>
        <asp:Panel ID="PanelEmbedReport" runat="server" Visible="false" ClientIDMode="Static" CssClass="PanelEmbedReport">
            <label id="reportnamelbl" runat="server"></label>
            <button id="backbtn" runat="server" OnServerClick="DisplayProjects"></button>
            <iframe runat="server" id="iFrameEmbedReport" seamless visible="true" onerror="alert('Failed to load report')"></iframe>
            <input type="text" id="tb_EmbedURL" runat="server" />
            <input type="text" id="accesstoken" runat="server" />
        </asp:Panel>

        <%--Actual project content div--%>
        <div id="projectcondiv" visible="false" runat="server">
            <%--this is the structure of each row in project div--%>
            <%-- <div class="ProjectRow" runat="server">
                <label class="projectname" runat="server">Project 1 for Shoonya </label>
                <button class="viewreportbtn" id="" runat="server">View Report</button>
            </div>--%>
        </div>

    </form>
</body>
</html>


