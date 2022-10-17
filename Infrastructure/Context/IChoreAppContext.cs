namespace ChoreApp.Infrastructure;
public interface IChoreAppContext : IDisposable
{
    DbSet<Family> Families { get; }
    DbSet<User> Users { get; }
    DbSet<Chore> Chores { get; }
    DbSet<WorkEvent> WorkEvents { get; }
    DbSet<WorkTimeslot> WorkTimeslots { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}