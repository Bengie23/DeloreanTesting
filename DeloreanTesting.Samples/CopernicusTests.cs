using DeloreanTesting.Copernicus;
using DeloreanTesting.CustomAttributes;

namespace DeloreanTesting.Samples
{
    [CopernicusTestClass("<orgName>", "<PersonalAccessToken>", "<Project>")]
    public class CopernicusTests
    {
        [CopernicusTestMethod(15)]
        [WorkItemState(15,"Doing")]
        public void TestMethod1()
        {
            //Should Run
        }

        [CopernicusTestMethod(15)]
        [WorkItemState(15, "doing")]
        public void TestMethod2()
        {
            //Should Run
        }

        [CopernicusTestMethod(15)]
        [WorkItemState(15, "Done")]
        public void TestMethod3()
        {
            //Should NOT Run
        }
    }
}
