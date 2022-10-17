namespace ChoreApp.Test;

public class UserRepositoryTests : Setup, IDisposable
{
    [Fact]
    public async Task CreateUser_given_CreateUserDTO_returns_UserDTO()
    {
        // Given
        CreateTestContext();
        var id = Guid.NewGuid();
        var family = await _context.Families.FirstOrDefaultAsync();
        var user = new CreateUserDTO
        {
            Id = id,
            Name = "Hanne",
            Age = 45,
            Family = new FamilyDTO(family.Id, family.Name)
        };

        var expected = new UserDTO(id, "Hanne");

        // When
        var actual = await _userRepository.CreateUserAsync(user);

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(new UserDTO(id, "Hanne"), await _userRepository.CreateUserAsync(user));
    }

    [Fact]
    public async Task ReadDetailedUserByIdAsync_given_Id_returns_UserDTO()
    {
        // Given
        CreateTestContext();
        var id = Guid.NewGuid();
        var family = await _context.Families.FirstOrDefaultAsync();

        await _userRepository.CreateUserAsync(new CreateUserDTO
        {
            Id = id,
            Name = "Hanne",
            Age = 45,
            Family = new FamilyDTO(family.Id, family.Name)
        });

        var expected = new UserDetailsDTO(id, "Hanne", 45, "Nielsen");

        // When
        var actual = await _userRepository.ReadDetailedUserByIdAsync(id);

        // Then
        Assert.Equal(expected, actual.Value);
        Assert.True((await _userRepository.ReadUserByIdAsync(Guid.NewGuid())).IsNone);
    }

    [Fact]
    public async Task ReadUserByIdAsync_given_Id_returns_UserDTO()
    {
        // Given
        CreateTestContext();
        var id = Guid.NewGuid();
        var family = await _context.Families.FirstOrDefaultAsync();

        await _userRepository.CreateUserAsync(new CreateUserDTO
        {
            Id = id,
            Name = "Hanne",
            Age = 45,
            Family = new FamilyDTO(family.Id, family.Name)
        });

        var expected = new UserDTO(id, "Hanne");

        // When
        var actual = await _userRepository.ReadUserByIdAsync(id);

        // Then
        Assert.Equal(expected, actual.Value);
        Assert.True((await _userRepository.ReadUserByIdAsync(Guid.NewGuid())).IsNone);
    }
}