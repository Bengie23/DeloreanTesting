using DeloreanTesting.Copernicus;
using DeloreanTesting.CustomAttributes;
using DeloreanTesting.Delorean;

namespace DeloreanTesting.Samples
{
    [DeloreanTestClass]
    public class ShouldSkipTests
    {
        private static bool ShouldSkipDefinitionMethod()
        {
            return true;
        }

        [TestMethod]
        [ShouldSkip(true)]
        public void TestMethod1()
        {
        }

        [DeloreanTestMethod]
        [ShouldSkip(nameof(ShouldSkipDefinitionMethod))]
        public void TestMethod2()
        {

        }
    }
}