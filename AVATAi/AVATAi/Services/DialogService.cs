using AVATAi.Core.Services;

namespace AVATAi.Services;

public class DialogService : IDialogService
{
    public async Task ShowAlert(string title, string message, string cancel)
    {
        var window = Application.Current?.Windows?.FirstOrDefault();
        var page = window?.Page;

        if (page != null)
            await page.DisplayAlert(title, message, cancel);
    }
}