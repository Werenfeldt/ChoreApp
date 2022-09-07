namespace Core;
public record UserDTO(Guid Id, string? Name);
public record UserDetailsDTO(Guid Id, string? Name, int? Age, string FamilyName);
public record CreateUserDTO
{
    public Guid Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public int? Age { get; set; }

    [Required]
    public FamilyDTO? Family { get; set; }
}

public record UpdateUserDTO : CreateUserDTO
{

    public ICollection<ChoreDTO>? ChoresCreated { get; set; }
    public ICollection<WorkTimeSlotDTO>? WorkTimeslots { get; set; }

    [InverseProperty("DoneByUser")]
    public ICollection<WorkEventDTO>? WorkEventsDone { get; set; }

    [InverseProperty("AssignedToUser")]
    public ICollection<WorkEventDTO>? WorkEventsAssigned { get; set; }
}