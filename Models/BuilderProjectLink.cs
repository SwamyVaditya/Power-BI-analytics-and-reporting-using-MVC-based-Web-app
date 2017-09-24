namespace BuilderWebApp3.Models {
    public class BuilderProjectLink {

        public BuilderProjectLink(string linkId, string builderId, string projectId)
        {
            LinkId = linkId;
            BuilderId = builderId;
            ProjectId = projectId;
        }

        public string LinkId { get; }
        public string BuilderId { get; }
        public string ProjectId { get; }

    }
}