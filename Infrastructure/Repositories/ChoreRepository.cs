namespace Infrastructure;

public class ChoreRepository : IChoreRepository
{
    public Task<ChoreDTO> CreateChoreAsync(CreateChoreDTO chore)
    {
        throw new NotImplementedException();
    }

    public Task<Response> DeleteChoreByIdAsync(Guid choreId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> EditChoreAsync(Guid choreId, UpdateChoreDTO chore)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ChoreDTO>> ReadAllChoresAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Option<ChoreDTO>> ReadChoreByIdAsync(Guid choreId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ChoreDTO>> ReadChoresByFamilyIdAsync(Guid familyId)
    {
        throw new NotImplementedException();
    }
}