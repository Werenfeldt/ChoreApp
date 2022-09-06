namespace Core;

public interface IChoreRepository
{
    Task<ChoreDetailedDTO> CreateChoreAsync(CreateChoreDTO chore);
    Task<IReadOnlyCollection<ChoreDTO>> ReadAllChoresAsync();
    Task<Option<ChoreDTO>> ReadChoreByIdAsync(Guid choreId);
    Task<IReadOnlyCollection<ChoreDTO>> ReadChoresByFamilyIdAsync(Guid familyId);
    Task<Response> EditChoreAsync(Guid choreId, UpdateChoreDTO chore);
    Task<Response> DeleteChoreByIdAsync(Guid choreId);
}