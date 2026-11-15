namespace CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;

// Entidad de dominio Alquiler, marcada como sealed para evitar herencia.
public sealed class Alquiler : Entity
{
    // Constructor privado: asegura que la creación de Alquiler
    // se haga únicamente a través de métodos estáticos de fábrica.
    private Alquiler(
        Guid id,
        Guid vehiculoId,
        Guid userId,
        DateRange duracion,
        Moneda precioPorPeriodo,
        Moneda mantenimiento,
        Moneda accesorios,
        Moneda precioTotal,
        AlquilerStatus status,
        DateTime fechaCreacion
    ) : base(id)
    {
        VehiculoId = vehiculoId;
        UserId = userId;
        Duracion = duracion;
        PrecioPorPeriodo = precioPorPeriodo;
        Mantenimiento = mantenimiento;
        Accesorios = accesorios;
        PrecioTotal = precioTotal;
        Status = status;
        FechaCreacion = fechaCreacion;
    }

    // Propiedades de la entidad (inmutables desde fuera, modificables solo dentro de la clase).
    public Guid VehiculoId { get; private set; }
    public Guid UserId { get; private set; }
    public Moneda? PrecioPorPeriodo { get; private set; }
    public Moneda? Mantenimiento { get; private set; }
    public Moneda? Accesorios { get; private set; }
    public Moneda? PrecioTotal { get; private set; }
    public AlquilerStatus Status { get; private set; }
    public DateRange? Duracion { get; private set; }

    // Fechas de ciclo de vida del alquiler
    public DateTime? FechaCreacion { get; private set; }
    public DateTime? FechaConfirmacion { get; private set; }
    public DateTime? FechaDenegacion { get; private set; }
    public DateTime? FechaCompletado { get; private set; }
    public DateTime? FechaCancelacion { get; private set; }

    // Método de fábrica para reservar un alquiler
    public static Alquiler Reservar(
      Vehiculo vehiculo,
      Guid userId,
      DateRange duracion,
      DateTime fechaCreacion,
      PrecioService precioService 
    )
    {
        // Se calcula el precio usando el Domain Service PrecioService
        var precioSercice = precioService.CalcularPrecio(vehiculo, duracion);

        // Se crea la instancia de Alquiler con estado inicial "Reservado"
        var alquiler = new Alquiler(
            Guid.NewGuid(),
            vehiculo.Id,
            userId,
            duracion,
            precioSercice.PrecioPorPeriodo,
            precioSercice.Mantenimiento,
            precioSercice.Accesorios,
            precioSercice.PrecioTotal,
            AlquilerStatus.Reservado,
            fechaCreacion
        );

        // Se dispara un evento de dominio indicando que se reservó un alquiler
        alquiler.RaiseDomainEvent(new AlquilerReservadoDomainEvent(alquiler.Id!));

        // Se actualiza el vehículo con la fecha del último alquiler
        vehiculo.fechaUltimoAlquiler = fechaCreacion;
       
        return alquiler;
    }

    // Confirmar un alquiler reservado
    public Result Confirmar(DateTime utcNow)
    {
        if(Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerError.NotReserved);
        }

        Status = AlquilerStatus.Confirmado;
        FechaConfirmacion = utcNow;

        RaiseDomainEvent(new AlquilerConfirmadoDomainEvent(Id!));

        return Result.Success();
    }

    // Rechazar un alquiler reservado
    public Result Rechazar(DateTime utcNow)
    {
        if(Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerError.NotReserved);
        }

        Status = AlquilerStatus.Rechazado;
        FechaDenegacion = utcNow;

        RaiseDomainEvent(new AlquilerRechazadoDomainEvent(Id!));

        return Result.Success();
    }

    // Cancelar un alquiler confirmado
    public Result Cancelar(DateTime utcNow)
    {
        if(Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerError.NotConfirmed);
        }
        
        var currentDate = DateOnly.FromDateTime(utcNow);
        
        // Validación: no se puede cancelar si ya inició el periodo de alquiler
        if(currentDate >= Duracion!.Inicio)
        {
            return Result.Failure(AlquilerError.AlreadyStarted);
        }

        Status = AlquilerStatus.Cancelado;
        FechaCancelacion = utcNow;

        RaiseDomainEvent(new AlquilerCanceladoDomainEvent(Id!));

        return Result.Success();
    }

    public Result Completar(DateTime utcNow)
    {
        if(Status != AlquilerStatus.Completado)
        {
            return Result.Failure(AlquilerError.NotConfirmed);
        }

        Status = AlquilerStatus.Completado;
        FechaCompletado = utcNow;

        RaiseDomainEvent(new AlquilerCompletadoDomainEvent(Id!));

        return Result.Success();
    }

}
