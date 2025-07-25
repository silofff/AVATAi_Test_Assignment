using System.Windows.Input;
using AVATAi.Core.Commands;

namespace AVATAi.Core.ViewModels;

public class WebPageViewModel : BaseViewModel
{
    private const string DefaultUrl = "https://avatai.my";
    private const string VideoUrl = "https://video.avatai.my";
    
    public string DefaultUrlSource { get; set; } = DefaultUrl;

    public string DefaultUrlText => RemoveHttps(DefaultUrl);
    
    public string VideoUrlText => RemoveHttps(VideoUrl);
    
    private string _videoUrlSource;
    public string VideoUrlSource
    {
        get => _videoUrlSource;
        set => SetProperty(ref _videoUrlSource, value);
    }
    
    private bool _isVideoSelected;
    public bool IsVideoSelected
    {
        get => _isVideoSelected;
        set
        {
            if (SetProperty(ref _isVideoSelected, value))
            {
                if (value && string.IsNullOrEmpty(VideoUrlSource))
                    VideoUrlSource = VideoUrl;
            }
        }
    }

    public ICommand ToggleUrlCommand { get; }

    public WebPageViewModel()
    {
        ToggleUrlCommand = new ExtendedCommand(ToggleUrl);
    }

    private void ToggleUrl()
    {
        IsVideoSelected = !IsVideoSelected;
    }
    
    private string RemoveHttps(string url) => url.Replace("https://", string.Empty);
}