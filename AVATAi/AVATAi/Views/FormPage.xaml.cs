using AVATAi.Core.ViewModels;

namespace AVATAi.Views;

public partial class FormPage
{
    public FormPage(FormPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}