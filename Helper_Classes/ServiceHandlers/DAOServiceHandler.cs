//This class is responsible for handling any service request from controller.
//It redirects the request to actual service implementation classes.

//    @author :  Abhishek tripathi
//    @version  :  1.0 


using System;
using System.Collections.Generic;
using System.Data;
using BuilderWebApp3.Helper_Classes.ServiceImplementations;

namespace BuilderWebApp3.Helper_Classes.ServiceHandlers {
    public class DAOServiceHandler {

        private readonly DAOServiceImplementation _daoImpl;

        public DAOServiceHandler() {
            try {
                _daoImpl = new DAOServiceImplementation();
            }
            catch (Exception e) {
                throw new Exception("Exception occurred during initialization/connection to database.",e);
            }
        }

        public void InsertToDb(object obj) {
            try {
                _daoImpl.InsertToDb(obj);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while inserting into database.",e);
            }
        }

        public void InsertSessionEndTimeToDb(string sessionId, DateTime endTime)
        {
            try {
                _daoImpl.InsertSessionEndTimeToDb(sessionId, endTime);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while inserting session end time in database.",e);
            }
        }

        public DataSet ExecuteQueryForDb(string query)
        {
            try {
                return _daoImpl.ExecuteQueryForDb(query);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while executing query in database.",e);
            }
        }

        public void ExecuteQueryForDb(List<string> queries)
        {
            try {
                _daoImpl.ExecuteQueryForDb(queries);
            }
            catch (Exception e) {
                throw new Exception("Exception occurred while executing query in database.", e);
            }
        }

    }
}