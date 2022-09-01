namespace Infrastructure;

public class ChoreRepository : IChoreRepository
{
    public readonly IChoreAppContext _context;

    public ChoreRepository(IChoreAppContext context)
    {
        _context = context;
    }
    public async Task<ChoreDTO> CreateChoreAsync(CreateChoreDTO chore)
    {
        var entity = new Chore(chore.Name, chore.Duration, chore.Interval, chore.OneTimer)
        {
            Description = chore.Description, 
            Created = DateTime.UtcNow,
            CreatedByUserId = chore.CreatedByUserId,
            FamilyId = chore.FamilyId,
        };

        _context.Chores.Add(entity);
        await _context.SaveChangesAsync();

        return new ChoreDTO(entity.Id, entity.Name);
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