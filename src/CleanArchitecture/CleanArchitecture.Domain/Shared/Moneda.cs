// Define el espacio de nombres donde vive esta clase/record
namespace CleanArchitecture.Domain.Shared;

// Se declara un 'record' llamado Moneda.
// Representa una cantidad de dinero (Monto) junto con su tipo de moneda (TipoMoneda).
public record Moneda(decimal Monto, TipoMoneda TipoMoneda)
{
    // Sobrecarga del operador + para sumar dos instancias de Moneda.
    public static Moneda operator +(Moneda a, Moneda b)
    {
        // Validación: no se pueden sumar monedas de diferentes tipos.
        if (a.TipoMoneda != b.TipoMoneda)
        {
            throw new InvalidOperationException("No se pueden sumar monedas de diferentes tipos.");
        }
        // Si son del mismo tipo, retorna una nueva Moneda con la suma de los montos.
        return new Moneda(a.Monto + b.Monto, a.TipoMoneda);
    }

    // Método estático que devuelve una Moneda con monto 0 y tipo "None".
    // Sirve como valor neutro o inicializador.
    public static Moneda Zero() => new Moneda(0, TipoMoneda.None);

    // Sobrecarga de Zero: permite especificar el tipo de moneda.
    // Retorna una Moneda con monto 0 y el tipo indicado.
    public static Moneda Zero(TipoMoneda tipoMoneda) => new (0, tipoMoneda);

    // Propiedad booleana que indica si la instancia actual es igual a una Moneda de monto 0
    // con el mismo tipo de moneda.
    public bool IsZero => this == Zero(TipoMoneda);
}
