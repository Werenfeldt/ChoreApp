namespace ChoreApp.Infrastructure;

public class ChoreAppContext : DbContext, IChoreAppContext
{
    public DbSet<Family> Families => Set<Family>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Chore> Chores => Set<Chore>();

    public DbSet<WorkEvent> WorkEvents => Set<WorkEvent>();

    public DbSet<WorkTimeslot> WorkTimeslots => Set<WorkTimeslot>();

    public ChoreAppContext(DbContextOptions<ChoreAppContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Chore>()
            .Property(c => c.Duration)
            .HasConversion(new EnumToStringConverter<Duration>());

        modelBuilder
            .Entity<Chore>()
            .Property(c => c.Interval)
            .HasConversion(new EnumToStringConverter<Interval>());

        modelBuilder
            .Entity<WorkTimeslot>()
            .Property(c => c.Duration)
            .HasConversion(new EnumToStringConverter<Duration>());

        modelBuilder
            .Entity<Chore>()
            .HasIndex(p => p.Name)
            .IsUnique();

        modelBuilder
            .Entity<WorkEvent>()
            .HasOne(w => w.AssignedToUser)
            .WithMany(w => w.WorkEventsAssigned)
            .OnDelete(DeleteBehavior.ClientCascade);


        //modelBuilder.Entity<WorkEvent>().HasKey(x => new { x.Id, x.AssignedToUserId, x.ChoreId });
    }
}