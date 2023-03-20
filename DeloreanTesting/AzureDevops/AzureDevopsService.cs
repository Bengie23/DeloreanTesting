using DeloreanTesting.AzureDevops.DTO;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;

namespace DeloreanTesting.AzureDevops
{
    /// <summary>
    /// Implementation of IAzureDevopsService Facade
    /// </summary>
    public sealed class AzureDevopsService : IAzureDevopsService
    {
        private readonly Uri uri;
        private readonly string personalAccessToken;
        private readonly string project;

        private static AzureDevopsService instance = null;
        private static readonly object padlock = new();

        public static AzureDevopsService BuildAndGetInstance(AzureDevopsServiceConfiguration config = null)
        {
            if (config is null && instance is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new AzureDevopsService(config);
                }
                return instance;
            }
        }

        internal AzureDevopsService(AzureDevopsServiceConfiguration config)
        {
            if (config is null)
            {
                throw new ArgumentNullException("missing configuration");
            }

            uri = new Uri("https://dev.azure.com/" + config.OrgName);
            this.project = config.Project;
            this.personalAccessToken = config.PersonalAccessToken;
        }

        ///<inheritdoc cref="IAzureDevopsService.GetWorkItem(int)"/>
        public WorkItem GetWorkItem(int id)
        {
            WorkItem workItem = null;
            try
            {
                workItem = GetWorkItemAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception) { }

            return workItem;
        }

        private async Task<WorkItem> GetWorkItemAsync(int id)
        {
            var credentials = new VssBasicCredential(string.Empty, personalAccessToken);
            using (var httpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                // build a list of the fields we want to see
                var fields = new[] { "System.Id", "System.Title", "System.State" };

                // get work items for the ids found in query
                //return await httpClient.GetWorkItemsAsync(ids, fields, result.AsOf).ConfigureAwait(false);
                return await httpClient.GetWorkItemAsync(id, fields);
            }
        }

        private async Task<IList<WorkItem>> QueryOpenWorkItems()
        {
            var credentials = new VssBasicCredential(string.Empty, personalAccessToken);

            // create a wiql object and build our query
            var wiql = new Wiql()
            {
                // NOTE: Even if other columns are specified, only the ID & URL are available in the WorkItemReference
                Query = "Select [Id] " +
                        "From WorkItems " +
                        "Where [System.TeamProject] = '" + project + "' " +
                        "And [System.State] <> 'Closed' " +
                        "Order By [State] Asc, [Changed Date] Desc",
            };

            // create instance of work item tracking http client
            using (var httpClient = new WorkItemTrackingHttpClient(uri, credentials))
            {
                // execute the query to get the list of work items in the results
                var result = await httpClient.QueryByWiqlAsync(wiql).ConfigureAwait(false);
                var ids = result.WorkItems.Select(item => item.Id).ToArray();

                // some error handling
                if (ids.Length == 0)
                {
                    return Array.Empty<WorkItem>();
                }

                // build a list of the fields we want to see
                var fields = new[] { "System.Id", "System.Title", "System.State" };

                // get work items for the ids found in query
                return await httpClient.GetWorkItemsAsync(ids, fields, result.AsOf).ConfigureAwait(false);
            }
        }
    }
}
