using System;
using System.Windows.Input;

namespace BanResources.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Predicate<object> _canExecute;
        public RelayCommand(Action execute)
            : this(execute, (object obj) => true) { }

        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
    }

    public class RelayCommand<TParameter> : ICommand
    {
        private readonly Action<TParameter> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<TParameter> execute)
            : this(execute, (object obj) => true) { }


        public RelayCommand(Action<TParameter> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute((TParameter)parameter!);
        }
    }
}
