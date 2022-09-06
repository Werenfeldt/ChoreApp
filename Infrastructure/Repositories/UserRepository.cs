namespace Infrastructure;

public class UserRepository : IUserRepository
{
    public readonly IChoreAppContext _context;

    public UserRepository(IChoreAppContext context)
    {
        _context = context;
    }
    public Task<UserDTO> CreateUserAsync(CreateUserDTO user)
    {
        throw new NotImplementedException();
    }

    public Task<Option<UserDetailsDTO>> ReadUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}