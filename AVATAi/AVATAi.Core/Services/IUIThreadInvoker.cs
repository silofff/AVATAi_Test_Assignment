namespace AVATAi.Core.Services;

public interface IUIThreadInvoker
{
    void InvokeOnMainThread(Action action);
    bool IsMainThread { get; }
}