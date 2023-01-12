using System;
using System.Windows.Input;

namespace PL.ViewModel;
/// <summary>
/// class that herited from command and defines the property
/// </summary>
public class RelayCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private Action excecute;

    public RelayCommand(Action excecute) => this.excecute = excecute;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => excecute();
}
/// <summary>
/// class that herited from command and defines the property and accepted value
/// </summary>
public class RelayCommand<T> : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private Action<T> excecute;

    public RelayCommand(Action<T> excecute) => this.excecute = excecute;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => excecute((T)parameter);
}

