using Microsoft.TeamFoundation.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// Work in progress
    /// </summary>
    public static class CustomAssertions
    {
        public static void DependencyHasBeenRegistered(this Assert assert, string name)
        {
            Assert.IsTrue(TrackeableDependenciesDictionary.HasBeenRegistered(name));
        }

        public static void DependenciesHaveBeenRegistered(this Assert assert, params string[] dependencies) 
        {
            Assert.IsTrue(dependencies.Any());
            foreach (var item in dependencies)
            {
                Assert.IsFalse(item.IsNullOrEmpty());
                assert.DependencyHasBeenRegistered(item);
            }
        }
    }
}
