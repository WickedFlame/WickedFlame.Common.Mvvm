using System.ComponentModel;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WickedFlame.Common.Mvvm.Common
{
    /// <summary>
    /// Base class that implements the INotifyPropertyChanged interface for databinding
    /// </summary>
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T1>(Expression<Func<T1>> propertyExpression)
        {
            OnPropertyChanged(PropertyValue.ExtractPropertyName<T1>(propertyExpression));
        }

        #endregion
    }
}
