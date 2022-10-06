namespace Infrastructure;

public class UserRepository : IUserRepository
{
    public readonly IChoreAppContext _context;

    public UserRepository(IChoreAppContext context)
    {
        _context = context;
    }
    public async Task<UserDTO> CreateUserAsync(CreateUserDTO user)
    {
        var existUser = await _context.Users.FindAsync(user.Id);
        var family = await _context.Families.FindAsync(user.Family.Id);

        if (existUser == null)
        {
            var entity = new User(user.Id, user.Name) { Age = user.Age, Family = family };

            _context.Users.Add(entity);

            await _context.SaveChangesAsync();

            return new UserDTO(
                entity.Id,
                entity.Name
            );
        }

        return new UserDTO(
                existUser.Id,
                existUser.Name
            );
    }

    public async Task<Option<UserDetailsDTO>> ReadDetailedUserByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            return new UserDetailsDTO(user.Id, user.Name, user.Age, user.Family.Name);
        }
        return null;
    }

    public async Task<Option<UserDTO>> ReadUserByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            return new UserDTO(user.Id, user.Name);
        }
        return null;
    }
}