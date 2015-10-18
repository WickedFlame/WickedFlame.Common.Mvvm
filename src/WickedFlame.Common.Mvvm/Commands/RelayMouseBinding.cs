using System.Windows.Input;
using System.Windows;

namespace WickedFlame.Common.Mvvm.Commands
{
	/// <example>
	/// Recommended usage:
	/// <code>
	/// 
	/// <Control.InputBindings>
	///     <ib:RelayMouseBinding Gesture="LeftButtonUp" CommandBinding="{Binding NewCommand}"/>
	/// </Control.InputBindings>
	/// 
	/// </code>
	/// </example>

	/// <summary>
	/// Bind RelayCommands to mouse gesutures.
	/// </summary>
	public class RelayMouseBinding : MouseBinding
	{
		public static readonly DependencyProperty CommandBindingProperty =
			DependencyProperty.Register("CommandBinding", typeof(ICommand),
			typeof(RelayMouseBinding),
			new FrameworkPropertyMetadata(OnCommandBindingChanged));

		public ICommand CommandBinding
		{
			get
			{
				return (ICommand)GetValue(CommandBindingProperty);
			}
			set
			{
				SetValue(CommandBindingProperty, value);
			}
		}

		private static void OnCommandBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var mouseBinding = (RelayMouseBinding)d;
			mouseBinding.Command = e.NewValue as ICommand;
		}
	}
}
