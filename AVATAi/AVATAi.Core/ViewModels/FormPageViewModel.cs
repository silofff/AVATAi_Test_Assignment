using System.Windows.Input;
using AVATAi.Core.Commands;
using AVATAi.Core.Navigation;
using AVATAi.Core.Services;

namespace AVATAi.Core.ViewModels;

public class FormPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;
    private readonly IDialogService _dialogService;

    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value, additionalPropertiesToNotify: nameof(CanSend));
    }

    private string _message;
    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value, additionalPropertiesToNotify: nameof(CanSend));
    }
    
    private bool _isSending;
    public bool IsSending
    {
        get => _isSending;
        set => SetProperty(ref _isSending, value, additionalPropertiesToNotify: nameof(CanSend));
    }
    
    public bool CanSend => 
        !string.IsNullOrWhiteSpace(Name) && 
        !string.IsNullOrWhiteSpace(Message) && 
        !IsSending;

    public ICommand SendCommand { get; }

    public FormPageViewModel(INavigationService navigation, IDialogService dialogService)
    {
        _navigation = navigation;
        _dialogService = dialogService;
        SendCommand = new ExtendedCommand(async () => await SendAsync(), () => !IsSending);
        RegisterCommandDependency(nameof(IsSending), SendCommand);
    }

    private async Task SendAsync()
    {
        if (IsSending) return;
        
        IsSending = true;

        await _navigation.ShowModalAsync(AppScreens.LoadingDialog);
        
        await Task.Delay(2000); // simulate sending

        await _navigation.CloseModalAsync();

        await _dialogService.ShowAlert("Success", "Form data sent successfully!", "OK");

        Name = string.Empty;
        Message = string.Empty;

        IsSending = false;
    }
}