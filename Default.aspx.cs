using System;
using System.Web.UI;
using BuilderWebApp3.Helper_Classes;
using BuilderWebApp3.Helper_Classes.Constants;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace BuilderWebApp3 {
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void signInButton_Click(object sender, EventArgs e) {

            try {
                // Validate LoggedInUser
                Session[SessionConstants.LoggedInUser] = nameBox.Value.Trim();
                if (!LoggedInUserValidated((string) Session[SessionConstants.LoggedInUser])) {
                    ClientScript.RegisterStartupScript(this.GetType(), "javascript", "invalidUser();", true);
                    return;
                }

                //Get access token: 
                // To call a Power BI REST operation, create an instance of AuthenticationContext and call AcquireToken
                // Create an instance of AuthenticationContext to acquire an Azure access token
                var tokenCache = new TokenCache();
                var authContext = new AuthenticationContext(PbiConstants.AuthorityUri, tokenCache);
                ValidateToken(authContext, PbiConstants.ResourceUri, PbiConstants.ClientId,
                    new Uri(PbiConstants.RedirectUri));
            }
            catch (Exception ex) {
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert(\"" + exStr + "\");", true);
            }
        }

        private static bool LoggedInUserValidated(string loggedInUser) {
            try {
                return Utility.IsUserNamePresent(loggedInUser);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while validating logged in user.",e);
            }
        }

        private void ValidateToken(AuthenticationContext authContext, string resourceUri, string clientId, Uri redirectUri) {

            AuthenticationResult authResult = null;
            if (Session[SessionConstants.AuthToken] != null) {
                authResult = (AuthenticationResult)Session[SessionConstants.AuthToken];
            }
            else {
                try {
                    authResult = authContext.AcquireToken(resourceUri, clientId, redirectUri);
                }
                catch (Exception ex) {
                    var exStr = string.Format("Authentication Failed!!!" + ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                    ClientScript.RegisterStartupScript(this.GetType(), "Message", "alert(\"" + exStr + "\")", true);
                }
                Session[SessionConstants.AuthToken] = authResult;
            }

            if (authResult == null)
                return;

            Response.Redirect("/PowerBIPage.aspx");
        }

    }
}