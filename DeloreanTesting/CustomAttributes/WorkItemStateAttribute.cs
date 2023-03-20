using DeloreanTesting.AzureDevops;
using DeloreanTesting.AzureDevops.DTO;
using DeloreanTesting.Helpers;

namespace DeloreanTesting.CustomAttributes
{
    /// <summary>
    /// Attribute that receives a work item id from Azure Devops.
    /// It will try to fetch the given work Item.
    /// Characteristics of given work item must be evaluated from a different attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WorkItemStateAttribute : Attribute
    {
        private readonly int WorkItemId;
        private readonly string WorkItemState;
        private readonly IAzureDevopsService ADOService;

        public WorkItemHasState Result { get; private set; }

        public WorkItemStateAttribute(int workItemId, string workItemState)
        {
            AzureDevopsService service;
            try
            {
                service = AzureDevopsService.BuildAndGetInstance();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("WorkItemState Attribute required CopernicusTestClass definition", ex);
            }

            this.WorkItemId = workItemId;
            this.WorkItemState = workItemState;
            this.ADOService = service;
            this.Result = new WorkItemHasState(workItemId, workItemState);
        }

        public WorkItemStateAttribute(string workItemId, string workItemState) : this(int.Parse(workItemId), workItemState) { }

        internal bool IsWorkItemStateMatch()
        {
            var workItem = FetchWorkItem();
            return workItem.isMatch;
        }

        private WorkItemHasState FetchWorkItem()
        {
            var workItem = ADOService.GetWorkItem(this.WorkItemId);
            if (workItem == null)
            {
                throw new InvalidOperationException($"Unable to retrieve workItem {this.WorkItemId}");
            }

            this.Result = this.Result with
            {
                isMatch = workItem.HasState(this.WorkItemState)
            };

            return this.Result;
        }
    }
}
