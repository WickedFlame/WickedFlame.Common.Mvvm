using System;
using System.Collections.Generic;

namespace wickedflame.common.mvvm.servicelocator
{
    /// <summary>
    /// Implements a simple service locator
    /// </summary>
    public class ServiceLocator : IServiceProvider, IDisposable
    {
		Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static ServiceLocator GetInstance()
        {
            return new ServiceLocator();
        }

		#region IServiceProvider Members

		/// <summary>
		/// Gets a service from the service locator
		/// </summary>
		/// <typeparam name="T">The type of service you want to get</typeparam>
		/// <returns>Returns the instance of the service</returns>
		public T GetService<T>()
		{
			var service = (T)GetService(typeof(T));
            
            if (service == null)
            {
                // use mef to try to find the service
                var sl = new CompositionLocator<T>();
                service = sl.GetService<T>();
            }

		    return service;
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
            O service = null;

            // load existing services
            if (services.ContainsKey(typeof(T)) && !ConstantLocator.Contains(typeof(T)))
                service = GetService<T>() as O;

            if (service == null)
            {
                // cleare existing services if a service with the same key exists
                if (services.ContainsKey(typeof(T)))
                    services.Remove(typeof(T));

                // try loading using MEF
                var sl = new CompositionLocator<T>();
                service = sl.GetService<T, O>() as O;
                if (service != null)
                    services.Add(typeof(T), service);
            }

            if (service == null)
            {
                // create using construction
                service = new O();
                services.Add(typeof(T), service);
            }

            return (T)(object)service;
        }

        /// <summary>
        /// Gets a service from the service locator
        /// </summary>
        /// <param name="serviceType">The type of service you want to get</param>
        /// <returns>Returns the instance of the service</returns>
        /// <remarks>This implements IServiceProvider</remarks>
        public object GetService(Type serviceType)
        {
            lock (ConstantLocator.Locker)
            {
                // look for local service registrations
                if (services.ContainsKey(serviceType))
                {
                    return services[serviceType];
                }

                // look for static service registrations
                if (ConstantLocator.Contains(serviceType))
                {
                    return ConstantLocator.GetService(serviceType);
                }
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
            lock (ConstantLocator.Locker)
			{
				if (!services.ContainsKey(typeof(T)))
				{
					services.Add(typeof(T), service);
					return true;
				}
				
                if (overwriteIfExists)
				{
					services[typeof(T)] = service;
					return true;
				}
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

        #region IDisposeable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
