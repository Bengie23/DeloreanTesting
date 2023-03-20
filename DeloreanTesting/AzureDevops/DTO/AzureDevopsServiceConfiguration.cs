namespace DeloreanTesting.AzureDevops.DTO
{
    public record AzureDevopsServiceConfiguration
    {
        public AzureDevopsServiceConfiguration(string orgName, string personalAccessToken, string project)
        {
            BaseUri = "https://dev.azure.com/";
            OrgName = orgName;
            Project = project;
            PersonalAccessToken = personalAccessToken;
        }
        public string BaseUri { get; set; }

        public string OrgName { get; set; }

        public string Project { get; set; }

        public string PersonalAccessToken { get; set; }
    }
}
