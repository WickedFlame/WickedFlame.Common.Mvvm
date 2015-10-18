
namespace wickedflame.common.mvvm.servicelocator
{
    public interface IServiceLocator
    {
        /// <summary>
        /// Gets a service from the service locator
        /// </summary>
        /// <typeparam name="T">The type of service you want to get</typeparam>
        /// <returns>Returns the instance of the service</returns>
        T GetService<T>();

        /// <summary>
        /// gets a service from the servicelocator. 
        /// if the service doesn't exist a new instance is created
        /// </summary>
        /// <typeparam name="T">type of service you want to get</typeparam>
        /// <typeparam name="O">type of instance that will be registered with the type</typeparam>
        /// <returns></returns>
        T GetService<T, O>() where O : class, new();

        /// <summary>
        /// Registers a service to the service locator. This will overwrite any registered services with the same registration type
        /// </summary>
        /// <param name="service">The service to add</param>
        /// <returns>Returns true if the service was successfully registered</returns>
        /// <remarks>
        ///     <para>This generics based implementation ensures that the service must at least inherit from the service type.</para>
        ///     <para>NOTE: the MSDN documentation on IServiceProvidor states that the GetService method returns an object of type servieProvider</para>
        /// </remarks>
        bool RegisterService<T>(T service);
    }
}
