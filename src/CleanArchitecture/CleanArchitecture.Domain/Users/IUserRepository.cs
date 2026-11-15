namespace CleanArchitecture.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // Metodo definido para agregar un usuario al repositorio, solo persiste en el dominuio, no en la base de datos
    void Add(User user);
}