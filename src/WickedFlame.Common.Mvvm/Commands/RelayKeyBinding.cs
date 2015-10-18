using System.Windows;
using System.Windows.Input;

namespace WickedFlame.Common.Mvvm.Commands
{
	/// <example>
	/// Recommended usage:
	/// <code>
	/// 
	/// <Control.InputBindings>
	///     <ib:RelayKeyBinding Key="N" Modifiers="Ctrl" CommandBinding="{Binding NewCommand}"/>
	/// </Control.InputBindings>
	/// 
	/// </code>
	/// </example>
	
	/// <summary>
	/// Bind RelayCommands to key gesutures.
	/// </summary>
	public class RelayKeyBinding : KeyBinding
	{
		public static readonly DependencyProperty CommandBindingProperty =
			DependencyProperty.Register("CommandBinding", typeof(ICommand),
			typeof(RelayKeyBinding),
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
			var keyBinding = (RelayKeyBinding)d;
			keyBinding.Command = e.NewValue as ICommand;
		}
	}
}
