namespace BuilderWebApp3.Helper_Classes.PBIClasses {
    public class PBIElement {
    }

    public class Dataset
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Dashboard
    {
        public string id { get; set; }
        public string displayName { get; set; }
    }

    public class Tile
    {
        public string id { get; set; }
        public string title { get; set; }
        public string subTitle { get; set; }
        public string embedUrl { get; set; }
    }

    public class PbiReport {
        public string id { get; set; }
        // the name of this property will change to 'displayName' when the API moves from Beta to V1 namespace
        public string name { get; set; }
        public string webUrl { get; set; }
        public string embedUrl { get; set; }
    }

}