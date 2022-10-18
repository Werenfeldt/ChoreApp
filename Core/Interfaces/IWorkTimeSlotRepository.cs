namespace ChoreApp.Core;

public interface IWorkTimeSlotRepository
{
    Task<WorkTimeSlotDTO> CreateWorkTimeSlotAsync(CreateWorkTimeSlotDTO workTimeSlot);
    Task<IReadOnlyCollection<WorkTimeSlotDTO>> ReadAllWorkTimeSlotByUserIdAsync(Guid userId);
    Task<Option<WorkTimeSlotDTO>> ReadWorkTimeSlotByIdAsync(Guid workTimeSlotId);

    Task<Response> EditWorkTimeSlotAsync(Guid workTimeSlotId, UpdateWorkTimeSlotDTO workTimeSlot);
    Task<Response> DeleteWorkTimeSlotByIdAsync(Guid workTimeSlotId);
}