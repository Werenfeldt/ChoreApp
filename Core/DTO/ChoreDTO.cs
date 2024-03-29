namespace Core;
public record ChoreDTO(Guid Id, string? Name);

public record ChoreDetailedDTO(Guid Id, string? Name, string Duration, string Interval, string? Description, DateTime Created, string Onetimer);

public record CreateChoreDTO
{

    [StringLength(50)]
    public string Name { get; set; }
    public Duration Duration { get; set; }

    public Interval Interval { get; set; }

    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Created { get; init; }

    public Guid? CreatedByUserId { get; init; }

    public Guid FamilyId { get; init; }
    public bool OneTimer { get; set; }
}

public record UpdateChoreDTO : CreateChoreDTO
{
    public Guid Id { get; set; }

    public ICollection<WorkEvent>? WorkEvent { get; set; }


}

