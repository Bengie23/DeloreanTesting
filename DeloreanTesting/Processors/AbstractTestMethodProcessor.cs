using DeloreanTesting.CustomAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.Processors
{
    /// <summary>
    /// Base class for processing IDeloreanTestMethodAttribute objects.
    /// </summary>
    public abstract class AbstractTestMethodProcessor : ITestMethodProcessor
    {
        protected readonly ITestMethod testMethod;
        protected readonly IDeloreanTestMethodAttribute testMethodAttribute;

        private TestResult[] TestResults { get; set; } = Array.Empty<TestResult>();

        public AbstractTestMethodProcessor(IDeloreanTestMethodAttribute testMethodAttribute, ITestMethod testMethod)
        {
            this.testMethod = testMethod;
            this.testMethodAttribute = testMethodAttribute;
        }

        ///<inheritdoc cref="ITestMethodProcessor.After"/>
        public abstract void After();

        ///<inheritdoc cref="ITestMethodProcessor.Before"/>
        public abstract void Before();

        ///<inheritdoc cref="ITestMethodProcessor.ShouldRun"/>
        public virtual bool ShouldRun() => true;

        ///<inheritdoc cref="ITestMethodProcessor.ShouldSkip"/>
        public virtual bool ShouldSkip() => false;

        ///<inheritdoc cref="ITestMethodProcessor.Execute"/>
        public void Execute()
        {
            Before();

            if (ShouldSkip() || !ShouldRun())
            {
                TestResults = TestResultsConstants.Skipped;
                return;

            }

            TestResults = testMethodAttribute.ExecuteAsOriginalTest(testMethod);

            After();
        }

        ///<inheritdoc cref="ITestMethodProcessor.TryGetTestResults"/>
        public TestResult[] TryGetTestResults()
        {
            if (TestResults == Array.Empty<TestResult>())
            {
                throw new InvalidOperationException("Unable to retrieve test results before test execution");
            }

            return TestResults;
        }
    }
}
