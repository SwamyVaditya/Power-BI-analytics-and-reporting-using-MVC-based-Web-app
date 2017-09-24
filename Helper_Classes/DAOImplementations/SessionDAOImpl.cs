using System;
using System.Text;
using BuilderWebApp3.Helper_Classes.Constants;
using BuilderWebApp3.Helper_Classes.DAOInterfaces;
using BuilderWebApp3.Models;

namespace BuilderWebApp3.Helper_Classes.DAOImplementations {
    public class SessionDAOImpl : ModelDAOInterface {

        public string InsertToDbQuery(object model)
        {
            var sessionObj = model as Session;
            return GetInsertQueryForDb(sessionObj);
        }

        private static string GetInsertQueryForDb(Session sessionObj)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO ").Append(DBConstants.DbSessionTable)
                .Append("(").Append(DBConstants.SessionLinkId).Append(", ").Append(DBConstants.SessionId)
                .Append(", ").Append(DBConstants.StartTime).Append(", ").Append(DBConstants.Location)
                .Append(", ").Append(DBConstants.Latitute).Append(", ").Append(DBConstants.Longitude)
                .Append(") VALUES(").Append("'")
                .Append(sessionObj.LinkId).Append("','")
                .Append(sessionObj.SessionId).Append("','").Append(sessionObj.StartTime)
                .Append("', '").Append(sessionObj.Location).Append("',").Append(sessionObj.Latitude)
                .Append(",").Append(sessionObj.Longitude).Append(")");

            return strBuilder.ToString();
        }

        public static string GetEndTimeQueryForDb(string sessionId, DateTime endTime)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("UPDATE ").Append(DBConstants.DbSessionTable)
                .Append(" SET ").Append(DBConstants.EndTime).Append(" = '")
                .Append(endTime).Append("' ").Append("WHERE ").Append(DBConstants.SessionId).Append(" = '")
                .Append(sessionId).Append("'");

            return strBuilder.ToString();
        }
    }
}