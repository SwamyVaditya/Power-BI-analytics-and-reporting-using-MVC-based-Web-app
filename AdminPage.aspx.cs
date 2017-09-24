using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.UI;
using BuilderWebApp3.Helper_Classes;
using BuilderWebApp3.Helper_Classes.Constants;
using BuilderWebApp3.Helper_Classes.DAOImplementations;
using BuilderWebApp3.Models;

namespace BuilderWebApp3 {
    public partial class AdminPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void AddUser(object sender, EventArgs e)
        {
            Response.Redirect("/Registration.aspx");
        }

        protected void AddProject(object sender, EventArgs e)
        {

            try {
                AddProjectDiv.Visible = true;
                disbaleBackDiv.Visible = true;
                var builders = GetBuildersList();
                projaddbuilderlist.Items.Clear();
                foreach (var builder in builders) {
                    projaddbuilderlist.Items.Add(builder.Name + " , " + builder.UserName);
                }
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        protected void AddProjectToDb(object sender, EventArgs e)
        {
            try
            {
                var projectName = projaddnametxt.Value;
                var builderInfo = projaddbuilderlist.SelectedValue.Split(',');
                var builderLoginName = builderInfo[1].Trim();
                var builderId = BuilderDAOImpl.GetBuilderIdFromBuilderName(builderLoginName);
                var projectId = Utility.GenerateProjectId(projectName);
                var linkId = Utility.GenerateLinkId();
                var projectDaoImpl = new ProjectDAOImpl();
                var project = new Project(projectName,projectId);

                if (projectDaoImpl.IsProjectNameForBuilderPresent(builderId, projectName))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "javascript", "projectNameExists();", true);
                }
                else
                {
                    projectDaoImpl.AddProjectToDb(builderId, linkId, project);
                    AddProjectDiv.Visible = false;
                    disbaleBackDiv.Visible = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "javascript", "projectAdded();", true);
                }
            }
            catch (Exception ex)
            {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        protected void ValidateAndUpdateProjectToDb(object sender, EventArgs e) {
            try {
                var projectName = projupdatenametxt.Value;
                var projectId = Utility.GenerateProjectId(projectName);
                var oldProjId = (string)Session[SessionConstants.OldProjectId];
                var projectDaoImpl = new ProjectDAOImpl();
                var builderId = projectDaoImpl.GetBuilderinfo(oldProjId).BuilderId;
                Project project = null;
                if (!projectName.Trim().Equals(Session[SessionConstants.OldProjectName])) {
                    project = new Project(projectName, projectId) { PbiReportName = projreporttxt.Value };
                    if (projectDaoImpl.IsProjectNameForBuilderPresent(builderId, projectName)) {
                        ClientScript.RegisterStartupScript(this.GetType(), "javascript", "projectNameExists();", true);
                        return;
                    }
                }
                project = new Project(projectName, oldProjId) { PbiReportName = projreporttxt.Value };
                projectDaoImpl.UpdateProjectToDb(oldProjId, project);
                UpdateProjectDiv.Visible = false;
                disbaleBackDiv.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "projectUpdated();", true);
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        public void DisplayUsers()
        {
            var builders = GetBuildersList();
            DisplayBuildersOnPage(builders);
        }

        public void DisplayProjects() {
            var projects = GetProjectsList();
            DisplayProjectsOnPage(projects);
        }

        protected void ValidateAndUpdateUser(object sender, EventArgs e)
        {
            try {
                string builderId;
                if (!Session[SessionConstants.OldBuilderLoginName].Equals(edit_loginnameTxt.Value.Trim())) {
                    if (Utility.IsUserNamePresent(edit_loginnameTxt.Value.Trim())) {
                        ClientScript.RegisterStartupScript(this.GetType(), "javascript", "userNameExistsError();", true);
                        return;
                    }
                    // 2.Generate BuilderID and ProjectID
                    builderId = Utility.GenerateBuilderId(edit_loginnameTxt.Value);
                }
                else
                    builderId = (string) Session[SessionConstants.OldBuilderId];


                // 3. Update in Database
                UpdateBuilderInfoInDb(builderId);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "userUpdated();", true);
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        private void UpdateBuilderInfoInDb(string newBuilderId) {
            try {
                var builder = new Builder(newBuilderId, edit_loginnameTxt.Value, edit_passwordTxt.Value,
                    edit_mailTxt.Value, edit_phoneTxt.Value,
                    edit_cityTxt.Value, edit_stateTxt.Value, edit_countryTxt.Value, edit_nameTxt.Value,
                    edit_orgnameTxt.Value);
                var builderDaoImpl = new BuilderDAOImpl();
                builderDaoImpl.UpdateBuilder((string) Session[SessionConstants.OldBuilderId], builder);
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        protected void HideConfirmPanel(object sender, EventArgs e) {
            confirmPanel.Visible = false;
        }

        protected void HideConfirmPanelProject(object sender, EventArgs e) {
            ConfirmPanelProject.Visible = false;
        }

        protected void ConfirmDeleteUser(object sender, EventArgs e){
            confirmPanel.Visible = true;
        }

        protected void ConfirmDeleteProject(object sender, EventArgs e) {
            ConfirmPanelProject.Visible = true;
        }

        protected void DeleteUser(object sender, EventArgs e)
        {
            confirmPanel.Visible = false;
            try {
                var builderId = hidden.Value;
                var builderDaoImpl = new BuilderDAOImpl();
                builderDaoImpl.DeleteBuilder(builderId);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript",
                    "deleteSuccess(\"User\");", true);
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        protected void DeleteProject(object sender, EventArgs e) {
            ConfirmPanelProject.Visible = false;
            try {
                var projectId = hiddenProject.Value;
                var projectDaoImpl = new ProjectDAOImpl();
                projectDaoImpl.DeleteProject(projectId);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript",
                    "deleteSuccess(\"Project\");", true);
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        protected void ManageUser(object sender, EventArgs e)
        {
            try
            {
                builderTableDiv.Visible = false;
                addBuilderBtn.Visible = false;
                builderManageDiv.Visible = true;
                var builderId = hidden.Value;
                var builderDaoImpl = new BuilderDAOImpl();
                var builder =  builderDaoImpl.ManageBuilder(DBConstants.BuilderId,builderId);
                if (builder == null)
                    return;

                builderLbl.Text = builder.Name.Trim();
                edit_builderidTxt.Value = builder.BuilderId.Trim();
                edit_nameTxt.Value = builder.Name.Trim();
                edit_loginnameTxt.Value = builder.UserName.Trim();
                Session[SessionConstants.OldBuilderLoginName] = builder.UserName.Trim();
                Session[SessionConstants.OldBuilderId] = builder.BuilderId.Trim();
                edit_mailTxt.Value = builder.EmailId.Trim();
                edit_passwordTxt.Value = builder.Password.Trim();
                edit_orgnameTxt.Value = builder.Organisation.Trim();
                edit_phoneTxt.Value = builder.Contact.Trim();
                edit_cityTxt.Value = builder.City.Trim();
                edit_stateTxt.Value = builder.State.Trim();
                edit_countryTxt.Value = builder.Country.Trim();
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        protected void ManageProject(object sender, EventArgs e)
        {
            try
            {
                disbaleBackDiv.Visible = true;
                UpdateProjectDiv.Visible = true;
                var projectId = hiddenProject.Value;
                var projectDaoImpl = new ProjectDAOImpl();
                var project = projectDaoImpl.ManageProject(projectId);
                if (project == null)
                    return;

                projupdatenametxt.Value = project.ProjectName.Trim();
                projreporttxt.Value = project.PbiReportName.Trim();
                Session["OldProjectId"] = project.ProjectId.Trim();
                Session["OldProjectName"] = project.ProjectName.Trim();
            }
            catch (Exception ex)
            {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        private IEnumerable<Builder> GetBuildersList()
        {
            try
            {
                var builderDaoImpl = new BuilderDAOImpl();
                return builderDaoImpl.GetBuildersList();
            }
            catch (Exception ex)
            {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
            return null;
        }

        private IEnumerable<Project> GetProjectsList() {
            try {
                var projectDaoImpl = new ProjectDAOImpl();
                return projectDaoImpl.GetProjectsList();
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
            return null;
        }

        private void DisplayBuildersOnPage(IEnumerable<Builder> builders)
        {
            try
            {
                if (builders == null)
                {
                    builderConTable.InnerHtml = "<p> No content to display. Click 'Add User' to get started.</p>";
                    return;
                }

                var htmlStr = String.Empty;
                foreach (var builder in builders)
                {
                    htmlStr += AppendToHtmlBuilder(builder.Name, builder.Organisation, builder.UserName, builder.Country,builder.BuilderId);
                }
                builderConTable.InnerHtml = htmlStr;

            }
            catch (Exception ex)
            {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        private void DisplayProjectsOnPage(IEnumerable<Project> projects) {
            try {
                if (projects == null) {
                    projectConTable.InnerHtml = "<p> No content to display. Click 'Add Project' to get started.</p>";
                    return;
                }

                var htmlStr = String.Empty;
                foreach (var project in projects)
                {
                    ProjectBuilderInfo projBuilderInfo = GetProjectBuilderInfo(project.ProjectId);
                    htmlStr += AppendToHtmlProject(project.ProjectName, projBuilderInfo.BuilderName, projBuilderInfo.BuilderOrg, project.ProjectId,projBuilderInfo.BuilderLoginName);
                }
                projectConTable.InnerHtml = htmlStr;

            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        private ProjectBuilderInfo GetProjectBuilderInfo(string projectId)
        {
            try {
                var projectDaoImpl = new ProjectDAOImpl();
                return projectDaoImpl.GetBuilderinfo(projectId);
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
            return null;
        }

        private static string AppendToHtmlBuilder(string name, string org, string loginName, string country,string builderId)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("<div class=\"UserRow\">").AppendLine()
                .Append("<div id=\"namediv\">").AppendLine()
                .Append("<label id=\"name\">").Append(name.Trim()).Append("</label>").AppendLine()
                .Append("</div>").AppendLine()
                .Append("<div id=\"orgdiv\">").AppendLine()
                .Append("<label id=\"organisation\">").Append(org.Trim()).Append("</label>").AppendLine()
                .Append("</div>").AppendLine()
                .Append("<div id=\"loginnamediv\">").AppendLine()
                .Append("<label id=\"loginname\">").Append(loginName.Trim()).Append("</label>").AppendLine()
                .Append("</div>").AppendLine()
                .Append("<div id=\"countrydiv\">").AppendLine()
                .Append("<label id=\"country\">").Append(country.Trim()).Append("</label>").AppendLine()
                .Append("</div>").AppendLine()
                .Append("<button class=\"ManageButton\" id=").Append("\"" + builderId.Trim() + "\"").Append(">Manage</button>").AppendLine()
                .Append("<button class=\"DeleteButton\" id=").Append("\"" + builderId.Trim() + "\"").Append(">Delete</button>").AppendLine()
                .Append("</div>").AppendLine();

            return strBuilder.ToString();
        }

        private static string AppendToHtmlProject(string projName, string builderName, string orgName, string projectId, string loginName) {
            var strBuilder = new StringBuilder();
            strBuilder.Append("<div class=\"ProjectRow\">").AppendLine()
                .Append("<div id=\"projectnamediv\">").AppendLine()
                .Append("<label id=\"projectname\">").Append(projName.Trim()).Append("</label>").AppendLine()
                .Append("</div>").AppendLine()
                .Append("<div id=\"projectbuilderdiv\">").AppendLine()
                .Append("<label id=\"projectbuilder\">").Append(builderName.Trim() + " (" + loginName.Trim() + ")").Append("</label>").AppendLine()
                .Append("</div>").AppendLine()
                .Append("<div id=\"projectorgdiv\">").AppendLine()
                .Append("<label id=\"projectorganisation\">").Append(orgName.Trim()).Append("</label>").AppendLine()
                .Append("</div>").AppendLine()
                .Append("<button class=\"ManageButtonProject\" id=").Append("\"" + projectId.Trim() + "\"").Append(">Manage</button>").AppendLine()
                .Append("<button class=\"DeleteButtonProject\" id=").Append("\"" + projectId.Trim() + "\"").Append(">Delete</button>").AppendLine()
                .Append("</div>").AppendLine();

            return strBuilder.ToString();
        }

        protected void LoadBuilder(object sender, EventArgs e)
        {
            adminDiv.Visible = false;
            builderBtn.BackColor = Color.White;
            builderBtn.ForeColor = ColorTranslator.FromHtml("#262626");
            projectBtn.BackColor = ColorTranslator.FromHtml("#0099cc");
            projectBtn.ForeColor = Color.White;
            builderLbl.Text = "Builders";
            builderLbl.Visible = true;
            builderTableDiv.Visible = true;
            addBuilderBtn.Visible = true;
            addProjectBtn.Visible = false;
            builderManageDiv.Visible = false;
            ProjectTableDiv.Visible = false;
            DisplayUsers();
        }

        protected void LoadProject(object sender, EventArgs e)
        {
            adminDiv.Visible = false;
            projectBtn.BackColor = Color.White;
            projectBtn.ForeColor = ColorTranslator.FromHtml("#262626");
            builderBtn.BackColor = ColorTranslator.FromHtml("#0099cc");
            builderBtn.ForeColor = Color.White;
            addBuilderBtn.Visible = false;
            builderTableDiv.Visible = false;
            builderManageDiv.Visible = false;
            ProjectTableDiv.Visible = true;
            addProjectBtn.Visible = true;
            projaddnametxt.Value=null;
            projaddbuilderlist.SelectedValue = null;
            builderLbl.Text = "Projects";
            builderLbl.Visible = true;
            AddProjectDiv.Visible = false;
            disbaleBackDiv.Visible = false;
            UpdateProjectDiv.Visible = false;
            DisplayProjects();
        }

        protected void LoadAdminHome(object sender, EventArgs e)
        {
            adminDiv.Visible = true;
            builderTableDiv.Visible = false;
            ProjectTableDiv.Visible = false;
            builderLbl.Visible = false;
            addBuilderBtn.Visible = false;
            addProjectBtn.Visible = false;
            projectBtn.BackColor = ColorTranslator.FromHtml("#0099cc");
            projectBtn.ForeColor = Color.White;
            builderBtn.BackColor = ColorTranslator.FromHtml("#0099cc");
            builderBtn.ForeColor = Color.White;
        }

    }

    public class ProjectBuilderInfo
    {
        public ProjectBuilderInfo(string builderName, string builderOrg, string loginName, string builderId)
        {
            BuilderName = builderName;
            BuilderOrg = builderOrg;
            BuilderLoginName = loginName;
            BuilderId = builderId;
        }

        public string BuilderName { get; set; }
        public string BuilderOrg { get; set; }
        public string BuilderLoginName { get; set; }
        public string BuilderId { get; set; }
    }
}