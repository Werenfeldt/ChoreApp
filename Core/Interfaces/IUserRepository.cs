namespace Core;

public interface IUserRepository
{
    Task<UserDTO> CreateUserAsync(CreateUserDTO user);
    Task<Option<UserDetailsDTO>> ReadUserByIdAsync(Guid id);

}