using System.Collections.Concurrent;

namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// Dictionary for tracking dependencies.
    /// </summary>
    public class DependencyTracker : IDependencyTracker
    {
        protected ConcurrentDictionary<Type, Type> tracking = new ConcurrentDictionary<Type, Type>();

        ///<inheritdoc cref="IDependencyTracker.GetTrackingData"/>
        public IReadOnlyDictionary<Type, Type> GetTrackingData()
        {
            return tracking.AsReadOnly();
        }

        ///<inheritdoc cref="IDependencyTracker.TrackDependency(Type, Type)"/>
        public void TrackDependency(Type dependency, Type implementation)
        {
            if (implementation is null)
            {
                System.Diagnostics.Debug.WriteLine($"unable to track dependency {dependency.Name}");
                return;
            }
            try
            {
                tracking.TryAdd(implementation, dependency);
            }
            catch (Exception e)
            {
                var test = e.Message;
                throw;
            }
        }
    }
}
