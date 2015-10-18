using System;
using System.Reflection;

namespace WickedFlame.Common.Mvvm.Notification
{
    /// <summary>
    /// This class is an implementation detail of the MessageToActionsMap class.
    /// </summary>
    internal class WeakAction 
    {
        readonly MethodInfo _method;
        readonly Type _delegateType;
        readonly WeakReference _weakRef;

        /// <summary>
        /// Constructs a WeakAction
        /// </summary>
        /// <param name="target">The instance to be stored as a weak reference</param>
        /// <param name="method">The Method Info to create the action for</param>
        /// <param name="parameterType">The type of parameter to be passed in the action. Pass null if there is not a paramater</param>
        internal WeakAction(object target, MethodInfo method, Type parameterType)
        {
            //create a Weakefernce to store the instance of the target in which the Method resides
            _weakRef = new WeakReference(target);

            this._method = method;

			// JAS - Added logic to construct callback type.
			if (parameterType == null)
				this._delegateType = typeof(Action);
			else
				this._delegateType = typeof(Action<>).MakeGenericType(parameterType);
        }

        /// <summary>
        /// Creates a "throw away" delegate to invoke the method on the target
        /// </summary>
        /// <returns></returns>
		internal Delegate CreateAction()
		{
			object target = _weakRef.Target;
			if (target != null)
			{		
				// Rehydrate into a real Action
				// object, so that the method
				// can be invoked on the target.
				return Delegate.CreateDelegate(
							this._delegateType,
							_weakRef.Target,
							_method);
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// returns true if the target is still in memory
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return _weakRef.IsAlive;
            }
        }
    }
}