namespace Core;
public record FamilyDTO(Guid Id, string? Name);

public record FamilyDetailsDTO(Guid Id, string? Name, IReadOnlyCollection<ChoreDTO>? Chores, IReadOnlyCollection<UserDTO>? FamilyMembers);

public record CreateFamilyDTO
{
    [StringLength(50)]
    public string? Name { get; set; }
}

public record UpdateFamilyDTO : CreateFamilyDTO
{
    public string? Id { get; set; }

    public ICollection<ChoreDTO>? Chores { get; set; }

    public ICollection<UserDTO>? FamilyMembers { get; set; }
}