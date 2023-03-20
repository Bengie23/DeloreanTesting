using DeloreanTesting.Copernicus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeloreanTesting.Helpers;

namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// Extension of a TestClass Attribute. When applied, all tests unders it will get affected by EinsteinsTestAttribute
    /// </summary>
    public class EinsteinTestClassAttribute : TestClassAttribute
    {
        public override TestMethodAttribute? GetTestMethodAttribute(TestMethodAttribute? testMethodAttribute)
        {
            if (testMethodAttribute is EinsteinTestMethodAttribute)
            {
                //var registeredDependenciesAttribute = testMethodAttribute.TryGetAttribute<RegisteredDependenciesTestMethodAttribute>();
                return testMethodAttribute;
            }

            return new EinsteinTestMethodAttribute();
        }
    }
}
