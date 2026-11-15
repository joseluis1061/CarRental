namespace CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Abstractions;

public static class ReviewErrors
{
    public static readonly Error NotElegible = new Error("Review.NotElegible", "No puedes crear una reseña para un alquiler que no ha sido completado.");
}