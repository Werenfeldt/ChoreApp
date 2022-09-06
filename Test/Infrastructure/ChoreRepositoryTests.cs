namespace Test;

public class ChoreRepositoryTests : Setup, IDisposable
{
    [Fact]
    public async Task CreateChore_GivenChoreDTO_ReturnsChoreDTO()
    {
        // Given
        CreateTestContext();
        var family = await _context.Families.FirstOrDefaultAsync();
        var user = await _context.Users.FirstOrDefaultAsync();
        
        var expected = new CreateChoreDTO
        {
            Name = "Vask Gulv",
            Duration = Duration.OneHour,
            Interval = Interval.ThreeWeeks, 
            Description = "", 
            Created = DateTime.UtcNow,
            CreatedByUserId = user.Id,
            FamilyId = family.Id, 
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
    public async Task ReadChoreById_given_choreId_return_ChoreDTO(){
        // Given
        CreateTestContext();

        // When
        var chore = await _choreRepository.ReadChoreByIdAsync(_choreId);
        var actual = chore.Value;
        // Then 
        Assert.Equal(_choreId, actual.Id);
        Assert.Equal("Støvsug", actual.Name);
    }

}