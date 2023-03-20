using DeloreanTesting.CustomAttributes;
using DeloreanTesting.Einstein;
using DeloreanTesting.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.Processors
{
    internal class DefaultTestMethodProcessor : AbstractTestMethodProcessor
    {
        private ShouldSkipAttribute? shouldSkipAttribute;
        private WorkItemStateAttribute? workItemHasStateAttribute;
        private DependenciesUnderTestAttribute? registeredDependenciesAttribute;

        public DefaultTestMethodProcessor(IDeloreanTestMethodAttribute testMethodAttribute, ITestMethod testMethod) : base(testMethodAttribute, testMethod)
        {
        }

        public override void After()
        {
            //Don't do anything yet
            
        }

        public override void Before()
        {
            //Set Shouldskip up
            var shouldSkipAttribute = this.testMethod.TryGetAttribute<ShouldSkipAttribute>();
            if (shouldSkipAttribute != null) { this.shouldSkipAttribute = shouldSkipAttribute; }

            //Set Workitemhasstate up
            var workItemHasStateAttribute = this.testMethod.TryGetAttribute<WorkItemStateAttribute>();
            if (workItemHasStateAttribute != null) { this.workItemHasStateAttribute = workItemHasStateAttribute; }

            //Set RegisteredDependencies up
            var registeredDependenciesAttribute = this.testMethod.TryGetAttribute<DependenciesUnderTestAttribute>();
            if (registeredDependenciesAttribute != null) { this.registeredDependenciesAttribute = registeredDependenciesAttribute; }
        }

        public override bool ShouldSkip()
        {
            if (this.shouldSkipAttribute != null)
            {
                return this.shouldSkipAttribute.ShouldSkip(this.testMethod);
            }

            return base.ShouldSkip();
        }

        public override bool ShouldRun()
        {
            if (this.workItemHasStateAttribute != null)
            {
                return this.workItemHasStateAttribute.IsWorkItemStateMatch();
            }
            return base.ShouldRun();
        }
    }
}
