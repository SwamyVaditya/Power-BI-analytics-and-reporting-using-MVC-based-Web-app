<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="BuilderWebApp3.AdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Protal</title>
    <link rel="stylesheet" href="/css/admin.css" type="text/css" />
     <link rel="stylesheet" href="css/builder/builderstable.css" type="text/css" />
     <link rel="stylesheet" href="css/builder/buildermanage.css" type="text/css" />
     <link rel="stylesheet" href="css/builder/builderdelete.css" type="text/css" />
    <link rel="stylesheet" href="css/project/projectstable.css" type="text/css" />
     <link rel="stylesheet" href="css/project/projectdelete.css" type="text/css" />
     <link rel="stylesheet" href="css/project/projectadd.css" type="text/css" />
    <link rel="stylesheet" href="css/project/projectmanage.css" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
</head>
<body runat="server" style="height: 772px">
<script src="Scripts/HelperScripts/adminpage.js"></script>
    <div id="headerCentered">
        <label id="adminLbl">Admin Portal</label>
    </div>
    <form id="form1" runat="server">
        <asp:Panel runat="server" CssClass="sidePanel">
           
            <asp:Button ID="homeBtn" runat="server" CssClass="homeBtn" OnClick="LoadAdminHome"/>
            <asp:Button ID="builderBtn" runat="server" CssClass="builderBtn" OnClick="LoadBuilder" Text="Builders      " />
            <asp:Button ID="projectBtn" runat="server" CssClass="projectBtn" OnClick="LoadProject" Text="Projects      " />
            
            <asp:Label ID="builderLbl" runat="server" CssClass="builderLbl" Text="Builders" Visible="False"></asp:Label>     
            <asp:Button ID="addBuilderBtn" runat="server" Text="Add User" CssClass="addBuilderBtn" Visible="False" OnClick="AddUser"/>
            <asp:Button ID="addProjectBtn" runat="server" Text="Add Project" CssClass="addProjectBtn" Visible="False" OnClick="AddProject"/>
            
            <%--This is the home page div for admin--%>
            <div id="adminDiv" runat="server" visible="true">
                <img id="adminImg" alt="" src="Image/admin_3.jpg"/>
            </div>

            <%--This is the main div for displaying all builders--%>
            <div id="builderTableDiv" runat="server" visible="False">
                <asp:Label ID="builderNameLbl" runat="server" CssClass="builderNameLbl" Text="Name"></asp:Label>
                <asp:Label ID="orgNameLbl" runat="server" CssClass="orgNameLbl" Text="Organisation  "></asp:Label>
                <asp:Label ID="loginNameLbl" runat="server" CssClass="loginNameLbl" Text="Login Name"></asp:Label>
                <asp:Label ID="countryNameLbl" runat="server" CssClass="countryNameLbl" Text="Country"></asp:Label>
                <asp:Label ID="actionLbl" runat="server" CssClass="actionLbl" Text="Actions"></asp:Label>
                <hr class="headingTblLine" />
                
                <button id="dummyDelete" runat="server" class="dummyBuilderIdBtnDelete" EnableViewState="false" OnServerClick="ConfirmDeleteUser"></button>
                <button id="dummyManage" runat="server" class="dummyBuilderIdBtnManage" EnableViewState="false" OnServerClick="ManageUser"></button>
                <asp:HiddenField ID="hidden"  runat="server" ClientIDMode="Static" />

                <asp:Panel ID="confirmPanel" runat="server" CssClass="confirmPanel" Visible="false">
                    <asp:Label ID="confirmLbl" runat="server" CssClass="confirmLbl" Text="Are you sure you want to permanently delete this user and all of its related content? "></asp:Label>
                    <asp:Button ID="YesBtn" runat="server" CssClass="YesBtn" Text="Confirm" OnClick="DeleteUser" />
                    <asp:Button ID="NoBtn" runat="server" CssClass="NoBtn" Text="Cancel" OnClick="HideConfirmPanel"/>
                </asp:Panel>

                <div runat="server" id="builderConTable">
                    <%--This is the html to be generated.--%>
                   <%-- <div class="UserRow">
                        <div id="namediv">
                            <label id="name">Abhishek Tripathi</label>
                        </div>
                        <div id="orgdiv">
                            <label id="organisation">Shoonya Technologies</label>
                        </div>
                        <div id="loginnamediv">
                            <label id="loginname">AbhishekT</label>
                        </div>
                        <div id="countrydiv">
                            <label id="country">India</label>
                        </div>
                        <button runat=\"server\" CssClass=\"ManageButton\" >Manage</button>
                        <button runat="server"CssClass=\"DeleteButton\" >Delete</button>
                       <label id="builderIdLbl" runat="server" visible="false"></label>
                    </div>--%>
                    <%--  --%>
                </div>
            </div>

            <%--This is the main div for managing a builder--%>
            <div id="builderManageDiv" runat="server" visible="false">
                <div id="BasicInfoDiv" runat="server">
                    <label id="BasicInfoLbl" class="BasicInfoLbl">Basic Info</label>
                    <hr class="BasicInfoLine" />
                    <input type="text" id="edit_builderidTxt" runat="server"></input>
                    <label id="edit_nameLbl">Name</label>
                    <input type="text" id="edit_nameTxt" runat="server" required></input>
                    <label id="edit_loginnameLbl">Login Name</label>
                    <input type="text" id="edit_loginnameTxt" runat="server" required></input>
                    <label id="edit_mailLbl">Email</label>
                    <input type="email" id="edit_mailTxt" runat="server" required></input>
                    <label id="edit_passwordLbl">Password</label>
                    <input type="text" id="edit_passwordTxt" runat="server" required></input>
                </div>

                <div id="OrgInfoDiv" runat="server">
                    <label id="OrgInfoLbl" class="OrgInfoLbl">Organisation Details</label>
                    <hr class="OrgInfoLine" />
                    <label id="edit_orgnameLbl">Organisation</label>
                    <input type="text" id="edit_orgnameTxt" runat="server" required></input>
                </div>

                <div id="ContactInfoDiv" runat="server">
                    <label id="ContactInfoLbl" class="ContactInfoLbl">Contact Details</label>
                    <hr class="ContactInfoLine" />
                    <label id="edit_phoneLbl">Phone/Mobile</label>
                    <input type="text" id="edit_phoneTxt" runat="server"></input>
                    <label id="edit_cityLbl">City</label>
                    <input type="text" id="edit_cityTxt" runat="server"></input>
                    <label id="edit_stateLbl">State</label>
                    <input type="text" id="edit_stateTxt" runat="server"></input>
                    <label id="edit_countryLbl">Country</label>
                    <input type="text" id="edit_countryTxt" runat="server"></input>
                </div>

                <asp:Button ID="SaveChanges" runat="server" CssClass="SaveChanges" OnClick="ValidateAndUpdateUser" Text="Save"/>
                <button id="CancelChanges" runat="server" class="CancelChanges" OnServerClick="LoadBuilder">Cancel</button>
            </div>

            <%--This is the main div for displaying all projects--%>
            <div id="ProjectTableDiv" runat="server" visible="False">
                <asp:Label runat="server" CssClass="projectNameLbl" Text="Project Name"></asp:Label>
                <asp:Label runat="server" CssClass="projectbuilderNameLbl" Text="Builder Name (Login Name)"></asp:Label>
                <asp:Label runat="server" CssClass="projectorgNameLbl" Text="Organisation"></asp:Label>
                <asp:Label runat="server" CssClass="projectactionLbl" Text="Actions"></asp:Label>
                <hr class="projectheadingTblLine" />
                
                <button id="dummyProjectIdBtnDelete" runat="server" class="dummyProjectIdBtnDelete" EnableViewState="false" OnServerClick="ConfirmDeleteProject"></button>
                <button id="dummyProjectIdBtnManage" runat="server" class="dummyProjectIdBtnManage" EnableViewState="false" OnServerClick="ManageProject"></button>
                <asp:HiddenField ID="hiddenProject"  runat="server" ClientIDMode="Static" />

                <asp:Panel ID="ConfirmPanelProject" runat="server" CssClass="ConfirmPanelProject" Visible="false">
                    <asp:Label ID="confirmProjectLbl" runat="server" CssClass="confirmProjectLbl" Text="Are you sure you want to permanently delete this project and all of its related content? "></asp:Label>
                    <asp:Button ID="YesBtnProject" runat="server" CssClass="YesBtnProject" Text="Confirm" OnClick="DeleteProject" />
                    <asp:Button ID="NoBtnProject" runat="server" CssClass="NoBtnProject" Text="Cancel" OnClick="HideConfirmPanelProject"/>
                </asp:Panel>

                <div runat="server" id="projectConTable">
                    <%--This is the html to be generated.--%>
                   <%-- <div class="ProjectRow">
                        <div id="projectnamediv">
                            <label id="projectname">Project 1</label>
                        </div>
                       <div id="projectbuilderdiv">
                            <label id="projectbuilder">AbhishekT</label>
                        </div>
                        <div id="projectorgdiv">
                            <label id="projectorganisation">Shoonya Technologies</label>
                        </div>
                        <button runat="server"CssClass=\"DeleteButtonProject\" >Delete</button>
                    </div>--%>
                    <%--  --%>
                </div>
            </div>
            
            <%--This is the main div for adding a project--%>
            <div id="disbaleBackDiv" runat="server" visible="False">
            </div>
            <div id="AddProjectDiv" runat="server" visible="false">
                <div class="projectHeader">
                    <label class="projHeaderLbl">Project</label>
                </div>
                <label id="projectaddname">Name</label>
                <input type="text" id="projaddnametxt" runat="server" required=""/>
                <label id="projaddbuilder">Builder</label>
                <asp:DropDownList ID="projaddbuilderlist" CssClass="projaddbuilderlist" runat="server" ></asp:DropDownList>
                
                <asp:Button ID="projaddyesbtn" CssClass="projaddyesbtn" runat="server" Text="Add" OnClick="AddProjectToDb" />
                <button id="projaddnobtn" class="projaddnobtn" runat="server" OnServerClick="LoadProject" >Cancel</button>
            </div>
            
            <%--This is the main div for updating a project--%>
            <div id="UpdateProjectDiv" runat="server" visible="false">
                <div class="projectHeader">
                    <label class="projHeaderLbl">Project</label>
                </div>
                <label id="projectupdatename">Name</label>
                <input type="text" id="projupdatenametxt" runat="server" required=""/>
                <label id="projreport">Report</label>
                <input type="text" id="projreporttxt" runat="server" />
                
                <asp:Button ID="projupdateyesbtn" CssClass="projupdateyesbtn" runat="server" Text="Save" OnClick="ValidateAndUpdateProjectToDb" />
                <button id="projupdatenobtn" class="projupdatenobtn" runat="server" OnServerClick="LoadProject" >Cancel</button>
            </div>

        </asp:Panel>  
            
    </form>
</body>
</html>
