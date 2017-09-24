using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BuilderWebApp3.Helper_Classes.Constants;
using BuilderWebApp3.Helper_Classes.DAOInterfaces;
using BuilderWebApp3.Helper_Classes.ServiceHandlers;
using BuilderWebApp3.Models;

namespace BuilderWebApp3.Helper_Classes.DAOImplementations {
    public class BuilderDAOImpl : ModelDAOInterface {

        public string InsertToDbQuery(object model) {
            var builderObj = model as Builder;
            return GetInsertQueryForDb(builderObj);
        }

        public Builder ManageBuilder(string builderQueryParam,string builderId) {
            try {
                var query = GetBuilderQueryForDb(builderQueryParam, builderId);
                var daoHandler = new DAOServiceHandler();
                var ds = daoHandler.ExecuteQueryForDb(query);
                return GetBuilders(ds).ElementAt(0);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while getting builder information from database.",e);
            }
        }

        public void DeleteBuilder(string builderId) {
            var queries = new List<string>(3);
            try {
                // 1. Delete query from BuilderToProject
                var linkDaoImpl = new BuilderProjectLinkDAOImpl();
                var queryFromLink = linkDaoImpl.GetDeleteBuilderQuery(builderId);
                queries.Add(queryFromLink);

                // 2. Delete query from ProjectDetails 
                var projectDaoImpl = new ProjectDAOImpl();
                var queryFromProject = projectDaoImpl.GetDeleteBuilderQuery(builderId);
                queries.Add(queryFromProject);

                // 3. Delete from BuilderDetails
                var query = GetDeleteBuilderQueryForDb(builderId);
                queries.Add(query);

                var daoHandler = new DAOServiceHandler();
                daoHandler.ExecuteQueryForDb(queries);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while deleting builder from database.",e);
            }
        }

        public IEnumerable<Builder> GetBuildersList() {
            try {
                var query = GetBuilderListQueryForDb();
                var daoHandler = new DAOServiceHandler();
                var ds = daoHandler.ExecuteQueryForDb(query);
                return GetBuilders(ds);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while getting list of builders from database.",e);
            }
        }

        private static IEnumerable<Builder> GetBuilders(DataSet ds) {
            List<Builder> builders = null;
            if (ds.Tables[0] == null)
                return null;
            try {
                var rows = ds.Tables[0].Rows;
                foreach (DataRow dataRow in rows) {
                    var builderId = dataRow[DBConstants.BuilderId].ToString();
                    var username = dataRow[DBConstants.UserName].ToString();
                    var pwd = dataRow[DBConstants.Password].ToString();
                    var city = dataRow[DBConstants.City].ToString();
                    var state = dataRow[DBConstants.State].ToString();
                    var country = dataRow[DBConstants.Country].ToString();
                    var email = dataRow[DBConstants.EmailId].ToString();
                    var contact = dataRow[DBConstants.Contact].ToString();
                    var dispName = dataRow[DBConstants.DispName].ToString();
                    var org = dataRow[DBConstants.Organisation].ToString();
                    var builder = new Builder(builderId, username, pwd, email, contact, city, state, country, dispName,
                        org);
                    if (builders == null)
                        builders = new List<Builder>();
                    builders.Add(builder);
                }
                return builders;
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while iterating over list of builders in database.",e);
            }
        }

        private static string GetBuilderListQueryForDb() {
            var strBuilder = new StringBuilder();
            strBuilder.Append("SELECT * FROM ").Append(DBConstants.DbBuilderTable);
            return strBuilder.ToString();
        }

        private static string GetBuilderQueryForDb(string builderQueryparam,string builderId) {
            var strBuilder = new StringBuilder();
            strBuilder.Append("SELECT * FROM ").Append(DBConstants.DbBuilderTable)
                .Append(" WHERE ").Append(builderQueryparam).Append(" = '")
                .Append(builderId).Append("' ");

            return strBuilder.ToString();
        }

        public bool IsUserNamePresent(string userName) {
            try {
                var query = IsUserNamePresentQuery(userName);
                var daoHandler = new DAOServiceHandler();
                var ds = daoHandler.ExecuteQueryForDb(query);
                return ds.Tables[0]?.Rows.Count > 0;
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while checking if username already exists in database or not.",e);
            }
        }

        private static string IsUserNamePresentQuery(string userName) {
            var strBuilder = new StringBuilder();
            strBuilder.Append("SELECT ").Append(DBConstants.UserName)
                .Append(" FROM ").Append(DBConstants.DbBuilderTable)
                .Append(" WHERE ").Append(DBConstants.UserName).Append(" = '")
                .Append(userName).Append("'");

            return strBuilder.ToString();
        }

        private static string GetInsertQueryForDb(Builder builderObj) {
            var strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO ").Append(DBConstants.DbBuilderTable).Append(" ( ")
                .Append(DBConstants.BuilderId).Append(",").Append(DBConstants.EmailId)
                .Append(",").Append(DBConstants.UserName)
                .Append(",").Append(DBConstants.Password).Append(",").Append(DBConstants.Contact)
                .Append(",").Append(DBConstants.City).Append(",").Append(DBConstants.State).Append(",")
                .Append(DBConstants.Country).Append(",").Append(DBConstants.DispName).Append(",")
                .Append(DBConstants.Organisation)
                .Append(" ) VALUES ('").Append(builderObj.BuilderId).Append("','")
                .Append(builderObj.EmailId).Append("','")
                .Append(builderObj.UserName).Append("','").Append(builderObj.Password).Append("','")
                .Append(builderObj.Contact).Append("','").Append(builderObj.City).Append("','")
                .Append(builderObj.State).Append("','").Append(builderObj.Country).Append("','")
                .Append(builderObj.Name).Append("','").Append(builderObj.Organisation).Append("')");

            return strBuilder.ToString();
        }

        private static string GetDeleteBuilderQueryForDb(string builderId) {
            var strBuilder = new StringBuilder();
            strBuilder.Append("DELETE FROM ").Append(DBConstants.DbBuilderTable)
                .Append(" WHERE ").Append(DBConstants.BuilderId).Append(" = '").Append(builderId).Append("' ");

            return strBuilder.ToString();
        }

        public void UpdateBuilder(string oldBuilderId, Builder builder) {
            try {
                var queries = new List<string>(1);
                var updateBuilderTableQuery = GetUpdateUserInBuilderTableQuery(oldBuilderId, builder);
                queries.Add(updateBuilderTableQuery);
                var daoHandler = new DAOServiceHandler();
                daoHandler.ExecuteQueryForDb(queries);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while updating builder information in database.",e);
            }
        }

        private static string GetUpdateUserInBuilderTableQuery(string oldBuilderId, Builder builder) {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" UPDATE ").Append(DBConstants.DbBuilderTable).Append(" SET ")
                .Append(DBConstants.BuilderId).Append(" = '").Append(builder.BuilderId).Append("' , ")
                .Append(DBConstants.EmailId).Append(" = '").Append(builder.EmailId).Append("' , ")
                .Append(DBConstants.UserName).Append(" = '").Append(builder.UserName).Append("' , ")
                .Append(DBConstants.Password).Append(" = '").Append(builder.Password).Append("' , ")
                .Append(DBConstants.Contact).Append(" = '").Append(builder.Contact).Append("' , ")
                .Append(DBConstants.City).Append(" = '").Append(builder.City).Append("' , ")
                .Append(DBConstants.State).Append(" = '").Append(builder.State).Append("' , ")
                .Append(DBConstants.Country).Append(" = '").Append(builder.Country).Append("' , ")
                .Append(DBConstants.DispName).Append(" = '").Append(builder.Name).Append("' , ")
                .Append(DBConstants.Organisation).Append(" = '").Append(builder.Organisation).Append("' WHERE ")
                .Append(DBConstants.BuilderId).Append(" = '").Append(oldBuilderId).Append("'");

            return strBuilder.ToString();
        }

        public static string GetBuilderIdFromBuilderName(string builderLoginName) {
            try {
                var query = GetIdFromLoginNameQuery(builderLoginName.Trim());
                var daoHandler = new DAOServiceHandler();
                var ds = daoHandler.ExecuteQueryForDb(query);
                var builder = GetBuilders(ds).ElementAt(0);
                return builder.BuilderId.Trim();
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while getting builder Id from builder name.",e);
            }
        }

        private static string GetIdFromLoginNameQuery(string builderLoginName) {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT * FROM ").Append(DBConstants.DbBuilderTable)
                .Append(" WHERE ").Append(DBConstants.UserName).Append(" = '")
                .Append(builderLoginName).Append("' ");

            return strBuilder.ToString();
        }

        public IEnumerable<Project> GetBuilderProjects(string userName) {
            try {
                var query = GetBuilderProjectQuery(userName);
                var daohandler = new DAOServiceHandler();
                var ds = daohandler.ExecuteQueryForDb(query);
                return ProjectDAOImpl.GetProjects(ds);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while getting projects information for the builder.",e);
            }
        }

        private static string GetBuilderProjectQuery(string userName) {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT * FROM ").Append(DBConstants.DbProjectTable).Append(" WHERE ")
                .Append(DBConstants.ProjectId).Append(" IN ( SELECT ").Append(DBConstants.LinkProjectId)
                .Append(" FROM ").Append(DBConstants.DbBuilderProjectLinkTable).Append(" WHERE ")
                .Append(DBConstants.LinkBuilderId).Append(" IN ( SELECT ").Append(DBConstants.BuilderId)
                .Append(" FROM ").Append(DBConstants.DbBuilderTable).Append(" WHERE ").Append(DBConstants.UserName)
                .Append(" = '").Append(userName).Append("' ) )");

            return strBuilder.ToString();
        }
    }
}