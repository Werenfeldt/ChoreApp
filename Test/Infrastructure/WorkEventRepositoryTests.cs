namespace ChoreApp.Test;

public class WorkEventRepositoryTests : Setup, IDisposable
{

    [Fact]
    public async void CreateWorkEvent_GivenWorkEventDTO_ReturnsWorkEventDTO()
    {
        // Given
        CreateTestContext();
        var user = await _userRepository.ReadUserByIdAsync(_userId);
        var chore = await _choreRepository.ReadChoreByIdAsync(_choreId);
        var expected = new CreateWorkEventDTO
        {
            AssignedTo = user,
            Chore = chore,
            CreatedDate = DateTime.UtcNow
        };

        // When
        var actual = await _workEventRepository.CreateWorkEventAsync(expected);

        // Then
        Assert.Equal(expected.Chore.Name, actual.ChoreName);
        Assert.Equal(expected.AssignedTo.Name, actual.AssignedToName);
        Assert.Equal(DateTime.UtcNow, actual.CreatedDate, precision: TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async void ReadWorkEventById_given_workEventId_return_workEventDTO()
    {
        // Given
        CreateTestContext();

        // When
        var workEvent = await _workEventRepository.ReadWorkEventByIdAsync(_workEventId);
        var actual = workEvent.Value;

        // Then
        Assert.Equal(_workEventId, actual.Id);
        Assert.Equal("Støvsug", actual.ChoreName);
        Assert.Equal("Marie Nielsen", actual.AssignedToName);
        Assert.Equal(DateTime.UtcNow, actual.CreatedDate, precision: TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async void ReadWorkEventById_given_NonExistId_return_null()
    {
        // Given

        // When
        var actual = await _workEventRepository.ReadWorkEventByIdAsync(Guid.NewGuid());

        // Then
        Assert.Equal(null, actual);
    }

    [Fact]
    public async void ReadAllWorkEventsByFamilyId_given_familyId_return_ListOfWorkEvents()
    {
        // Given
        CreateTestContext();
        var family = await _familyRepository.ReadFamilyByIdAsync(_familyId);
        var newUser = await _userRepository.CreateUserAsync(new CreateUserDTO { Name = "Sofie Lund", Age = 44, Family = new FamilyDTO(family.Value.Id, family.Value.Name) });
        var chore = await _choreRepository.CreateChoreAsync(new CreateChoreDTO
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
        var newChore = await _choreRepository.ReadChoreByIdAsync(chore.Id);

        var secondWorkEvent = await _workEventRepository.CreateWorkEventAsync(new CreateWorkEventDTO
        {
            AssignedTo = newUser,
            Chore = newChore,
            CreatedDate = DateTime.UtcNow.AddDays(2)
        });

        // When
        var workEvents = await _workEventRepository.ReadAllWorkEventsForFamilyAsync(_familyId);

        // Then
        Assert.Equal(_workEventId, workEvents.ElementAt(0).Id);
        Assert.Equal(secondWorkEvent.Id, workEvents.ElementAt(1).Id);

        Assert.Equal("Marie Nielsen", workEvents.ElementAt(0).AssignedToName);
        Assert.Equal("Sofie Lund", workEvents.ElementAt(1).AssignedToName);

        Assert.Equal("Støvsug", workEvents.ElementAt(0).ChoreName);
        Assert.Equal("Vask Gulv", workEvents.ElementAt(1).ChoreName);

        Assert.Equal(DateTime.UtcNow, workEvents.ElementAt(0).CreatedDate, precision: TimeSpan.FromSeconds(5));
        Assert.Equal(DateTime.UtcNow.AddDays(2), workEvents.ElementAt(1).CreatedDate, precision: TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async void ReadAllWorkEventsByUserId_GivenUserId_Return_ListOfWorkEvents()
    {
        // Given
        CreateTestContext();
        var user = await _userRepository.ReadUserByIdAsync(_userId);
        var chore = await _choreRepository.CreateChoreAsync(new CreateChoreDTO
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
        var newChore = await _choreRepository.ReadChoreByIdAsync(chore.Id);

        var secondWorkEvent = await _workEventRepository.CreateWorkEventAsync(new CreateWorkEventDTO
        {
            AssignedTo = user,
            Chore = newChore,
            CreatedDate = DateTime.UtcNow.AddDays(2)
        });

        // When
        var workEvents = await _workEventRepository.ReadAllWorkEventsForUserAsync(_userId);

        // Then
        Assert.Equal(_workEventId, workEvents.ElementAt(0).Id);
        Assert.Equal(secondWorkEvent.Id, workEvents.ElementAt(1).Id);

        Assert.Equal("Marie Nielsen", workEvents.ElementAt(0).AssignedToName);
        Assert.Equal("Marie Nielsen", workEvents.ElementAt(1).AssignedToName);

        Assert.Equal("Støvsug", workEvents.ElementAt(0).ChoreName);
        Assert.Equal("Vask Gulv", workEvents.ElementAt(1).ChoreName);

        Assert.Equal(DateTime.UtcNow, workEvents.ElementAt(0).CreatedDate, precision: TimeSpan.FromSeconds(5));
        Assert.Equal(DateTime.UtcNow.AddDays(2), workEvents.ElementAt(1).CreatedDate, precision: TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async void DeleteWorkEvent_given_workEventId_return_DeletedResponse()
    {
        // Given
        CreateTestContext();

        // When
        var actual = await _workEventRepository.DeleteWorkEventByIdAsync(_workEventId);

        // Then
        Assert.Equal(Response.Deleted, actual);
    }

    [Fact]
    public async void DeleteWorkEvent_given_nonExistingWorkEventId_return_NotFoundResponse()
    {
        // Given
        CreateTestContext();

        // When
        var actual = await _workEventRepository.DeleteWorkEventByIdAsync(Guid.NewGuid());

        // Then
        Assert.Equal(Response.NotFound, actual);
    }

    [Fact]
    public async void EditWorkEvent_given_workEventId_return_UpdatedResponse()
    {
        // Given
        CreateTestContext();
        var user = await _userRepository.ReadUserByIdAsync(_userId);
        var chore = await _choreRepository.ReadChoreByIdAsync(_choreId);
        var updatedWorkEvent = new UpdateWorkEventDTO
        {
            DoneBy = user,
            DateDone = DateTime.UtcNow
        };
        // When
        var actualResponse = await _workEventRepository.EditWorkEventAsync(_workEventId, updatedWorkEvent);
        var actualEntityOption = await _workEventRepository.ReadWorkEventByIdAsync(_workEventId);
        var actualEntity = actualEntityOption.Value;

        // Then
        Assert.Equal(Response.Updated, actualResponse);
        Assert.Equal(_workEventId, actualEntity.Id);
        Assert.Equal("Støvsug", actualEntity.ChoreName);
        Assert.Equal("Marie Nielsen", actualEntity.AssignedToName);
        Assert.Equal(DateTime.UtcNow, actualEntity.CreatedDate, precision: TimeSpan.FromSeconds(5));
        Assert.Equal("Marie Nielsen", actualEntity.DoneByName);
        Assert.Equal(DateTime.UtcNow, actualEntity.DateDone, precision: TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async void EditWorkEvent_given_NonExistingWorkEventId_return_NotFoundResponse()
    {
        // Given
        CreateTestContext();
        var user = await _userRepository.ReadUserByIdAsync(_userId);
        var chore = await _choreRepository.ReadChoreByIdAsync(_choreId);
        var updatedWorkEvent = new UpdateWorkEventDTO
        {
            DoneBy = user,
            DateDone = DateTime.UtcNow
        };
    
        // When
        var actualResponse = await _workEventRepository.EditWorkEventAsync(Guid.NewGuid(), updatedWorkEvent);
    
        // Then
        Assert.Equal(Response.NotFound, actualResponse);
    }
}