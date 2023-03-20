using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloreanTesting.Delorean
{
    /// <summary>
    /// Extension of a TestClass Attribute. When applied, all tests under it will get affected by the DeloreanTestAttribute.
    /// </summary>
    public class DeloreanTestClassAttribute : TestClassAttribute
    {
        public override TestMethodAttribute? GetTestMethodAttribute(TestMethodAttribute? testMethodAttribute)
        {
            if (testMethodAttribute is DeloreanTestMethodAttribute)
            {
                return testMethodAttribute;
            }
            return new DeloreanTestMethodAttribute();
        }
        
    }
}
