using System.Windows.Input;
using AVATAi.Core.Services;

namespace AVATAi.Core.Commands;

public class ExtendedCommand : ICommand
{
    private readonly Func<Task> _executeAsync;
    private readonly Action _executeSync;
    private readonly Func<bool>? _canExecute;
    private bool _isExecuting;
    private readonly bool _blockWhileExecuting;

    public event EventHandler? CanExecuteChanged;
    
    protected IUIThreadInvoker UIThreadInvoker => Dependencies.Get<IUIThreadInvoker>();

    public ExtendedCommand(Func<Task> executeAsync, Func<bool>? canExecute = null, bool blockWhileExecuting = true)
    {
        _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
        _canExecute = canExecute;
        _blockWhileExecuting = blockWhileExecuting;
    }

    public ExtendedCommand(Action executeSync, Func<bool>? canExecute = null, bool blockWhileExecuting = true)
    {
        _executeSync = executeSync ?? throw new ArgumentNullException(nameof(executeSync));
        _canExecute = canExecute;
        _blockWhileExecuting = blockWhileExecuting;
    }

    public bool CanExecute(object parameter)
    {
        if (_isExecuting && _blockWhileExecuting)
            return false;

        return _canExecute?.Invoke() ?? true;
    }

    public async void Execute(object parameter)
    {
        if (!CanExecute(parameter))
            return;

        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();

            if (_executeAsync != null)
                await _executeAsync();
            else
                _executeSync?.Invoke();
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
