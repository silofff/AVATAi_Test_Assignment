using AVATAi.Core.Services;
using AVATAi.Core.Navigation;
using AVATAi.Core.ViewModels;
using AVATAi.Views;

namespace AVATAi.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _services;

    public NavigationService(IServiceProvider services)
    {
        _services = services;
    }
    
    public Task GoBackAsync()
    {
        return Shell.Current.GoToAsync("..");
    }

    public Task NavigateAsync(AppScreens screen, Dictionary<string, object> parameters = null)
    {
        return Shell.Current.GoToAsync(GetRoute(screen), true, parameters ?? new Dictionary<string, object>());
    }
    
    public async Task NavigateAsync<TParam>(AppScreens screen, TParam parameter)
    {
        var pageType = screen switch
        {
            AppScreens.StudentDetail => typeof(StudentDetailPage),
            AppScreens.LoadingDialog => typeof(LoadingPage),
            _ => throw new ArgumentOutOfRangeException(nameof(screen))
        };

        if (_services.GetService(pageType) is Page { BindingContext: IInitialize<TParam> vm } page)
        {
            vm.Initialize(parameter);
            await Shell.Current.Navigation.PushAsync(page);
        }
    }

    public Task ShowModalAsync(AppScreens modal, Dictionary<string, object> parameters = null)
    {
        return Shell.Current.GoToAsync(GetRoute(modal), true, parameters ?? new Dictionary<string, object>());
    }
    
    public Task CloseModalAsync()
    {
        return Shell.Current.GoToAsync("..");
    }

    private string GetRoute(AppScreens screen)
    {
        return screen switch
        {
            AppScreens.StudentDetail => nameof(StudentDetailPage),
            AppScreens.LoadingDialog => nameof(LoadingPage),
            _ => throw new ArgumentOutOfRangeException(nameof(screen), screen, null)
        };
    }
}