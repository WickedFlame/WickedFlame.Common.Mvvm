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
            
            Mediator.Register(this);
        }
        
        public virtual void Loaded()
        {
        }

		public virtual void Unload()
		{
		}
        
		#region Mediator

		static readonly Mediator _mediator = new Mediator();

		public Mediator Mediator
		{
			get { return _mediator; }
		}

		#endregion
    }
}
