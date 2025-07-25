using AVATAi.Core.Services;

namespace AVATAi.Services;

public class UIThreadInvoker : IUIThreadInvoker
{
    public void InvokeOnMainThread(Action action)
    {
        MainThread.BeginInvokeOnMainThread(action);
    }

    public bool IsMainThread => MainThread.IsMainThread;
}