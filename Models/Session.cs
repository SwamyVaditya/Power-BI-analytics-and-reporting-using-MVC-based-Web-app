using System;

namespace BuilderWebApp3.Models {
    public class Session {

        public Session(string sessionId, DateTime startTime, string location, float latitude, float longitude, string linkId) {
            SessionId = sessionId;
            StartTime = startTime;
            Location = location;
            Latitude = latitude;
            Longitude = longitude;
            LinkId = linkId;
        }

        public string SessionId { get; }
        public DateTime StartTime { get; }
        public string Location { get; }
        public float Latitude { get; }
        public float Longitude { get; }
        public string LinkId { get; }
    }
}