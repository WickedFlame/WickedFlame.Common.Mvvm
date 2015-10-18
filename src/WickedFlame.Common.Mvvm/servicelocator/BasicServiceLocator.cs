using System;
using System.Collections.Generic;

namespace wickedflame.common.mvvm.servicelocator
{
    public class BasicServiceLocator : IServiceLocator, IServiceProvider
    {
        Dictionary<Type, object> services = new Dictionary<Type, object>();

        #region IServiceProvider Members

        /// <summary>
        /// Gets a service from the service locator
        /// </summary>
        /// <typeparam name="T">The type of service you want to get</typeparam>
        /// <returns>Returns the instance of the service</returns>
        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        /// <summary>
        /// gets a service from the servicelocator. 
        /// if the service doesn't exist a new instance is created
        /// </summary>
        /// <typeparam name="T">type of service you want to get</typeparam>
        /// <typeparam name="O">type of instance that will be registered with the type</typeparam>
        /// <returns></returns>
        public T GetService<T, O>() where O : class, new()
        {
            if (!services.ContainsKey(typeof(T)))
            {
                services.Add(typeof(T), new O());
            }

            return GetService<T>();
        }

        /// <summary>
        /// Gets a service from the service locator
        /// </summary>
        /// <param name="serviceType">The type of service you want to get</param>
        /// <returns>Returns the instance of the service</returns>
        /// <remarks>This implements IServiceProvider</remarks>
        public object GetService(Type serviceType)
        {
            // look for local service registrations
            if (services.ContainsKey(serviceType))
            {
                return services[serviceType];
            }

            return null;
        }

        /// <summary>
        /// Registers a service to the service locator
        /// </summary>
        /// <param name="service">The service to add</param>
        /// <param name="overwriteIfExists">Passing true will replace any existing service</param>
        /// <returns>Returns true if the service was successfully registered</returns>
        /// <remarks>
        ///     <para>This generics based implementation ensures that the service must at least inherit from the service type.</para>
        ///     <para>NOTE: the MSDN documentation on IServiceProvidor states that the GetService method returns an object of type servieProvider</para>
        /// </remarks>
        public bool RegisterService<T>(T service, bool overwriteIfExists)
        {
            if (!services.ContainsKey(typeof (T)))
            {
                services.Add(typeof (T), service);
                return true;
            }

            if (overwriteIfExists)
            {
                services[typeof (T)] = service;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Registers a service to the service locator. This will overwrite any registered services with the same registration type
        /// </summary>
        /// <param name="service">The service to add</param>
        /// <returns>Returns true if the service was successfully registered</returns>
        /// <remarks>
        ///     <para>This generics based implementation ensures that the service must at least inherit from the service type.</para>
        ///     <para>NOTE: the MSDN documentation on IServiceProvidor states that the GetService method returns an object of type servieProvider</para>
        /// </remarks>
        public bool RegisterService<T>(T service)
        {
            return RegisterService<T>(service, true);
        }

        #endregion
    }
}
