using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using BuilderWebApp3.Helper_Classes.Constants;
using BuilderWebApp3.Helper_Classes.DAOImplementations;
using BuilderWebApp3.Helper_Classes.PBIClasses;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;

namespace BuilderWebApp3 {
    public partial class PowerBIPage : Page {
        protected void Page_Load(object sender, EventArgs e) {
            try {
                var builderDaoImpl = new BuilderDAOImpl();
                var builder = builderDaoImpl.ManageBuilder(DBConstants.UserName, (string)Session[SessionConstants.LoggedInUser]);
                buildernamelbl.InnerText = builder.Name.Trim();
                signinlbltxt.InnerText = builder.UserName.Trim();
                orglbltxt.InnerText = builder.Organisation.Trim();
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        protected void Logout(object sender, EventArgs e) {
            Response.Redirect("Default.aspx");
        }

        private WebRequest WebRequest(string url,string method) {
            //Configure datasets request
            WebRequest request = System.Net.WebRequest.Create(string.Format("{0}" + url, PbiConstants.BaseUri)) as HttpWebRequest;

            if (request == null) return null;
            request.Method = method;
            request.ContentLength = 0;
            request.Headers.Add("Authorization",string.Format("Bearer {0}", ((AuthenticationResult)Session[SessionConstants.AuthToken]).AccessToken));
            return request;
        }

        protected void DisplayProjects(object sender, EventArgs e) {
            PanelEmbedReport.Visible = false;
            loading.Visible = false;
            dummy.Visible = false;
            projectcondiv.Visible = true;
            var userName = (string)Session[SessionConstants.LoggedInUser];
            var builderDaoImpl = new BuilderDAOImpl();
            var projects = builderDaoImpl.GetBuilderProjects(userName);
            try {
                if (projects == null) {
                    projectcondiv.InnerHtml = "<p> No content to display.</p>";
                    return;
                }
                var htmlStr = string.Empty;
                foreach (var project in projects) {
                    htmlStr += AppendToHtmlProject(project.ProjectName, project.PbiReportName);
                }
                projectcondiv.InnerHtml = htmlStr;
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        private static string AppendToHtmlProject(string projectName, string pbiReportName) {
            var strBuilder = new StringBuilder();

            strBuilder.Append("<div class=\"ProjectRow\" runat=\"server\">").AppendLine()
                .Append("<label class=\"projectname\" runat=\"server\">").Append(projectName.Trim()).Append("</label>")
                .Append("<button class=\"viewreportbtn\" runat=\"server\" id=\"" + pbiReportName.Trim() +
                        "\" >View Report</button>").AppendLine()
                .Append("</div>");

            return strBuilder.ToString();
        }

        protected void ViewPbiReport(object sender, EventArgs e) {

            var reportName = hidden.Value;
            if (string.IsNullOrEmpty(reportName)) {
                ScriptManager.RegisterClientScriptBlock(this,typeof(Page),"javascript","alert(\"No report found for the Project. Please contact your administrator.\")",true);
                return;
            }
            try {
                EmbedPbiReport(reportName);
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "errorFunc(\"" + exStr + "\");", true);
            }
        }

        private void EmbedPbiReport(string reportName) {
            try {
                var request = WebRequest("reports", "GET");
                var responseContent = string.Empty;
                tb_EmbedURL.Value = string.Empty;
                reportnamelbl.InnerText = string.Empty;

                //Get datasets response from request.GetResponse()
                using (var response = request.GetResponse() as System.Net.HttpWebResponse) {
                    //Get reader from response stream
                    if (response == null) return;
                    using (var reader = new StreamReader(response.GetResponseStream())) {
                        responseContent = reader.ReadToEnd();
                        //Deserialize JSON string
                        var entireJson = JToken.Parse(responseContent);
                        var inner = entireJson["value"].Value<JArray>();
                        var isFirst = true;
                        foreach (var token in inner.Children()) {
                            var report = token.ToObject<PbiReport>();
                            if (!report.name.Trim().Equals(reportName.Trim())) continue;
                            tb_EmbedURL.Value = report.embedUrl.Trim();
                            accesstoken.Value = ((AuthenticationResult) Session[SessionConstants.AuthToken]).AccessToken;
                            break;
                        }
                        if (string.IsNullOrEmpty(tb_EmbedURL.Value)) {
                            ScriptManager.RegisterClientScriptBlock(this, typeof (Page), "javascript",
                                "alert(\"No report found for the Project. Please contact your administrator.\")", true);
                            return;
                        }
                        PanelEmbedReport.Visible = true;
                        reportnamelbl.InnerText = reportName;
                        ClientScript.RegisterStartupScript(this.GetType(), "javascript", "loadPbiReport(); ", true);
                    }
                }
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while embedding PBI report in application.",e);
            }
        }

    }
}