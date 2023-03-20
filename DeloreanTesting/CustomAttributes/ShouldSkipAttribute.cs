using DeloreanTesting.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.CustomAttributes
{
    /// <summary>
    /// Attribute that checks for a should skip input.
    /// Input can be a reference to a string method definition or a bool value itself.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ShouldSkipAttribute : Attribute
    {
        private bool ShouldSkipValue;

        private readonly string? ShouldSkipDefinitionMethodName;

        public ShouldSkipAttribute(string shouldSkipDefinitionMethoName)
        {
            ShouldSkipDefinitionMethodName = shouldSkipDefinitionMethoName ?? throw new ArgumentNullException(nameof(shouldSkipDefinitionMethoName));
        }

        public ShouldSkipAttribute(bool shouldSkipValue = false)
        {
            ShouldSkipValue = shouldSkipValue;
        }

        /// <summary>
        /// Performs the operation described in the Should Skip Attribute to evaluate if the test should be run.
        /// </summary>
        /// <param name="testMethod"></param>
        /// <returns></returns>
        internal bool ShouldSkip(ITestMethod testMethod)
        {
            if (ShouldSkipDefinitionMethodName is not null)
            {
                ShouldSkipValue = testMethod.TryExecuteDefinitionMethod(ShouldSkipDefinitionMethodName);
            }

            return ShouldSkipValue;
        }

        
    }
}
