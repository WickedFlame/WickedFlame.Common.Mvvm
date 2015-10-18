using System;
using System.Windows.Input;
using System.Diagnostics;

namespace WickedFlame.Common.Mvvm.Commands
{
	/// <summary>
	/// A command whose sole purpose is to 
	/// relay its functionality to other
	/// objects by invoking delegates. The
	/// default return value for the CanExecute
	/// method is 'true'.
	/// </summary>
	public class RelayCommand : ICommand
	{
		#region Fields

		readonly Action _execute;
		readonly Predicate<object> _canExecute;
		internal readonly RoutedUICommand _routedUICommand;
		#endregion // Fields

		#region Constructors

		/// <summary>
		/// Creates a new command that can always execute.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		public RelayCommand(Action execute)
			: this(execute, null)
		{
		}

		/// <summary>
		/// Creates a new command.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		/// <param name="canExecute">The execution status logic.</param>
		public RelayCommand(Action execute, Predicate<object> canExecute, RoutedUICommand routedUICommand = null)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");

			_execute = execute;
			_canExecute = canExecute;
			_routedUICommand = routedUICommand;
		}

		#endregion // Constructors

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			_execute();
		}

		#endregion // ICommand Members

		#region KeyGestures

		public Key GestureKey { get; set; }

		public ModifierKeys GestureModifier { get; set; }

		public MouseAction MouseGesture { get; set; }

		#endregion
	}



	/// <summary>
	/// http://stackoverflow.com/questions/900437/relaycommand-lambda-syntax-problem
	/// To pass a parameter to the delegate, you'll need to use his new RelayCommand<T> constructor instead
	/// public ICommand GotoRegionCommand
	/// {
	/// 	get
	/// 	{
	/// 		if (_gotoRegionCommand == null)
	/// 			_gotoRegionCommand = new RelayCommand<String>(GotoRegionCommandWithParameter);
	/// 		return _gotoRegionCommand;
	/// 	}
	/// }
	/// private void GotoRegionCommandWithParameter(object param)
	/// {
	/// 	var str = param as string;
	/// }
	/// </summary>
	/// <typeparam name="T"></typeparam>

	public class RelayCommand<T> : ICommand
	{
		// Fields
		private readonly Predicate<T> _canExecute;
		private readonly Action<T> _execute;

		// Events
		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (this._canExecute != null)
				{
					CommandManager.RequerySuggested += value;
				}
			}
			remove
			{
				if (this._canExecute != null)
				{
					CommandManager.RequerySuggested -= value;
				}
			}
		}

		// Methods
		public RelayCommand(Action<T> execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action<T> execute, Predicate<T> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
			this._canExecute = canExecute;
		}

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			if (this._canExecute != null)
			{
				return this._canExecute((T)parameter);
			}
			return true;
		}

		public void Execute(object parameter)
		{
			this._execute((T)parameter);
		}
	}
}
