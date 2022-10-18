namespace ChoreApp.Core;

public interface IUserRepository
{
    Task<UserDTO> CreateUserAsync(CreateUserDTO user);
    Task<Option<UserDetailsDTO>> ReadDetailedUserByIdAsync(Guid id);
    Task<Option<UserDTO>> ReadUserByIdAsync(Guid id);

}