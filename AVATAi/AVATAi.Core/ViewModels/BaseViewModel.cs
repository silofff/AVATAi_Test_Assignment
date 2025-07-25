using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AVATAi.Core.Commands;

namespace AVATAi.Core.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly Dictionary<string, List<ICommand>> _propertyDependencies = new();

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        if (propertyName != null && _propertyDependencies.TryGetValue(propertyName, out var commands))
        {
            foreach (var cmd in commands.OfType<ExtendedCommand>())
                cmd.RaiseCanExecuteChanged();
        }
    }

    protected bool SetProperty<T>(
        ref T backingField,
        T value,
        [CallerMemberName] string propertyName = null,
        params string[] additionalPropertiesToNotify)
    {
        if (EqualityComparer<T>.Default.Equals(backingField, value))
            return false;

        backingField = value;
        OnPropertyChanged(propertyName);

        // уведомляем все дополнительные свойства
        if (additionalPropertiesToNotify != null)
        {
            foreach (var prop in additionalPropertiesToNotify)
                OnPropertyChanged(prop);
        }

        return true;
    }

    protected void RegisterCommandDependency(string propertyName, ICommand command)
    {
        if (!_propertyDependencies.ContainsKey(propertyName))
            _propertyDependencies[propertyName] = new List<ICommand>();

        if (!_propertyDependencies[propertyName].Contains(command))
            _propertyDependencies[propertyName].Add(command);
    }
}
