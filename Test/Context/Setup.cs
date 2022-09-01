namespace Tests;
public abstract class Setup
{
    protected readonly ChoreAppContext _context;
    protected readonly ChoreRepository _projectRepository;
    //protected readonly UserRepository _userRepository;
    public ContextSetup()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ChoreAppContext>();
        builder.UseSqlite(connection);
        var context = new ChoreAppContext(builder.Options);
        context.Database.EnsureCreated();

        _context = context;
        _ChoreRepository = new ChoreRepository(_context);
        //_userRepository = new UserRepository(_context);
    }
}