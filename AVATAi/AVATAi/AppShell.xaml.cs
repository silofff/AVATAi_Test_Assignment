using AVATAi.Views;

namespace AVATAi;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(StudentDetailPage), typeof(StudentDetailPage));
        Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
    }
}