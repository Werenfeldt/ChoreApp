namespace Test;

public class ChoreRepositoryTests : Setup, IDisposable
{
    [Fact]
    public async Task CreateChore_GivenChoreDTO_ReturnsChoreDTO()
    {
        // Given
        var chore = new CreateChoreDTO
        {
            Name = "Støvsug",
            Duration = Duration.FiveMinutes,
            Interval = Interval.TwoDays,
            CreatedByUserId = new Guid(),
            FamilyId = new Guid(),

        };

        // When
        var actual = await _choreRepository.CreateChoreAsync(chore);

        // Then
        Assert.Equal("Støvsug", actual.Name);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}