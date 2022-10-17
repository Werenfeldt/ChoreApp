namespace ChoreApp.Core;

public interface IWorkEventRepository
{
    Task<WorkEventDTO> CreateWorkEventAsync(CreateWorkEventDTO workEvent);
    Task<IReadOnlyCollection<WorkEventDTO>> ReadAllWorkEventsForUserAsync(Guid userId);
    Task<IReadOnlyCollection<WorkEventDTO>> ReadAllWorkEventsForFamilyAsync(Guid familyId);
    Task<Option<WorkEventDetailedDTO>> ReadWorkEventByIdAsync(Guid workEvent);
    Task<Response> EditWorkEventAsync(Guid workEventId, UpdateWorkEventDTO workEvent);
    Task<Response> DeleteWorkEventByIdAsync(Guid workEvent);
}