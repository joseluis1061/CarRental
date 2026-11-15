namespace CleanArchitecture.Application;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Domain.Alquileres;

public static class DependencyInjection
{
    // Método de extensión que se agrega a IServiceCollection
    // Esto permite que en Program.cs o Startup.cs se pueda llamar a "services.AddApplication()"
    // y automáticamente se registren todos los servicios de la capa Application.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 🔹 Registro de MediatR
        // Escanea la asamblea (proyecto Application) buscando todos los "Handlers"
        // (RequestHandlers, NotificationHandlers, etc.) que implementen interfaces de MediatR.
        // De esta forma, no tienes que registrar cada handler manualmente.
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // 🔹 Registro de PrecioService
        // Aquí se registra explícitamente el servicio de dominio PrecioService
        // como "Transient", lo que significa que se crea una nueva instancia
        // cada vez que se solicita. Esto permite que cualquier handler o clase
        // de la capa Application pueda pedirlo vía inyección de dependencias.
        services.AddTransient<PrecioService>();

        // Retorna la colección de servicios ya configurada
        return services;
    }
}
