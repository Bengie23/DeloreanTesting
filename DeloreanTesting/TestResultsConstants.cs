using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting
{
    /// <summary>
    /// Describes static values used as returnable test results.
    /// </summary>
    public static class TestResultsConstants
    {
        private static readonly string skipped_message = "Test has been skipped as a result of a Delorean evaluation.";
        public static readonly TestResult[] Skipped = new[]
                {
                    new TestResult
                    {
                        Outcome = UnitTestOutcome.Inconclusive,
                        TestFailureException = new AssertInconclusiveException(skipped_message)
                    }
                };


    }
}
