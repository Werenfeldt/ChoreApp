namespace Infrastructure;

public class WorkEventRepository : IWorkEventRepository
{
    public readonly IChoreAppContext _context;

    public WorkEventRepository(IChoreAppContext context)
    {
        _context = context;
    }
    public async Task<WorkEventDTO> CreateWorkEventAsync(CreateWorkEventDTO workEvent)
    {
        var chore = await _context.Chores.FindAsync(workEvent.Chore.Id);
        var assginedTo = await _context.Users.FindAsync(workEvent.AssignedTo.Id);
        var entity = new WorkEvent()
        {
            AssignedToUser = assginedTo,
            Chore = chore,
            CreatedDate = workEvent.CreatedDate
        };

        _context.WorkEvents.Add(entity);
        await _context.SaveChangesAsync();

        return new WorkEventDTO(entity.Id, entity.Chore.Name, entity.AssignedToUser.Name, entity.CreatedDate);
    }

    public async Task<Response> DeleteWorkEventByIdAsync(Guid workEvent)
    {
        var entity = await _context.WorkEvents.FindAsync(workEvent);

        if (entity != null)
        {
            _context.WorkEvents.Remove(entity);
            await _context.SaveChangesAsync();
            return Response.Deleted;
        }
        return Response.NotFound;
    }

    public async Task<Response> EditWorkEventAsync(Guid workEventId, UpdateWorkEventDTO workEvent)
    {
        var entity = await _context.WorkEvents.FindAsync(workEventId);
        var user = await _context.Users.FindAsync(workEvent.DoneBy.Id);
        if (entity != null)
        {
            entity.DateDone = workEvent.DateDone;
            entity.DoneByUser = user;

            await _context.SaveChangesAsync();
            return Response.Updated;
        }
        return Response.NotFound;
    }

    public async Task<IReadOnlyCollection<WorkEventDTO>> ReadAllWorkEventsForFamilyAsync(Guid familyId)
    {
        var family = await _context.Families.Include(f => f.Users).ThenInclude(u => u.WorkEventsAssigned).FirstOrDefaultAsync(f => f.Id == familyId);

        return family.Users.SelectMany(u => u.WorkEventsAssigned.Select(w => new WorkEventDTO(w.Id, w.Chore.Name, w.AssignedToUser.Name, w.CreatedDate))).ToList();
    }

    public async Task<IReadOnlyCollection<WorkEventDTO>> ReadAllWorkEventsForUserAsync(Guid userId)
    {
        var user = await _context.Users.Include(u => u.WorkEventsAssigned).FirstOrDefaultAsync(u => u.Id == userId);

        return user.WorkEventsAssigned.Select(w => new WorkEventDTO(w.Id, w.Chore.Name, w.AssignedToUser.Name, w.CreatedDate)).ToList();
    }

    public async Task<Option<WorkEventDetailedDTO>> ReadWorkEventByIdAsync(Guid workEvent)
    {
        var entity = await _context.WorkEvents.FindAsync(workEvent);

        if (entity != null)
        {
            return new WorkEventDetailedDTO(entity.Id, entity.Chore.Name, entity.AssignedToUser.Name, entity.CreatedDate, entity.DoneByUser.Name, entity.DateDone);
        }

        return null;
    }
}