namespace Test;

public class ChoreRepositoryTests : Setup, IDisposable
{
    [Fact]
    public async Task CreateChore_GivenChoreDTO_ReturnsChoreDTO()
    {
        // Given
        CreateTestContext();

        var expected = new CreateChoreDTO
        {
            Name = "Vask Gulv",
            Duration = Duration.OneHour,
            Interval = Interval.ThreeWeeks,
            Description = "",
            Created = DateTime.UtcNow,
            CreatedByUserId = _userId,
            FamilyId = _familyId,
            OneTimer = true
        };

        // When
        var actual = await _choreRepository.CreateChoreAsync(expected);

        // Then
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal("OneHour", actual.Duration);
        Assert.Equal("ThreeWeeks", actual.Interval);
        Assert.Equal("", actual.Description);
        Assert.Equal(DateTime.UtcNow, actual.Created, precision: TimeSpan.FromSeconds(5));
        Assert.Equal("Ja", actual.Onetimer);
    }

    [Fact]
    public async Task ReadChoreById_given_choreId_return_ChoreDTO()
    {
        // Given
        CreateTestContext();

        // When
        var chore = await _choreRepository.ReadChoreByIdAsync(_choreId);
        var actual = chore.Value;
        // Then 
        Assert.Equal(_choreId, actual.Id);
        Assert.Equal("Støvsug", actual.Name);
    }

    [Fact]
    public async Task DeleteChore_given_choreId_return_DeletedResponse()
    {
        // Given
        CreateTestContext();

        // When 
        var actual = await _choreRepository.DeleteChoreByIdAsync(_choreId);

        // Then
        Assert.Equal(Response.Deleted, actual);
    }

    [Fact]
    public async Task DeleteChore_given_choreId_return_NotFoundResponse()
    {
        // Given
        CreateTestContext();

        // When 
        var actual = await _choreRepository.DeleteChoreByIdAsync(Guid.NewGuid());

        // Then
        Assert.Equal(Response.NotFound, actual);

    }
    [Fact]
    public async Task EditChore_given_choreId_return_UpdatedResponse()
    {
        // Given
        CreateTestContext();
        var updatedChore = new UpdateChoreDTO
        {
            Id = _choreId,
            Name = "Vask tøj",
            Duration = Duration.TwentyMinutes,
            Interval = Interval.TwoWeeks,
            Description = "",
            OneTimer = false
        };

        // When
        var actualResponse = await _choreRepository.EditChoreAsync(_choreId, updatedChore);
        var actualEntityOption = await _choreRepository.ReadDetailedChoreByIdAsync(_choreId);
        var actualEntity = actualEntityOption.Value;

        // Then
        Assert.Equal(Response.Updated, actualResponse);
        Assert.Equal(_choreId, actualEntity.Id);
        Assert.Equal("Vask tøj", actualEntity.Name);
        Assert.Equal("TwentyMinutes", actualEntity.Duration);
        Assert.Equal("TwoWeeks", actualEntity.Interval);
        Assert.Equal("", actualEntity.Description);
        Assert.Equal("Nej", actualEntity.Onetimer);
    }

    [Fact]
    public async Task EditChore_given_choreId_return_NotFoundResponse()
    {
        // Given
        CreateTestContext();
        var updatedChore = new UpdateChoreDTO
        {
            Id = _choreId,
            Name = "Vask tøj",
            Duration = Duration.TwentyMinutes,
            Interval = Interval.TwoWeeks,
            Description = "",
            OneTimer = false
        };

        // When
        var actualResponse = await _choreRepository.EditChoreAsync(Guid.NewGuid(), updatedChore);

        // Then
        Assert.Equal(Response.NotFound, actualResponse);
    }

    [Fact]
    public async void ReadAllChores_return_ListOfChores()
    {
        // Given
        CreateTestContext();
        var secondeChore = await _choreRepository.CreateChoreAsync(new CreateChoreDTO
        {
            Name = "Vask Gulv",
            Duration = Duration.OneHour,
            Interval = Interval.ThreeWeeks,
            Description = "",
            Created = DateTime.UtcNow,
            CreatedByUserId = _userId,
            FamilyId = _familyId,
            OneTimer = true
        });

        // When
        var chores = await _choreRepository.ReadAllChoresAsync(_familyId);

        // Then
        Assert.Equal(_choreId, chores.ElementAt(0).Id);
        Assert.Equal(secondeChore.Id, chores.ElementAt(1).Id);

        Assert.Equal("Støvsug", chores.ElementAt(0).Name);
        Assert.Equal("Vask Gulv", chores.ElementAt(1).Name);
    }

}