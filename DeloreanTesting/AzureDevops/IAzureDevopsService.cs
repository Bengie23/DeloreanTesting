using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace DeloreanTesting.AzureDevops
{
    /// <summary>
    /// Anti-Corruption for AzureDevops Systems communications.
    /// </summary>
    public interface IAzureDevopsService
    {
        /// <summary>
        /// Attempts to return a work item
        /// </summary>
        /// <param name="id">work item id</param>
        /// <returns>local Work Item record</returns>
        WorkItem GetWorkItem(int id);
    }
}
