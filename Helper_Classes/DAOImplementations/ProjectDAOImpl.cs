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
    public class ProjectDAOImpl : ModelDAOInterface{

        public string InsertToDbQuery(object model)
        {
            var projectObj = model as Project;
            return GetInsertQueryForDb(projectObj);
        }

        private static string GetInsertQueryForDb(Project projObj) {
            var strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO ").Append(DBConstants.DbProjectTable).Append(" ( ")
                .Append(DBConstants.ProjectName).Append(", ").Append(DBConstants.ProjectId)
                .Append(" ) VALUES ('").Append(projObj.ProjectName).Append("','")
                .Append(projObj.ProjectId).Append("')");

            return strBuilder.ToString();
        }

        public string GetDeleteBuilderQuery(string builderId)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" DELETE FROM ").Append(DBConstants.DbProjectTable)
                .Append(" WHERE ").Append(DBConstants.ProjectId).Append(" IN (SELECT ")
                .Append(DBConstants.LinkProjectId).Append(" FROM ").Append(DBConstants.DbBuilderProjectLinkTable)
                .Append(" WHERE ").Append(DBConstants.LinkBuilderId).Append(" = '").Append(builderId)
                .Append("' )");

            return strBuilder.ToString();
        }

        public IEnumerable<Project> GetProjectsList()
        {
            try {
                var query = GetProjectListQueryForDb();
                var daoHandler = new DAOServiceHandler();
                var ds = daoHandler.ExecuteQueryForDb(query);
                return GetProjects(ds);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while getting list of projects from database.",e);
            }
        }

        public ProjectBuilderInfo GetBuilderinfo(string projectId)
        {
            try {
                var query = GetProjectBuilderQueryForDb(projectId);
                var daoHandler = new DAOServiceHandler();
                var ds = daoHandler.ExecuteQueryForDb(query);
                return GetProjectBuilder(ds);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while getting project builder info from project id for the given project.",e);
            }
        }

        private static string GetProjectListQueryForDb() {
            var strBuilder = new StringBuilder();
            strBuilder.Append("SELECT * FROM ").Append(DBConstants.DbProjectTable);
            return strBuilder.ToString();
        }

        public static IEnumerable<Project> GetProjects(DataSet ds) {
            List<Project> projects = null;
            if (ds.Tables[0] == null)
                return null;
            try {
                var rows = ds.Tables[0].Rows;
                foreach (DataRow dataRow in rows) {
                    var projectId = dataRow[DBConstants.ProjectId].ToString();
                    var projectName = dataRow[DBConstants.ProjectName].ToString();
                    var pbiReportName = dataRow[DBConstants.PbiReportName].ToString();
                    var project = new Project(projectName, projectId) {PbiReportName = pbiReportName};
                    if (projects == null)
                        projects = new List<Project>();
                    projects.Add(project);
                }
                return projects;
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while iterating over list of projects from database.",e);
            }
        }

        private static ProjectBuilderInfo GetProjectBuilder(DataSet ds)
        {
            if (ds.Tables[0] == null)
                return null;
            try {
                var dataRow = ds.Tables[0].Rows[0];
                var builderName = dataRow[DBConstants.DispName].ToString();
                var builderLoginName = dataRow[DBConstants.UserName].ToString();
                var orgName = dataRow[DBConstants.Organisation].ToString();
                var builderId = dataRow[DBConstants.BuilderId].ToString();
                return new ProjectBuilderInfo(builderName, orgName, builderLoginName, builderId);
            }catch(Exception e){
                throw new Exception("Exception occurred while getting project builder info from database.",e);
            }
        }

        private static string GetProjectBuilderQueryForDb(string projectId)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ").Append(DBConstants.DispName).Append(",")
                .Append(DBConstants.Organisation).Append(",").Append(DBConstants.UserName)
                .Append(",").Append(DBConstants.BuilderId)
                .Append(" FROM ").Append(DBConstants.DbBuilderTable)
                .Append(" WHERE ").Append(DBConstants.BuilderId).Append(" IN (").Append(" SELECT ")
                .Append(DBConstants.LinkBuilderId).Append(" FROM ").Append(DBConstants.DbBuilderProjectLinkTable)
                .Append(" WHERE ").Append(DBConstants.LinkProjectId).Append(" = '").Append(projectId).Append("' )");

            return strBuilder.ToString();
        }

        public void DeleteProject(string projectId)
        {
            var queries = new List<string>(2);
            try {
                // 1. Delete query from BuilderToProject
                var linkDaoImpl = new BuilderProjectLinkDAOImpl();
                var queryFromLink = linkDaoImpl.GetDeleteProjectQuery(projectId);
                queries.Add(queryFromLink);

                // 2. Delete query from ProjectDetails 
                var queryFromProject = GetDeleteProjectQuery(projectId);
                queries.Add(queryFromProject);

                var daoHandler = new DAOServiceHandler();
                daoHandler.ExecuteQueryForDb(queries);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while deleting project from database.",e);
            }
        }

        private static string GetDeleteProjectQuery(string projectId)
        {
            var strBuilder =new StringBuilder();
            strBuilder.Append(" DELETE FROM ").Append(DBConstants.DbProjectTable)
                .Append(" WHERE ").Append(DBConstants.ProjectId).Append(" ='")
                .Append(projectId).Append("' ");
            return strBuilder.ToString();
        }

        public void AddProjectToDb(string builderId, object linkId, Project project)
        {
            try {
                var queries = new List<string>(2);
                // 1. Save to Project table query
                var projSaveQuery = GetProjectSaveQuery(project);
                queries.Add(projSaveQuery);

                // 2. Save to BuilderProjectLink Table query
                var linkSaveQuery = GetLinkSaveQuery(linkId, builderId, project.ProjectId);
                queries.Add(linkSaveQuery);

                // 3. Save to DB
                var daoHandler = new DAOServiceHandler();
                daoHandler.ExecuteQueryForDb(queries);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while adding project in database.",e);
            }
        }

        private static string GetLinkSaveQuery(object linkId, string builderId, string projectId)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" INSERT INTO ").Append(DBConstants.DbBuilderProjectLinkTable)
                .Append(" ( ").Append(DBConstants.LinkId).Append(",").Append(DBConstants.LinkBuilderId)
                .Append(",").Append(DBConstants.LinkProjectId).Append(" ) VALUES ('")
                .Append(linkId).Append("' , '").Append(builderId).Append("' , '")
                .Append(projectId).Append("' )");

            return strBuilder.ToString();
        }

        private static string GetProjectSaveQuery(Project project)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" INSERT INTO ").Append(DBConstants.DbProjectTable)
                .Append(" ( ").Append(DBConstants.ProjectName).Append(",").Append(DBConstants.ProjectId)
                .Append(" ) VALUES ( '").Append(project.ProjectName).Append("' , '")
                .Append(project.ProjectId).Append("' )");

            return strBuilder.ToString();
        }

        public bool IsProjectNameForBuilderPresent(string builderId, string projectName)
        {
            try {
                var query = IsProjectNameForBuilderPresentQuery(builderId, projectName);
                var daoHandler = new DAOServiceHandler();
                var ds = daoHandler.ExecuteQueryForDb(query);
                return ds.Tables[0]?.Rows.Count > 0;
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while checking if project name already exists for the given builder or not.",e);
            }
        }

        private static string IsProjectNameForBuilderPresentQuery(string builderId, string projectName)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT * FROM ").Append(DBConstants.DbBuilderProjectLinkTable)
                .Append(" WHERE ").Append(DBConstants.LinkBuilderId).Append(" = '").Append(builderId)
                .Append("' AND ").Append(DBConstants.LinkProjectId).Append(" IN ( SELECT ")
                .Append(DBConstants.ProjectId).Append(" FROM ").Append(DBConstants.DbProjectTable)
                .Append(" WHERE ").Append(DBConstants.ProjectName).Append(" = '").Append(projectName)
                .Append("' )");

            return strBuilder.ToString();
        }

        public Project ManageProject(string projectId)
        {
            try {
                var query = GetProjectQueryForDb(projectId);
                var daoHandler = new DAOServiceHandler();
                var ds = daoHandler.ExecuteQueryForDb(query);
                return GetProjects(ds).ElementAt(0);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while getting project info from project id in database.",e);
            }
        }

        private static string GetProjectQueryForDb(string projectId)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("SELECT * FROM ").Append(DBConstants.DbProjectTable)
                .Append(" WHERE ").Append(DBConstants.ProjectId).Append(" = '")
                .Append(projectId).Append("' ");

            return strBuilder.ToString();
        }

        public void UpdateProjectToDb(string oldProjId, Project project)
        {
            try {
                var queries = new List<string>(1);
                var updateProjectTableQuery = GetUpdateProjectInprojectTableQuery(oldProjId, project);
                queries.Add(updateProjectTableQuery);
                var daoHandler = new DAOServiceHandler();
                daoHandler.ExecuteQueryForDb(queries);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while updating project information in database.",e);
            }
        }

        private static string GetUpdateProjectInprojectTableQuery(string oldProjId, Project project)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" UPDATE ").Append(DBConstants.DbProjectTable).Append(" SET ")
                .Append(DBConstants.ProjectId).Append(" = '").Append(project.ProjectId).Append("' , ")
                .Append(DBConstants.ProjectName).Append(" = '").Append(project.ProjectName).Append("' , ")
                .Append(DBConstants.PbiReportName).Append(" = '").Append(project.PbiReportName).Append("' WHERE ")
                .Append(DBConstants.ProjectId).Append(" = '").Append(oldProjId).Append("'");

            return strBuilder.ToString();
        }

        private static string GetUpdateProjectInLinkTableQuery(string oldProjId, Project project)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" UPDATE ").Append(DBConstants.DbBuilderProjectLinkTable).Append(" SET ")
                .Append(DBConstants.LinkProjectId).Append(" = '").Append(project.ProjectId).Append("' WHERE ")
                .Append(DBConstants.LinkProjectId).Append(" = '").Append(oldProjId).Append("'");

            return strBuilder.ToString(); 
        }
    }
}