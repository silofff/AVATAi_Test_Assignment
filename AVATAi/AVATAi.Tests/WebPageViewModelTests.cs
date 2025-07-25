using AVATAi.Core.ViewModels;

namespace AVATAi.Tests;

public class WebPageViewModelTests
{
    [Fact]
    public void VideoUrlSource_ShouldBeLazyLoaded()
    {
        var vm = new WebPageViewModel();

        Assert.Null(vm.VideoUrlSource);
        Assert.False(vm.IsVideoSelected);

        vm.IsVideoSelected = true;

        Assert.Equal("https://video.avatai.my", vm.VideoUrlSource);
        Assert.True(vm.IsVideoSelected);
    }
}