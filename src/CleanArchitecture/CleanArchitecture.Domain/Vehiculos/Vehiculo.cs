namespace CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

public sealed class Vehiculo : Entity
{
    public Vehiculo(
        Guid id, 
        Modelo modelo, 
        Vin vin,
        Moneda precio,
        Moneda mantenimiento,
        DateTime? fechaUltimoAlquiler,
        List<Accesorio> accesorios,
        Direccion direccion
    ) : base(id)
    {
        Modelo = modelo;
        Vin = vin;
        Precio = precio;
        Moneda = precio?.TipoMoneda;
        Mantenimiento = mantenimiento;
        MantenimientoTipoMoneda = mantenimiento?.TipoMoneda;
        this.fechaUltimoAlquiler = fechaUltimoAlquiler;
        Accesorios = accesorios ?? new List<Accesorio>();
        Direccion = direccion;
    }
    public Modelo? Modelo { get; private set; }
    public Vin? Vin { get; private set; }
    
    public Direccion? Direccion { get; private set; }
    public Moneda? Precio { get; private set; }
    public TipoMoneda? Moneda { get; private set; }
    public Moneda? Mantenimiento { get; private set; } // Precio de mantenimiento del vehículo
    public TipoMoneda? MantenimientoTipoMoneda { get; private set; } 
    public DateTime? fechaUltimoAlquiler { get; internal set; }
    public List<Accesorio> Accesorios { get; private set; } = new List<Accesorio>();
}