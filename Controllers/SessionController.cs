using System;
using System.Web.Mvc;
using BuilderWebApp3.Helper_Classes.ServiceHandlers;
using BuilderWebApp3.Models;

namespace BuilderWebApp3.Controllers
{
    public class SessionController : Controller
    {
        private readonly DAOServiceHandler _daoHandler;

        public SessionController()
        {
            _daoHandler = new DAOServiceHandler();
        }

        // Session/Add
        public void AddSessionInfo(string sessionId, string startTime, string location,string builderProjectId)
        {
            var startDate = DateTime.Parse(startTime);
            var clientLoc = location.Split(',');
            var session = new Session(sessionId,startDate,clientLoc[0],float.Parse(clientLoc[1]),float.Parse(clientLoc[2]),builderProjectId); 
            _daoHandler.InsertToDb(session);
        }

        // Session/AddSessionEndTime
        public void AddSessionEndTime(string sessionId, string endTime)
        {
            _daoHandler.InsertSessionEndTimeToDb(sessionId, DateTime.Parse(endTime));
        }
    }
}