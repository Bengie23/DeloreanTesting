using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.Processors
{
    /// <summary>
    /// Contract to extend the current behaviour of a test method.
    /// A TestMethodProcessor will handle IDeloreanTestMethodAttribute objects by extending their nature with the described in this contract.
    /// </summary>
    public interface ITestMethodProcessor
    {
        /// <summary>
        /// Operations before running the test
        /// </summary>
        void Before();

        /// <summary>
        /// Operations after running the test
        /// </summary>
        void After();

        /// <summary>
        /// Indicates whether or not a test should be run.
        /// </summary>
        /// <returns></returns>
        bool ShouldRun();

        /// <summary>
        /// Indicates whether or not a test should be skipped.
        /// </summary>
        /// <returns></returns>
        bool ShouldSkip();

        /// <summary>
        /// Decorated execution of a test method.
        /// </summary>
        void Execute();

        /// <summary>
        /// Returns Test Results of current TestMethod
        /// </summary>
        /// <returns></returns>
        TestResult[] TryGetTestResults();
    }
}
