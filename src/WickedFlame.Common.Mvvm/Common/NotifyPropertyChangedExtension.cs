using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace WickedFlame.Common.Mvvm.Common
{
    /// <summary>
    /// Extension for registering to objects that implement INotifyPropertyChanged interface.
    /// </summary>
    /// <example>
    /// Element.CurrentItem.OnNotifyPropertyChanged(() => this.Entity.CurrentFormation.FormationUID, this.OnFormationChanged)
    /// </example>
    public static class NotifyPropertyChangedExtension
    {
        /// <summary>
        /// Extension for registering to objects that implement INotifyPropertyChanged interface.
        /// Usage:
        /// Element.CurrentItem.OnNotifyPropertyChanged(() => this.Entity.CurrentFormation.FormationUID, this.OnFormationChanged)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="propertyName"></param>
        /// <param name="action"></param>
        public static void OnNotifyPropertyChanged<T>(this INotifyPropertyChanged target, Expression<Func<T>> propertyName, Action action)
        {
            if (target == null)
            {
                return;
            }

            target.PropertyChanged += (sender, args) =>
            {
                if (PropertyValue.ExtractPropertyName(propertyName) == args.PropertyName)
                {
                    action();
                }
            };
        }
    }
}
