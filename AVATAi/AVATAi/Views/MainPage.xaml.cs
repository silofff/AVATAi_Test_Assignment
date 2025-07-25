using AVATAi.Core.ViewModels;

namespace AVATAi.Views;

public partial class MainPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
