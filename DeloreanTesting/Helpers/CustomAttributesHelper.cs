using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace DeloreanTesting.Helpers
{
    /// <summary>
    /// Helper class for test method attributes
    /// </summary>
    public static class CustomAttributesHelper
    {
        /// <summary>
        /// Attempts to return the given attribute, return default(T) when not found..
        /// </summary>
        /// <typeparam name="T">Attribute to get</typeparam>
        /// <param name="testMethod">Test method</param>
        /// <returns>Given Attribute</returns>
        public static T? TryGetAttribute<T>(this ITestMethod testMethod) where T : Attribute
        {
            var attributes = testMethod.FindAttributes<T>();
            if (attributes.Any() && attributes.First() is T givenAttribute)
            {
                return givenAttribute;
            }

            return default;
        }

        /// <summary>
        /// Returns all occurances of given attribute.
        /// </summary>
        /// <typeparam name="T">Attribute to find</typeparam>
        /// <param name="testMethod">Test method</param>
        /// <returns>Found Attributes</returns>
        public static IEnumerable<T> FindAttributes<T>(this ITestMethod testMethod) where T : Attribute
        {
            // Look for any direct attributes
            var attributes = new List<T>();
            attributes.AddRange(testMethod.GetAttributes<T>(inherit: true));

            // look for the attribute in the hierarchy
            var type = testMethod.MethodInfo.DeclaringType;
            while (type != null)
            {
                attributes.AddRange(type.GetCustomAttributes<T>(inherit: true));
                type = type.DeclaringType;
            }
            return attributes;
        }

        /// <summary>
        /// Attempts to find the definition method (function).
        /// Executes the found definition method.
        /// Returns the results of definition method.
        /// </summary>
        /// <param name="testMethod"></param>
        /// <param name="DefinitionMethodName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool TryExecuteDefinitionMethod(this ITestMethod testMethod, string DefinitionMethodName)
        {
            try
            {
                // Search for the method specified by name in this class or any parent classes.
                var searchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Static;
                var method = testMethod.MethodInfo.DeclaringType.GetMethod(DefinitionMethodName, searchFlags);
                return (bool)method.Invoke(null, null);
            }
            catch (Exception e)
            {
                var message = $"Method {DefinitionMethodName} not found. Ensure the method is in the same class as the test method, marked as `static`, returns a `bool`, and doesn't accept any parameters.";
                throw new ArgumentException(message, e);
            }
        }
    }
}
