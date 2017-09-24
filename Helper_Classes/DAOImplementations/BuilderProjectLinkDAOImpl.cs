using System.Text;
using BuilderWebApp3.Helper_Classes.Constants;
using BuilderWebApp3.Helper_Classes.DAOInterfaces;
using BuilderWebApp3.Helper_Classes.ServiceHandlers;
using BuilderWebApp3.Models;

namespace BuilderWebApp3.Helper_Classes.DAOImplementations {
    public class BuilderProjectLinkDAOImpl : ModelDAOInterface {

        public string InsertToDbQuery(object model)
        {
            var link = model as BuilderProjectLink;
            return GetInsertQueryForDb(link);
        }

        public string GetDeleteBuilderQuery(string builderId)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("DELETE FROM ").Append(DBConstants.DbBuilderProjectLinkTable)
                .Append(" WHERE ").Append(DBConstants.LinkBuilderId).Append(" = '").Append(builderId).Append("' ");

            return strBuilder.ToString();
        }

        private static string GetInsertQueryForDb(BuilderProjectLink link) {
            var strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO ").Append(DBConstants.DbBuilderProjectLinkTable)
                .Append(" ( ").Append(DBConstants.LinkId).Append(",").Append(DBConstants.LinkBuilderId)
                .Append(",").Append(DBConstants.LinkProjectId).Append(" ) VALUES ('").Append(link.LinkId)
                .Append("','").Append(link.BuilderId).Append("','").Append(link.ProjectId).Append("')");

            return strBuilder.ToString();
        }

        public string GetDeleteProjectQuery(string projectId)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" DELETE FROM ").Append(DBConstants.DbBuilderProjectLinkTable)
                .Append(" WHERE ").Append(DBConstants.LinkProjectId).Append(" ='")
                .Append(projectId).Append("' ");

            return strBuilder.ToString();
        }
    }
}