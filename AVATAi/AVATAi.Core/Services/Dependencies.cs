using Microsoft.Extensions.DependencyInjection;

namespace AVATAi.Core.Services;

public static class Dependencies
{
    public static IServiceProvider? ServiceProvider { get; set; }

    public static TRegisterType Get<TRegisterType>() where TRegisterType : class
    {
        if (ServiceProvider is null)
            throw new Exception($"{nameof(ServiceProvider)} cannot be null.");

        return ServiceProvider.GetService<TRegisterType>()
               ?? throw new Exception($"Service {typeof(TRegisterType).Name} is not registered.");
    }
}