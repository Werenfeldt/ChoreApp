namespace Name;

public class UserRepository : IUserRepository
{
    public Task<UserDTO> CreateUserAsync(CreateUserDTO user)
    {
        throw new NotImplementedException();
    }

    public Task<Option<UserDetailsDTO>> ReadUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}