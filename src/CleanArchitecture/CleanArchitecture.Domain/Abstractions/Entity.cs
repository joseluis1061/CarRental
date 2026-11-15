namespace CleanArchitecture.Domain.Abstractions;

// Clase abstracta base para todas las entidades del dominio.
// Implementa la interfaz IDomainEvent, lo que permite manejar eventos de dominio.
public abstract class Entity : IDomainEvent
{
    // Lista privada donde se almacenan los eventos generados por la entidad.
    // Esta lista representa el "buffer" de eventos que luego serán publicados.
    private readonly List<IDomainEvent> _domainEvents = new();

    // Constructor protegido: asegura que solo clases derivadas puedan instanciar.
    protected Entity(Guid id)
    {
        Id = id; // Cada entidad tiene un identificador único.
    }

    // Propiedad inmutable que representa el identificador de la entidad.
    public Guid Id { get; init; }

    // Devuelve una colección de solo lectura con los eventos acumulados.
    // Esto permite que otros componentes (ej. un Event Dispatcher) los lean.
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() {
        return _domainEvents.ToList();
    }

    // Limpia la lista de eventos una vez que han sido publicados o procesados.
    // Evita que se disparen múltiples veces.
    public void ClearDomainEvents() {
        _domainEvents.Clear();
    }

    // Método protegido para agregar un nuevo evento de dominio.
    // Solo las clases derivadas pueden invocar este método.
    // Aquí es donde la entidad "publica" un evento que luego será suscrito.
    protected void RaiseDomainEvent(IDomainEvent domainEvent) {
        _domainEvents.Add(domainEvent);
    }
}
