// Define el espacio de nombres donde vive este servicio de dominio
namespace CleanArchitecture.Domain.Alquileres;

using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

// Clase de servicio de dominio encargada de calcular precios de alquiler
public class PrecioService
{
    // Método principal que calcula el precio de un vehículo en un periodo determinado
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
    {
        // Se obtiene el tipo de moneda del precio del vehículo
        var tipoMoneda = vehiculo.Precio!.TipoMoneda;

        // Se calcula el precio base multiplicando los días del periodo por el monto del precio del vehículo
        var precioPorPeriodo = new Moneda(periodo.CantidadDias * vehiculo.Precio.Monto, tipoMoneda);

        // Variable para acumular el porcentaje adicional por accesorios
        decimal porcentajeChange = 0; 
        
        // Se recorren los accesorios del vehículo y se suman porcentajes según el tipo
        foreach (var accesorio in vehiculo.Accesorios)
        {
            porcentajeChange += accesorio switch
            {
                // Aire acondicionado o control de crucero adaptativo → +5%
                Accesorio.AireAcondicionado or Accesorio.ControlDeCruceroAdaptativo => 0.05m,
                // Asientos de cuero → +10%
                Accesorio.AsientosDeCuero => 0.10m,
                // Cámara de reversa → +15%
                Accesorio.CamaraDeReversa => 0.15m,
                // Otros accesorios → sin cargo adicional
                _ => 0
            };
        }
        
        // Inicializa los cargos por accesorios en cero
        var accesorioCharges = Moneda.Zero(tipoMoneda);

        // Si hubo algún porcentaje acumulado, se calcula el cargo adicional
        if (porcentajeChange > 0)
        {
            accesorioCharges = new Moneda(precioPorPeriodo.Monto * porcentajeChange, tipoMoneda);
        }

        // Inicializa el precio total en cero
        var precioTotal = Moneda.Zero();

        // Se suma el precio base por periodo
        precioTotal += precioPorPeriodo;

        // Si el vehículo tiene un costo de mantenimiento distinto de cero, se suma
        if (!vehiculo.Mantenimiento!.IsZero)
        {
            precioTotal += vehiculo.Mantenimiento;
        }

        // Se retorna un objeto PrecioDetalle con el desglose de valores
        return new PrecioDetalle(
            PrecioPorPeriodo: precioPorPeriodo,
            Mantenimiento: vehiculo.Mantenimiento,
            Accesorios: accesorioCharges,
            PrecioTotal: precioTotal
        );
    }
}
