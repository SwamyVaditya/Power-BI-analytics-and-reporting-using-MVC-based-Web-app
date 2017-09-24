using System;
using System.Web.UI;
using BuilderWebApp3.Helper_Classes;
using BuilderWebApp3.Helper_Classes.Constants;
using BuilderWebApp3.Helper_Classes.ServiceHandlers;
using BuilderWebApp3.Models;

namespace BuilderWebApp3 {
    public partial class Registration : Page {
        protected void Page_Load(object sender, EventArgs e) {
        }

        public void ValidateAndAddUser(object sender, EventArgs e) {

            try
            {
                // Steps:
                // 1.Check if username already exists
                if (Utility.IsUserNamePresent(loginname.Value))
                    ClientScript.RegisterStartupScript(this.GetType(), "javascript", "userNameExistsError();", true);
                else
                {
                    // 2.Generate BuilderID and ProjectID
                    Session[SessionConstants.BuilderId] = Utility.GenerateBuilderId(loginname.Value);

                    // 3. Store in Database
                    InsertToDatabase();
                    ClientScript.RegisterStartupScript(this.GetType(), "javascript", "userRegistered();", true);
                }
            } catch (Exception ex){
                var exStr = string.Format(ex.Message + "\\nRoot Cause :\\n" + ex.GetBaseException().Message);
                ClientScript.RegisterStartupScript(this.GetType(), "javascript", "daoInitError(\"" + exStr + "\");", true);

            }

        }

        private void InsertToDatabase() {
            try {
                var daoHandler = new DAOServiceHandler();
                // Insert Builder in DB
                var builder = new Builder((string) Session[SessionConstants.BuilderId], loginname.Value, password.Value,
                    mail.Value, phone.Value,
                    city.Value, state.Value, country.Value, name.Value, organisation.Value);
                daoHandler.InsertToDb(builder);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while inserting new user/builder in database.",e);
            }
        }

    }
}