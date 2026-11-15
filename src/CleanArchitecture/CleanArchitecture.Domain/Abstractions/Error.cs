// Define el espacio de nombres donde se ubica este record.
// Normalmente se usa para organizar las clases y records por capas o dominios.
namespace CleanArchitecture.Domain.Abstractions;

// Se declara un 'record' llamado Error.
// Los records en C# son tipos inmutables, ideales para representar datos simples.
public record Error(string Code, string Name)
{
    // Campo estático que representa la ausencia de error.
    // Se inicializa con valores vacíos para indicar que "no hay error".
    public static Error None = new Error(string.Empty, string.Empty);

    // Campo estático que representa un error específico: valor nulo.
    // Se define con un código único ("Error.NullValue") y un mensaje descriptivo.
    public static Error NullValue = new Error("Error.NullValue", "El valor ingresado no puede ser nulo.");
};
