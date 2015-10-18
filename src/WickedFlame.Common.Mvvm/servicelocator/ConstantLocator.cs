using System;
using System.Collections.Generic;

namespace wickedflame.common.mvvm.servicelocator
{
    public class ConstantLocator : IServiceLocator, IServiceProvider
    {
        /// <summary>
        /// private ctx to prevent instantiation
        /// </summary>
        private ConstantLocator()
        {
        }

        internal static object Locker = new object();

        static Dictionary<Type, object> _services;
        static Dictionary<Type, object> Services
        {
            get
            {
                if (_services == null)
                    _services = new Dictionary<Type, object>();
                return _services;
            }
        }

        internal static bool Contains(Type type)
        {
            return Services.ContainsKey(type);
        }

        public static bool RegisterService<T>(T service)
        {
            return RegisterService<T>(service, true);
        }

        public static bool RegisterService<T>(T service, bool overwriteIfExists)
        {
            lock (Locker)
            {
                if (!Services.ContainsKey(typeof(T)))
                {
                    Services.Add(typeof(T), service);
                    return true;
                }

                if (overwriteIfExists)
                {
                    Services[typeof(T)] = service;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets a service from the service locator
        /// </summary>
        /// <typeparam name="T">The type of service you want to get</typeparam>
        /// <returns>Returns the instance of the service</returns>
        public static T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        /// <summary>
        /// gets a service from the global servicelocator. 
        /// if the service doesn't exist a new instance is created
        /// </summary>
        /// <typeparam name="T">type of service you want to get</typeparam>
        /// <typeparam name="O">type of instance that will be registered with the type</typeparam>
        /// <returns></returns>
        public static T GetService<T, O>() where O : class, new()
        {
            lock (Locker)
            {
                if (!Services.ContainsKey(typeof(T)))
                {
                    Services.Add(typeof(T), new O());
                }
            }

            return GetService<T>();
        }

        public static object GetService(Type serviceType)
        {
            //lock (services)
            lock (Locker)
            {
                if (Services.ContainsKey(serviceType))
                {
                    return Services[serviceType];
                }
            }
            return null;
        }

        #region IServiceProvider Members

        object IServiceProvider.GetService(Type serviceType)
        {
            return GetService(serviceType);
        }

        #endregion

        #region IServiceLocator Members

        T IServiceLocator.GetService<T>()
        {
            return GetService<T>();
        }

        T IServiceLocator.GetService<T, O>()
        {
            return GetService<T, O>();
        }

        bool IServiceLocator.RegisterService<T>(T service)
        {
            return RegisterService<T>(service);
        }

        #endregion
    }
}
