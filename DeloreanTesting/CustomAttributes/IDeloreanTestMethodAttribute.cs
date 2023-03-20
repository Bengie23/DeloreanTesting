using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.CustomAttributes
{
    /// <summary>
    /// Contract for redirecting the current behaviour of a test method execute.
    /// </summary>
    public interface IDeloreanTestMethodAttribute
    {
        /// <summary>
        /// Executes the original test of a test method.
        /// </summary>
        /// <param name="testMethod">test method to execute</param>
        /// <returns>Test results </returns>
        TestResult[] ExecuteAsOriginalTest(ITestMethod testMethod);
    }
}
