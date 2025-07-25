using AVATAi.Core.Services;
using AVATAi.Core.ViewModels;
using AVATAi.Services;
using AVATAi.Views;
using Microsoft.Extensions.Logging;

namespace AVATAi;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
            // Services   
            .AddSingleton<INavigationService, NavigationService>()
            .AddSingleton<IDialogService, DialogService>()
            .AddSingleton<IUIThreadInvoker, UIThreadInvoker>()
            
            // Pages
            .AddTransient<MainPage>()
            .AddTransient<FormPage>()
            .AddTransient<WebPage>()
            .AddTransient<StudentDetailPage>()
            
            // ViewModels
            .AddTransient<MainPageViewModel>()
            .AddTransient<FormPageViewModel>()
            .AddTransient<StudentDetailPageViewModel>()
            .AddTransient<WebPageViewModel>();
            
#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        var app = builder.Build();
        Dependencies.ServiceProvider = app.Services;
        
        return app;
    }
}