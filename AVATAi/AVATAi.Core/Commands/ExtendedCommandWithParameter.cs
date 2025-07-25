using System.Windows.Input;
using AVATAi.Core.Services;

namespace AVATAi.Core.Commands;

public class ExtendedCommand<T> : ICommand
{
    private readonly Func<T, Task>? _executeAsync;
    private readonly Action<T> _executeSync;
    private readonly Func<T, bool>? _canExecute;
    private bool _isExecuting;
    private readonly bool _blockWhileExecuting;

    public event EventHandler? CanExecuteChanged;
    
    protected IUIThreadInvoker UIThreadInvoker => Dependencies.Get<IUIThreadInvoker>();

    public ExtendedCommand(Func<T, Task> executeAsync, Func<T, bool>? canExecute = null, bool blockWhileExecuting = true)
    {
        _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
        _canExecute = canExecute;
        _blockWhileExecuting = blockWhileExecuting;
    }

    public ExtendedCommand(Action<T> executeSync, Func<T, bool>? canExecute = null, bool blockWhileExecuting = true)
    {
        _executeSync = executeSync ?? throw new ArgumentNullException(nameof(executeSync));
        _canExecute = canExecute;
        _blockWhileExecuting = blockWhileExecuting;
    }

    public bool CanExecute(object parameter)
    {
        if (_isExecuting && _blockWhileExecuting)
            return false;

        if (_canExecute != null && parameter is T tParam)
            return _canExecute(tParam);

        return true;
    }

    public async void Execute(object parameter)
    {
        if (!CanExecute(parameter))
            return;

        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();

            if (parameter is T tParam)
            {
                if (_executeAsync != null)
                    await _executeAsync(tParam);
                else
                    _executeSync?.Invoke(tParam);
            }
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    public void RaiseCanExecuteChanged()
    {
        UIThreadInvoker.InvokeOnMainThread(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty));
    }
}