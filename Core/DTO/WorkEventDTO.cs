namespace Core;
public record WorkEventDTO(Guid Id, string ChoreName, string AssignedToName, DateTime CreatedDate);
public record WorkEventDetailedDTO(Guid Id, string ChoreName, string AssignedToName, DateTime CreatedDate, string DoneByName, DateTime DateDone);
public record CreateWorkEventDTO
{
    [Required]
    public UserDTO? AssignedTo { get; set; }

    [Required]
    public ChoreDTO? Chore { get; set; }

    public DateTime CreatedDate { get; set; }
}

public record UpdateWorkEventDTO
{
    public UserDTO? DoneBy { get; set; }
    public DateTime DateDone { get; set; }
}