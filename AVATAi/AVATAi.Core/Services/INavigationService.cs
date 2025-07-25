namespace AVATAi.Core.Services;

using AVATAi.Core.Navigation;

public interface INavigationService
{
    Task NavigateAsync(AppScreens screen, Dictionary<string, object> parameters = null);
    Task GoBackAsync();

    Task NavigateAsync<TParam>(AppScreens screen, TParam parameter);
    Task ShowModalAsync(AppScreens modal, Dictionary<string, object> parameters = null);
    Task CloseModalAsync();
}

