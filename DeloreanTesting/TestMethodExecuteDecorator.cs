using DeloreanTesting.CustomAttributes;
using DeloreanTesting.Processors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting
{
    /// <summary>
    /// Builder class that decorates the existing TestMethod and TestMethodAttribute.
    /// </summary>
    public static class TestMethodExecuteDecorator
    {
        public static TestResult[] Decorate<T>(IDeloreanTestMethodAttribute testMethodAttribute, ITestMethod testMethod) where T : ITestMethodProcessor
        {
            //var instance = new DefaultTestMethodProcessor(testMethodAttribute, testMethod);
            var instance = BuildProcessor<T>(new object[] { testMethodAttribute, testMethod });
            instance.Execute();
            return instance.TryGetTestResults();
        }

        public static T BuildProcessor<T>(params object[] args) where T : ITestMethodProcessor
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
