namespace ChoreApp.Core;

public interface IChoreRepository
{
    Task<ChoreDetailedDTO> CreateChoreAsync(CreateChoreDTO chore);
    Task<IReadOnlyCollection<ChoreDTO>> ReadAllChoresAsync(Guid familyId);
    Task<Option<ChoreDTO>> ReadChoreByIdAsync(Guid choreId);
    Task<Option<ChoreDetailedDTO>> ReadDetailedChoreByIdAsync(Guid choreId);
    Task<Response> EditChoreAsync(Guid choreId, UpdateChoreDTO chore);
    Task<Response> DeleteChoreByIdAsync(Guid choreId);
}