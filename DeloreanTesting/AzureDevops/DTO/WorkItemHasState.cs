using DeloreanTesting.Helpers;

namespace DeloreanTesting.AzureDevops.DTO
{
    /// <summary>
    /// DTO for Azure Devops work Item with a given state
    /// </summary>
    /// <param name="Id"></param>
    public record WorkItemHasState(int Id, string StateToMatch)
    {
        internal int Id { get; set; } = Id;
        internal string StateToMatch { get; set; } = StateToMatch;
        public bool isMatch { get; set; } = false;
        
    }
}
