namespace CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Shared;
public record PrecioDetalle
(
    Moneda PrecioPorPeriodo,
    Moneda Mantenimiento,
    Moneda Accesorios,
    Moneda PrecioTotal
);