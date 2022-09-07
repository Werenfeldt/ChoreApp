namespace Test;

public class WorkTimeSlotRepositoryTests : Setup, IDisposable
{
    [Fact]
    public async Task CreateWorkTimeSlot_Given_CreateWorkTimeSlotDTO_Returns_WorkTimeSlotDTO()
    {
        // Given
        CreateTestContext();

        var expected = new CreateWorkTimeSlotDTO
        {
            Duration = Duration.OneHour,
            User = new UserDTO(_userId, "Marie Nielsen"),
            Weekday = DayOfWeek.Monday
        };

        // When
        var actual = await _workTimeSlotRepository.CreateWorkTimeSlotAsync(expected);

        // Then
        Assert.Equal("OneHour", actual.Duration);
        Assert.Equal("Monday", actual.Weekday);
    }

    [Fact]
    public async void DeleteWorkTimeSlot_Given_WorkTimeSlotId_Return_DeletedResponse()
    {
        // Given
        CreateTestContext();

        // When
        var actual = await _workTimeSlotRepository.DeleteWorkTimeSlotByIdAsync(_workTimeSlotId);

        // Then
        Assert.Equal(Response.Deleted, actual);
    }

    [Fact]
    public async void DeleteWorkTimeSlot_Given_WorkTimeSlotId_Return_NotFoundResponse()
    {
        // Given
        CreateTestContext();

        // When
        var actual = await _workTimeSlotRepository.DeleteWorkTimeSlotByIdAsync(Guid.NewGuid());

        // Then
        Assert.Equal(Response.NotFound, actual);
    }


}