using AVATAi.Core.ViewModels;

namespace AVATAi.Views;

public partial class StudentDetailPage
{
    public StudentDetailPage(StudentDetailPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}