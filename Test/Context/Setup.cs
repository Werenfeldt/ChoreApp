
namespace Test;
public abstract class Setup
{
    protected readonly ChoreAppContext _context;
    protected readonly ChoreRepository _choreRepository;
    protected readonly UserRepository _userRepository;
    public Setup()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChoreAppContext>();
        builder.UseSqlite(connection);
        var context = new ChoreAppContext(builder.Options);
        context.Database.EnsureCreated();

        //Seeds TestDate


        _context = context;
        _choreRepository = new ChoreRepository(_context);
        _userRepository = new UserRepository(_context);
    }

    public void Seed()
    {
        var family = new Family { Id = new Guid(), Name = "Nielsen" };
        var user = new User { Name = "Marie Nielsen", FamilyId = family.Id };

        _context.AddRange(
            family,
            user
        );
        _context.SaveChanges();
    }
}