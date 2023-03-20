using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace DeloreanTesting.Helpers
{
    /// <summary>
    /// Helper for AzureDevops WorkItem objects.
    /// </summary>
    public static class WorkItemHelper
    {
        static readonly string StateFIeld = "System.State";
        
        /// <summary>
        /// Evaluates the state of a given WorkItem
        /// </summary>
        /// <param name="workItem"></param>
        /// <param name="stateToMatch"></param>
        /// <returns></returns>
        public static bool HasState(this WorkItem workItem, string stateToMatch)
        {
            if (workItem is null || !workItem.Fields.Any(x=> x.Key == StateFIeld))
            {
                return false;
            }
            var currentState = workItem.Fields[StateFIeld].ToString();
            return string.Equals(currentState, stateToMatch, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
