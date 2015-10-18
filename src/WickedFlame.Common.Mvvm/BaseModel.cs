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
        
        public virtual void Loaded()
        {
        }
        
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

		List<string> _validateProperties;
		private void FillValidateProperties()
		{

			if (_validateProperties != null)
				return;


			//get all properties of the instance (properties of child and base class)
			//var properties = (from p in this.GetType().GetProperties()
			//                  where p.DeclaringType != typeof(ItemBase) && p.Name != "DataValue" && p.Name != "Id"
			//                  select p).ToList();
			var properties = (from p in this.GetType().GetProperties() select p).ToList();

			_validateProperties = (from ia in properties
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
				if (_validateProperties == null)
				{
					return true;
				}
				else
				{
					return !_validateProperties.Any(prop => !string.IsNullOrEmpty(this[prop]));
				}
			}
		}

		#endregion
	}
}
