namespace CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;

// Definimos una interface llamada IAlquilerRepository.
// Representa el contrato que cualquier repositorio de "Alquileres" debe cumplir.
public interface IAlquilerRepository
{
    // Método asíncrono que busca un alquiler por su Id.
    // Devuelve un objeto Alquiler o null si no existe.
    Task<Alquiler?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // Método asíncrono que verifica si un vehículo ya tiene un alquiler
    // que se superpone con el rango de fechas indicado.
    // Devuelve true si hay traslape, false si no.
    Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo,
        DateRange duracion,
        CancellationToken cancellationToken = default
    );

    // Método para agregar un nuevo alquiler al repositorio.
    // No es asíncrono porque normalmente solo marca la entidad para guardar.
    void Add(Alquiler alquiler);
}
