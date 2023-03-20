using DeloreanTesting.Processors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.CustomAttributes
{
    /// <summary>
    /// Base class for redirecting existing test method execute behaviour.
    /// </summary>
    public class AbstractDeloreanTestMethodAttribute : TestMethodAttribute, IDeloreanTestMethodAttribute
    {
        ///<inheritdoc cref="IDeloreanTestMethodAttribute.ExecuteAsOriginalTest(ITestMethod)"/>
        public TestResult[] ExecuteAsOriginalTest(ITestMethod testMethod)
        {
            return base.Execute(testMethod);
        }

        ///<inheritdoc cref="TestMethodAttribute.Execute(ITestMethod)"/>
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            return TestMethodExecuteDecorator.Decorate<DefaultTestMethodProcessor>(this, testMethod);
        }

    }
}
