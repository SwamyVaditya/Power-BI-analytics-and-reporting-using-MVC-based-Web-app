namespace BuilderWebApp3.Models {
    public class Builder {

        public Builder(string builderId, string userName, string password, string emailId, string contact, string city, string state, string country,string name,string org)
        {
            BuilderId = builderId;
            UserName = userName;
            Password = password;
            EmailId = emailId;
            Contact = contact;
            City = city;
            State = state;
            Country = country;
            Name = name;
            Organisation = org;
        }

        public string BuilderId { get; }
        public string UserName { get; }
        public string Password { get; }
        public string EmailId { get; }
        public string Contact { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string Name { get; set; }
        public string Organisation { get; set; }
    }
}