namespace Infrastructure;

public class WorkTimeSlotRepository : IWorkTimeSlotRepository
{
    public readonly IChoreAppContext _context;

    public WorkTimeSlotRepository(IChoreAppContext context)
    {
        _context = context;
    }

    public async Task<WorkTimeSlotDTO> CreateWorkTimeSlotAsync(CreateWorkTimeSlotDTO workTimeSlot)
    {
        var user = await _context.Users.FindAsync(workTimeSlot.User.Id);
        var entity = new WorkTimeslot(workTimeSlot.Duration, workTimeSlot.Weekday)
        {
            User = user
        };

        _context.WorkTimeslots.Add(entity);
        await _context.SaveChangesAsync();

        return new WorkTimeSlotDTO(entity.Id, entity.Duration.ToString(), entity.Weekday.ToString());
    }

    public async Task<Response> DeleteWorkTimeSlotByIdAsync(Guid workTimeSlotId)
    {
        var entity = await _context.WorkTimeslots.FindAsync(workTimeSlotId);

        if (entity != null)
        {
            _context.WorkTimeslots.Remove(entity);
            await _context.SaveChangesAsync();
            return Response.Deleted;
        }
        return Response.NotFound;
    }

    public async Task<Response> EditWorkTimeSlotAsync(Guid workTimeSlotId, UpdateWorkTimeSlotDTO workTimeSlot)
    {
        var entity = await _context.WorkTimeslots.FindAsync(workTimeSlotId);

        if (entity != null)
        {
            entity.Duration = workTimeSlot.Duration;
            entity.Weekday = workTimeSlot.Weekday;

            await _context.SaveChangesAsync();
            return Response.Updated;
        }
        return Response.NotFound;
    }

    public async Task<Option<WorkTimeSlotDTO>> ReadWorkTimeSlotByIdAsync(Guid workTimeSlotId)
    {
        var entity = await _context.WorkTimeslots.FindAsync(workTimeSlotId);

        if (entity != null)
        {
            return new WorkTimeSlotDTO(entity.Id, entity.Duration.ToString(), entity.Weekday.ToString());
        }
        return null;
    }
    public async Task<IReadOnlyCollection<WorkTimeSlotDTO>> ReadAllWorkTimeSlotByUserIdAsync(Guid userId)
    {
        var user = await _context.Users.Include(w => w.WorkTimeslots).FirstOrDefaultAsync(u => u.Id == userId);

        return user.WorkTimeslots.Select(w => new WorkTimeSlotDTO(w.Id, w.Duration.ToString(), w.Weekday.ToString())).ToList();
    }

}