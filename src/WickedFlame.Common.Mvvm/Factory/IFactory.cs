using System.Windows;

namespace WickedFlame.Common.Mvvm.Factory
{
    public interface IFactory
    {
        object CreateViewModel(DependencyObject sender);
    }
}
