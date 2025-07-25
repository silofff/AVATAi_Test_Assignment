using AVATAi.Core.ViewModels;
using AVATAi.Core.Services;
using Moq;

namespace AVATAi.Tests;

public class FormPageViewModelTests
{   
    [Fact]
    public void CanSend_ShouldBeFalse_WhenFieldsEmpty()
    {
        var nav = new Mock<INavigationService>();
        var dialog = new Mock<IDialogService>();

        var vm = new FormPageViewModel(nav.Object, dialog.Object);

        Assert.False(vm.CanSend);

        vm.Name = "Test";
        Assert.False(vm.CanSend);

        vm.Message = "Hello";
        Assert.True(vm.CanSend);
    }
}