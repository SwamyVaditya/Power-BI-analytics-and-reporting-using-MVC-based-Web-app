using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BuilderWebApp3.Helper_Classes.Constants;
using BuilderWebApp3.Helper_Classes.DAOImplementations;

namespace BuilderWebApp3.Helper_Classes.ServiceImplementations {
    public class DAOServiceImplementation {

        private SqlConnection Conn { get; set; }

        public DAOServiceImplementation() {
            try {
                InitDb();
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while connecting to database.",e);
            }
        }

        private void InitDb() {
            try {
                Conn = new SqlConnection(GetDbConnectionString());
            }
            catch (Exception e) {
               throw new Exception("Exception occurred while initialising database.",e);
            }
        }

        private static string GetDbConnectionString() {
            var strBuilder = new StringBuilder();
            strBuilder.Append("Server=").Append(DBConstants.DbServer)
                .Append(";Database=").Append(DBConstants.DbName)
                .Append(";User id=").Append(DBConstants.DbUserName)
                .Append(";Password=").Append(DBConstants.DbPassword)
                .Append(";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");

            return strBuilder.ToString();
        }

        public void InsertToDb(object obj)
        {
            try {
                var factory = new ModelFactory();
                var model = factory.GetAppropriateModel(obj);
                var query = model.InsertToDbQuery(obj);
                ExecuteInsertQueryForDb(query);
            }
            catch (Exception e) {
                throw new Exception("Exception occured while insertinfg into database.",e);
            }
        }

        private void ExecuteInsertQueryForDb(string query) {
            try
            {
                Conn.Open();
                var sqlQuery = new SqlCommand(query, Conn);
                var adapter = new SqlDataAdapter(sqlQuery);
                var ds = new DataSet();
                adapter.Fill(ds);
                Conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Exception occurred while executing query in database.",e);
            }
        }

        public void InsertSessionEndTimeToDb(string sessionId, DateTime endTime)
        {
            try {
                var insertQuery = SessionDAOImpl.GetEndTimeQueryForDb(sessionId, endTime);
                ExecuteInsertQueryForDb(insertQuery);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while inserting session end time in database.",e);
            }
        }

        public DataSet ExecuteQueryForDb(string query)
        {
            try {
                Conn.Open();
                var sqlQuery = new SqlCommand(query, Conn);
                var adapter = new SqlDataAdapter(sqlQuery);
                var ds = new DataSet();
                adapter.Fill(ds);
                Conn.Close();
                return ds;
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while executing query in database.",e);
            }
        }

        public void ExecuteQueryForDb(List<string> queries)
        {
            if (queries == null || queries.Count <= 0) return;

            SqlTransaction trans = null;
            try {
                Conn.Open();
                trans = Conn.BeginTransaction();
                foreach (var commandString in queries)
                {
                    var command = new SqlCommand(commandString, Conn, trans);
                    command.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex) 
            {
                trans?.Rollback();
                throw new Exception("Exception occurred while executing query in database.",ex);
            }
        }

    }
}