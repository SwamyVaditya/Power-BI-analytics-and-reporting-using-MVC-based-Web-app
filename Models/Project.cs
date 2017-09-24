namespace BuilderWebApp3.Models {
    public class Project {

        public Project(string projectName, string projectId){
            ProjectName = projectName;
            ProjectId = projectId;
        }

        public string ProjectName { get; }
        public string ProjectId { get;}
        public string PbiReportName { get; set; }

    }
}