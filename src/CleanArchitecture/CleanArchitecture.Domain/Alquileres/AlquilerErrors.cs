namespace CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Abstractions;

public static class AlquilerError
{
    public static Error NotFound = new Error("Alquiler.NotFound", "El alquiler con el Id no fue encontrado.");

    public static Error Overlap = new Error(
        "Alquiler.Overlap",
        "El alquiler esta siendo tomado por 2 o mas clientes al mismo tiempo."
    );

    public static Error NotReserved = new Error(
        "Alquiler.NotReserved",
        "El alquiler no se encuentra en estado reservado."
    );

    public static Error NotConfirmed = new Error(
        "Alquiler.NotConfirmed",
        "El alquiler no se encuentra en estado confirmado."
    );

    public static Error AlreadyStarted = new Error(
        "Alquiler.AlreadyStarted",
        "El alquiler ya ha comenzado."
    );
};