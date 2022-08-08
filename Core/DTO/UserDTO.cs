namespace Core;
public record UserDTO(string Id, string? Name);
public record UserDetailsDTO(string Id, string? Name, int Age, Family FamilyName);
public record CreateUserDTO
{
    
    [StringLength(50)]
    public string? Name { get; set; }

    public int? Age { get; set; }

    [Required]
    public Family? FamilyName { get; set; }
}

public record UpdateUserDTO : CreateUserDTO
{
    public string? Id { get; set; }
    public ICollection<ChoreDTO>? ChoresCreated { get; set; }
    public ICollection<WorkTimeSlotDTO>? WorkTimeslots { get; set; }

    [InverseProperty("DoneByUser")]
    public ICollection<WorkEventDTO>? WorkEventsDone { get; set; }

    [InverseProperty("AssignedToUser")]
    public ICollection<WorkEventDTO>? WorkEventsAssigned { get; set; }
}