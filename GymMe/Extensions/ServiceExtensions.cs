
namespace GymMe.Extensions;

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        var handlerTypes = 
            typeof(Program)
            .Assembly
            .GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("Handler"));
            
        foreach(var handler in handlerTypes)
            builder.Services.AddScoped(handler);
            
        return builder;
    }
}