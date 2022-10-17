namespace ChoreApp.Infrastructure;

public class ChoreRepository : IChoreRepository
{
    public readonly IChoreAppContext _context;

    public ChoreRepository(IChoreAppContext context)
    {
        _context = context;
    }
    public async Task<ChoreDetailedDTO> CreateChoreAsync(CreateChoreDTO chore)
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

        return new ChoreDetailedDTO(entity.Id, entity.Name, entity.Duration.ToString(), entity.Interval.ToString(), entity.Description, entity.Created, entity.OneTimer ? "Ja" : "Nej");
    }

    public async Task<Response> DeleteChoreByIdAsync(Guid choreId)
    {
        var entity = await _context.Chores.FindAsync(choreId);

        if (entity != null)
        {
            _context.Chores.Remove(entity);
            await _context.SaveChangesAsync();
            return Response.Deleted;
        }
        return Response.NotFound;
    }

    public async Task<Response> EditChoreAsync(Guid choreId, UpdateChoreDTO chore)
    {
        var entity = await _context.Chores.FindAsync(choreId);

        if (entity != null)
        {
            entity.Name = chore.Name;
            entity.Description = chore.Description;
            entity.Duration = chore.Duration;
            entity.Interval = chore.Interval;
            entity.OneTimer = chore.OneTimer;

            await _context.SaveChangesAsync();
            return Response.Updated;
        }
        return Response.NotFound;

    }

    public async Task<IReadOnlyCollection<ChoreDTO>> ReadAllChoresAsync(Guid familyId)
    {
        var family = await _context.Families.Include(f => f.Chores).FirstOrDefaultAsync(f => f.Id ==familyId);
        
        return family.Chores.Select(c => new ChoreDTO(c.Id, c.Name, c.Duration.ToString(), c.Interval.ToString())).ToList();
    }

    public async Task<Option<ChoreDTO>> ReadChoreByIdAsync(Guid choreId)
    {
        var entity = await _context.Chores.FindAsync(choreId);

        if (entity != null)
        {
            return new ChoreDTO(entity.Id, entity.Name, entity.Duration.ToString(), entity.Interval.ToString());
        }
        return null;
    }

    public async Task<Option<ChoreDetailedDTO>> ReadDetailedChoreByIdAsync(Guid choreId)
    {
        var entity = await _context.Chores.FindAsync(choreId);

        if (entity != null)
        {
            return new ChoreDetailedDTO(entity.Id, entity.Name, entity.Duration.ToString(), entity.Interval.ToString(), entity.Description, entity.Created, entity.OneTimer ? "Ja" : "Nej");
        }
        return null;
    }
}