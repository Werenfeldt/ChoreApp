namespace Infrastructure;

public class FamilyRepository : IFamilyRepository
{
    public readonly IChoreAppContext _context;

    public FamilyRepository(IChoreAppContext context)
    {
        _context = context;
    }
    public async Task<FamilyDTO> CreateFamilyAsync(CreateFamilyDTO user)
    {
        var entity = new Family(user.Name)
        {
            Chores = new List<Chore>(),
            Users = new List<User>()
        };

        _context.Families.Add(entity);

        await _context.SaveChangesAsync();

        return new FamilyDTO(
            entity.Id,
            entity.Name
        );
    }

    public async Task<Option<FamilyDetailsDTO>> ReadFamilyByIdAsync(Guid id)
    {
        var family = await _context.Families.Include(f => f.Chores).FirstOrDefaultAsync(f => f.Id == id);
        if (family != null)
        {
            return new FamilyDetailsDTO(
                family.Id,
                family.Name,
                family.Chores.Select(chore => new ChoreDTO(chore.Id, chore.Name)).ToList(),
                family.Users.Select(user => new UserDTO(user.Id, user.Name)).ToList()
                );
        }
        return null;
    }
}