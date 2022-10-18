namespace ChoreApp.Core;

public record ChoreDTO(Guid Id, string? Name, string Duration, string Interval);

public record ChoreDetailedDTO(Guid Id, string? Name, string Duration, string Interval, string? Description, DateTime Created, string Onetimer);

public record CreateChoreDTO
{

    [StringLength(50)]
    public string Name { get; set; }
    public Duration Duration { get; set; }

    public Interval Interval { get; set; }

    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Created { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public Guid FamilyId { get; set; }
    public bool OneTimer { get; set; }
}

public record UpdateChoreDTO : CreateChoreDTO
{
    public Guid Id { get; set; }

    public ICollection<WorkEvent>? WorkEvent { get; set; }


}

