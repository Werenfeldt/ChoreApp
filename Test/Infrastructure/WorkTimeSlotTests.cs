namespace ChoreApp.Test;

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

    [Fact]
    public async void EditWorkTimeSlot_Given_WorkTimeSlotIdAndWorkTimeSlot_Return_Updated()
    {
        // Given
        CreateTestContext();
        var updatedWorkTimeSlot = new UpdateWorkTimeSlotDTO
        {
            Id = _workTimeSlotId,
            Duration = Duration.FiveMinutes,
            Weekday = DayOfWeek.Sunday
        };

        // When
        var actualResponse = await _workTimeSlotRepository.EditWorkTimeSlotAsync(_workTimeSlotId, updatedWorkTimeSlot);


        // Then
        Assert.Equal(Response.Updated, actualResponse);
    }

    [Fact]
    public async void EditWorkTimeSlot_Given_WorkTimeSlotIdAndWorkTimeSlot_Return_NotFound()
    {
        // Given
        CreateTestContext();
        var updatedWorkTimeSlot = new UpdateWorkTimeSlotDTO
        {
            Id = _workTimeSlotId,
            Duration = Duration.FiveMinutes,
            Weekday = DayOfWeek.Sunday
        };

        // When
        var actualResponse = await _workTimeSlotRepository.EditWorkTimeSlotAsync(Guid.NewGuid(), updatedWorkTimeSlot);


        // Then
        Assert.Equal(Response.NotFound, actualResponse);
    }

    [Fact]
    public async void ReadWorkTimeSlotById_Given_WorkTimeSlotId_Return_WorkTimeSlotDTO()
    {
        // Given
        CreateTestContext();

        // When
        var workTimeSlot = await _workTimeSlotRepository.ReadWorkTimeSlotByIdAsync(_workTimeSlotId);
        var actual = workTimeSlot.Value;

        // Then
        Assert.Equal(_workTimeSlotId, actual.Id);
        Assert.Equal("OneHour", actual.Duration);
        Assert.Equal("Monday", actual.Weekday);
    }

    [Fact]
    public async void ReadWorkTimeSlotById_Given_WorkTimeSlotId_Return_Null()
    {
        // Given
        CreateTestContext();

        // When
        var actual = await _workTimeSlotRepository.ReadWorkTimeSlotByIdAsync(Guid.NewGuid());


        // Then
        Assert.True(actual.IsNone);
    }

    [Fact]
    public async void ReadAllWorkTimeSlotByUserId_Return_ListOfWorkTimeSlots()
    {
        // Given
        CreateTestContext();
        var user = await _userRepository.ReadUserByIdAsync(_userId);
        var secondWorkTimeSlot = await _workTimeSlotRepository.CreateWorkTimeSlotAsync(new CreateWorkTimeSlotDTO
        {
            Duration = Duration.FiveMinutes,
            Weekday = DayOfWeek.Tuesday,
            User = new UserDTO(user.Value.Id, user.Value.Name)
        });

        // When
        var workTimeSlots = await _workTimeSlotRepository.ReadAllWorkTimeSlotByUserIdAsync(_userId);

        // Then
        Assert.Equal(_workTimeSlotId, workTimeSlots.ElementAt(0).Id);
        Assert.Equal(secondWorkTimeSlot.Id, workTimeSlots.ElementAt(1).Id);

        Assert.Equal("Monday", workTimeSlots.ElementAt(0).Weekday);
        Assert.Equal("Tuesday", workTimeSlots.ElementAt(1).Weekday);

        Assert.Equal("OneHour", workTimeSlots.ElementAt(0).Duration);
        Assert.Equal("FiveMinutes", workTimeSlots.ElementAt(1).Duration);
    }


}