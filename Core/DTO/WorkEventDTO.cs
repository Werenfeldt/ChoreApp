namespace Core;
public record WorkEventDTO(Guid Id, ChoreDTO Chore, string DateDone, UserDTO AssignedTo, UserDTO DoneBy);

public record CreateWorkEventDTO
{
    [Required]
    public UserDTO? AssignedTo { get; set; }

    [Required]
    public ChoreDTO? Chore { get; set; }

    public DateTime CreatedDate { get; set; }
}

public record UpdateWorkEventDTO : CreateWorkEventDTO
{
    public UserDTO? DoneBy { get; set; }
}