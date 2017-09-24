using System;
using BuilderWebApp3.Helper_Classes.DAOImplementations;

namespace BuilderWebApp3.Helper_Classes {
    public class Utility {

        public static string GenerateBuilderId(string loginName) {
            return loginName + "~" + Guid.NewGuid().ToString();
        }

        public static bool IsUserNamePresent(string loginName) {
            try {
                var builderDaoImpl = new BuilderDAOImpl();
                return builderDaoImpl.IsUserNamePresent(loginName);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while checking if username already present in database or not.",e);
            }
        }

        public static string GenerateProjectId(string projectName)
        {
            return projectName + "~" + Guid.NewGuid().ToString();
        }

        public static object GenerateLinkId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}