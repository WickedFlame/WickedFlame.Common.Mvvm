using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WickedFlame.Common.Mvvm.Commands
{
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
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
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

            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute((T)parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
