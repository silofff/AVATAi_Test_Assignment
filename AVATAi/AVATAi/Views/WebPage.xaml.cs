using AVATAi.Core.ViewModels;

namespace AVATAi.Views;

public partial class WebPage
{
    public WebPage(WebPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}