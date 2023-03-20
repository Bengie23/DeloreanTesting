using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Reflection;

namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// Attribute to specify a set of dependencies to assert against within the given test.
    /// </summary>
    public class DependenciesUnderTestAttribute : DataTestMethodAttribute, ITestDataSource
    {
        public readonly IReadOnlyCollection<string> dependencies;

        public DependenciesUnderTestAttribute(params Type[] dependencies) : this(dependencies.Select(dep => dep.Name).ToArray())
        {
        }

        public DependenciesUnderTestAttribute(params string[] dependencies) : base()
        {
            List<string> data = new List<string>();
            foreach (var item in dependencies)
            {
                if (item != null)
                {
                    data.Add(item);
                }
            }
            this.dependencies = new ReadOnlyCollection<string>(data);
            TrackeableDependenciesDictionary.StoreDependenciesUnderTest(this.dependencies);
        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            string[] data = dependencies.ToArray();
            yield return new object[] { data };
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            return methodInfo.Name;
        }
    }
}
