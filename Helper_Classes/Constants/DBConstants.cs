using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuilderWebApp3.Helper_Classes.Constants {
    public class DBConstants {

        // name of the Database table for Session Info
        public static string DbSessionTable = "dbo.SessionInfo";
        // name of the Database table for Builer Info
        public static string DbBuilderTable = "dbo.BuilderDetails";
        // name of the Database table for Project Info
        public static string DbProjectTable = "dbo.ProjectDetails";
        // name of the Database table for Session to project Link
        public static string DbBuilderProjectLinkTable = "dbo.BuilderToProject";
        // name of the Server on which database is hosted
        public static string DbServer = "tcp:laffxh61on.database.windows.net,1433";
        // name of the database
        public static string DbName = "sqlDB1";
        // Username
        public static string DbUserName = "abhishek@laffxh61on";
        // Password
        public static string DbPassword = "21&KIRAN7";


        // DB Table's column constants
        // DB Table BuilderDetails
        public static string BuilderId = "BuilderID";
        public static string EmailId = "EmailID";
        public static string UserName = "UserName";
        public static string Contact = "Contact";
        public static string City = "City";
        public static string State = "State";
        public static string Country = "Country";
        public static string Password = "Password";
        public static string DispName = "Name";
        public static string Organisation = "Organisation";

        // DB Table ProjectDetails
        public static string ProjectId = "ProjectID";
        public static string ProjectName = "ProjectName";
        public static string PbiReportName = "PBIReportName";

        // DB Table BuilderToProjectLink
        public static string LinkId = "LinkID";
        public static string LinkBuilderId = "BuilderID";
        public static string LinkProjectId = "ProjectID";

        // DB Table SessionInfo
        public static string SessionLinkId = "LinkID";
        public static string SessionId = "Session_ID";
        public static string StartTime = "StartTime";
        public static string EndTime = "EndTime";
        public static string Location = "Location";
        public static string Latitute = "Latitude";
        public static string Longitude = "Longitude";

    }
}