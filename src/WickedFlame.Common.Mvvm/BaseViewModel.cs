using System.Windows;
using WickedFlame.Common.Mvvm.Common;
using WickedFlame.Common.Mvvm.Notification;
//using WickedFlame.Common.Mvvm.servicelocator;

namespace WickedFlame.Common.Mvvm
{
    /// <summary>
    /// Base class for all view models
    /// </summary>
    public class BaseViewModel : NotifyPropertyChangedBase
    {
        public BaseViewModel()
        {
            if (Designer.IsDesignMode)
                return;

            //Register all decorated methods to the Mediator
            Mediator.Register(this);
        }

        //public BaseViewModel()
        //    : this(null)
        //{
        //}

        //public BaseViewModel(DependencyObject parent)
        //{
        //    if (Designer.IsDesignMode)
        //        return;

        //    Parent = parent;

        //    //Register all decorated methods to the Mediator
        //    Mediator.Register(this);
        //}

        //public DependencyObject Parent
        //{
        //    get;
        //    private set;
        //}

        public virtual void Loaded()
        {
        }

		public virtual void Unload()
		{
		}

        ///// <summary>
        ///// resolves an instance using InjectionMap
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //protected T GetInstance<T>()
        //{
        //    using (var resolver = new InjectionMap.InjectionResolver())
        //    {
        //        return resolver.Resolve<T>();
        //    }
        //}

    	#region ServiceLocator

        //readonly ServiceLocator _serviceLocator = new ServiceLocator();

        ///// <summary>
        ///// Gets the service locator 
        ///// </summary>
        //public ServiceLocator ServiceLocator
        //{
        //    get 
        //    {
        //        return _serviceLocator; 
        //    }
        //}

        ///// <summary>
        ///// Gets a service from the service locator
        ///// </summary>
        ///// <typeparam name="T">The type of service to return</typeparam>
        ///// <returns>Returns a service that was registered with the Type T</returns>
        //public T GetService<T>()
        //{
        //    return _serviceLocator.GetService<T>();
        //}

        ///// <summary>
        ///// gets a service from the servicelocator. 
        ///// if the service doesn't exist a new instance is created
        ///// </summary>
        ///// <typeparam name="T">type of service you want to get</typeparam>
        ///// <typeparam name="O">type of instance that will be registered with the type</typeparam>
        ///// <returns></returns>
        //public T GetService<T, O>() where O : class, new()
        //{
        //    return _serviceLocator.GetService<T, O>();
        //}

        #endregion

		#region Mediator

		static readonly Mediator mediator = new Mediator();

		public Mediator Mediator
		{
			get { return mediator; }
		}

		#endregion
    }
}
