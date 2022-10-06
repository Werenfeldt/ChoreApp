namespace Test;

public class FamilyRepositoryTests : Setup, IDisposable
{
    [Fact]
    public async Task CreateFamily_given_CreateFamilyDTO_returns_FamilyDTO()
    {
        // Given
        CreateTestContext();
        var family = new CreateFamilyDTO
        {
            Name = "Hansen",
        };

        var expected = new FamilyDTO(Guid.NewGuid(), "Hansen");

        // When
        var actual = await _familyRepository.CreateFamilyAsync(family);

        // Then
        Assert.Equal(expected.Name, actual.Name);
    }

    [Fact]
    public async Task ReadFamilyByIdAsync_given_Id_returns_FamilyDTO()
    {
        // Given
        CreateTestContext();

        var expected = new FamilyDetailsDTO(_familyId, "Nielsen", new List<ChoreDTO>() { new ChoreDTO(_choreId, "Støvsug") }, new List<UserDTO>() { new UserDTO(_userId, "Marie Nielsen") });

        // When
        var actual = await _familyRepository.ReadFamilyByIdAsync(_familyId);

        // Then
        Assert.Equal(expected.Id, actual.Value.Id);
        Assert.Equal(expected.Name, actual.Value.Name);
        Assert.Equal(expected.Chores, actual.Value.Chores);
        Assert.Equal(expected.FamilyMembers, actual.Value.FamilyMembers);
        Assert.True((await _userRepository.ReadUserByIdAsync(Guid.NewGuid())).IsNone);
    }
}