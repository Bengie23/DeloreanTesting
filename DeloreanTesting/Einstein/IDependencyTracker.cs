namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// Dictionary for tracking dependencies
    /// </summary>
    public interface IDependencyTracker
    {
        /// <summary>
        /// Adds tracking for a service type and implementation type.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        void TrackDependency(Type serviceType, Type implementationType);

        /// <summary>
        /// Returns tracking data.
        /// </summary>
        /// <returns></returns>
        IReadOnlyDictionary<Type, Type> GetTrackingData();
    }
}
