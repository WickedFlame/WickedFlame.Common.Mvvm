using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WickedFlame.Common.Mvvm.Common;
//using WickedFlame.Common.Mvvm.servicelocator;

namespace WickedFlame.Common.Mvvm
{
	public class BaseModel : NotifyPropertyChangedBase, IDataErrorInfo
	{
        public BaseModel() { }

        //public BaseModel(DependencyObject parent)
        //{
        //    Parent = parent;
        //}

        public virtual void Loaded()
        {
        }

        //public DependencyObject Parent { get; private set; }

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

	    #region IDataErrorInfo Members

		/// <summary>
		/// Gets an error message indicating what is wrong with this object.
		/// </summary>
		/// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
		public string Error { get; private set; }

		/// <summary>
		/// Gets the error message for the property with the given name.
		/// </summary>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		public string this[string property]
		{
			get
			{
				string msg = !string.IsNullOrEmpty(property) ? IsPropertyValid(property) : string.Empty;
				if (string.IsNullOrEmpty(msg))
				{
					this.Error = null;
				}
				else
				{
					this.Error = property;  //Keep track of the most recently validated property that is invalid.
				}
				return msg;
			}
		}

		/// <summary>
		/// Determines whether the specified property is valid.
		/// </summary>
		/// <param name="property">The name of the property.</param>
		/// <returns>if not valid the error message, otherwise null.</returns>
		protected virtual string IsPropertyValid(string property)
		{
			return null;
		}

		List<string> validateProperties;
		private void FillValidateProperties()
		{

			if (validateProperties != null)
				return;


			//get all properties of the instance (properties of child and base class)
			//var properties = (from p in this.GetType().GetProperties()
			//                  where p.DeclaringType != typeof(ItemBase) && p.Name != "DataValue" && p.Name != "Id"
			//                  select p).ToList();
			var properties = (from p in this.GetType().GetProperties() select p).ToList();

			validateProperties = (from ia in properties
								  orderby ia.Name
								  select ia.Name).ToList();
		}

		/// <summary>
		/// Determines whether this instance is valid.
		/// Properties of this instance (child and base class) will be verified.
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
		/// </returns>
		public bool IsValid
		{
			get
			{
				this.FillValidateProperties();
				if (validateProperties == null)
				{
					return true;
				}
				else
				{
					return !validateProperties.Any(prop => !string.IsNullOrEmpty(this[prop]));
				}
			}
		}

		#endregion
	}
}
