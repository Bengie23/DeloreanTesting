using DeloreanTesting.AzureDevops;
using DeloreanTesting.AzureDevops.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.Copernicus
{
    /// <summary>
    /// Extension of a TestClass Attribute. When applied, all tests under it will get affected by the CopernicusTestAttribute.
    /// </summary>
    public class CopernicusTestClassAttribute : TestClassAttribute
    {
        public CopernicusTestClassAttribute(string orgName, string personalAccessToken, string project) : this(new AzureDevopsServiceConfiguration(orgName, personalAccessToken, project))
        {

        }
        internal CopernicusTestClassAttribute(AzureDevopsServiceConfiguration configuration) => AzureDevopsService.BuildAndGetInstance(configuration);
        public override TestMethodAttribute? GetTestMethodAttribute(TestMethodAttribute? testMethodAttribute)
        {
            if (testMethodAttribute is CopernicusTestMethodAttribute copernicusTestMethodAttribute)
            {
                var test = copernicusTestMethodAttribute.WorkItemId;
                return testMethodAttribute;
            }

            throw new InvalidOperationException("WorkItem Id is required to create a Copernicus test.");
        }
    }
}
