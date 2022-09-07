
namespace Test;
public abstract class Setup
{
    protected readonly DbContextOptions<Infrastructure.ChoreAppContext> _contextOption;
    protected readonly ChoreAppContext _context;

    protected readonly SqliteConnection _connection;

    protected readonly ChoreRepository _choreRepository;
    protected readonly UserRepository _userRepository;
    protected readonly FamilyRepository _familyRepository;

    protected readonly Guid _userId;
    protected readonly Guid _choreId;
    protected readonly Guid _familyId;

    public Setup()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOption = new DbContextOptionsBuilder<ChoreAppContext>()
            .UseSqlite(connection)
            .Options;
        var context = new ChoreAppContext(contextOption);

        context.Database.EnsureCreated();


        _context = context;
        _contextOption = contextOption;
        _connection = connection;
        _choreId = Guid.NewGuid();
        _userId = Guid.NewGuid();
        _familyId = Guid.NewGuid();

        _choreRepository = new ChoreRepository(_context);
        _userRepository = new UserRepository(_context);
        _familyRepository = new FamilyRepository(_context);


        //Seeds TestDate
        Seed();
    }

    public void Seed()
    {
        var family = new Family("Nielsen") {Id = _familyId};
        var user = new User(_userId, "Marie Nielsen") { Family = family };
        var chore = new Chore("Støvsug", Duration.TwentyMinutes, Interval.OneWeek)
        {
            Id = _choreId,
            Family = family,
            User = user
        };

        _context.AddRange(
            family,
            user,
            chore
        );
        _context.SaveChanges();
    }

    public ChoreAppContext CreateTestContext() => new ChoreAppContext(_contextOption);

    public void Dispose() => _connection.Dispose();
}