// Define el espacio de nombres donde vive esta clase/record
namespace CleanArchitecture.Domain.Shared;

// Se declara un 'record' llamado TipoMoneda.
// Los records en C# son tipos inmutables que se usan para representar datos.
public record TipoMoneda
{
    // Campo estático que representa un valor "nulo" o vacío de moneda.
    public static readonly TipoMoneda None = new TipoMoneda("");

    // Campo estático que representa la moneda Euro.
    public static readonly TipoMoneda Eur = new TipoMoneda("EUR");

    // Campo estático que representa la moneda Dólar estadounidense.
    public static readonly TipoMoneda Usd = new TipoMoneda("USD");

    // Constructor privado que recibe un código de moneda y lo asigna a la propiedad.
    // Se usa 'expression-bodied constructor' para simplificar la sintaxis.
    TipoMoneda(string codigo) => Codigo = codigo;

    // Propiedad pública de solo inicialización (init-only).
    // Una vez asignada en el constructor, no puede cambiarse después.
    public string Codigo { get; init; }

    // Colección estática que contiene todas las monedas válidas (excepto Null).
    // Se expone como IReadOnlyCollection para que no pueda modificarse externamente.
    public static readonly IReadOnlyCollection<TipoMoneda> All = new[] { Eur, Usd };

    // Método estático que busca una moneda a partir de su código.
    // Si el código existe en la colección All, retorna la instancia correspondiente.
    // Si no existe, lanza una excepción indicando que el tipo de moneda es inválido.
    public static TipoMoneda FromCodigo(string codigo)
    {
        return All.FirstOrDefault(x => x.Codigo == codigo) ?? 
            throw new ApplicationException($"Tipo de moneda inválido: {codigo}");
    }
}
