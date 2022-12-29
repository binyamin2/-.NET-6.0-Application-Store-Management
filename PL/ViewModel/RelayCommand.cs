using System;
using System.Windows.Input;

namespace PL.ViewModel;

public class RelayCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private Action excecute;

    public RelayCommand(Action excecute) => this.excecute = excecute;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => excecute();
}

public class RelayCommand<T> : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private Action<T> excecute;

    public RelayCommand(Action<T> excecute) => this.excecute = excecute;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => excecute((T)parameter);
}

