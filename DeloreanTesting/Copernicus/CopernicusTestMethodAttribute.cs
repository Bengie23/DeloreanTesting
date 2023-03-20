using DeloreanTesting.CustomAttributes;

namespace DeloreanTesting.Copernicus
{
    /// <summary>
    /// Attribute to mark a test method as a Copernicus test method.
    /// </summary>
    public class CopernicusTestMethodAttribute : AbstractDeloreanTestMethodAttribute
    {
        public int WorkItemId { get; set; }
        public CopernicusTestMethodAttribute(int workItemId )
        {
            this.WorkItemId = workItemId;
        }
    }
}
