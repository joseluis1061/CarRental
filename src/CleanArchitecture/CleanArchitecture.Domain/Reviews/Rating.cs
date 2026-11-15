namespace CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Abstractions;
public sealed record Rating
{
    public static readonly Error Invalid = new Error("Rating.Invalid", "El valor de la calificación debe estar entre 1 y 5.");

    public int Value { get; init;}

    private Rating(int value) => Value = value;

    // private Rating(int value) { value = value; } // Otra forma de definir el constructor privado

    public static Result<Rating> Create(int value)
    {
        if(value < 1 || value > 5)
        {
            return Result.Failure<Rating>(Invalid);
        }

        return Result.Success(new Rating(value));
    }
}