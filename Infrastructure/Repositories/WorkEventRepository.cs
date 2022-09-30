namespace Infrastructure;

public class EventRepository : IWorkEventRepository
{
    public Task<WorkEventDTO> CreateWorkEventAsync(CreateWorkEventDTO workEvent)
    {
        throw new NotImplementedException();
    }

    public Task<Response> DeleteWorkEventByIdAsync(Guid workEvent)
    {
        throw new NotImplementedException();
    }

    public Task<Response> EditWorkEventAsync(Guid workEventId, UpdateWorkEventDTO workEvent)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<WorkEventDTO>> ReadAllWorkEventsForFamilyAsync(Guid familyId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<WorkEventDTO>> ReadAllWorkEventsForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Option<WorkEventDTO>> ReadWorkEventByIdAsync(Guid workEvent)
    {
        throw new NotImplementedException();
    }
}