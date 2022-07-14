
namespace Model;

public class Chore
{
    [Key]
    public Guid Id { get; init; }

    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public Duration Duration { get; set; }

    public Interval Interval { get; set; }

    [DataType(DataType.Date)]
    public DateTime Created { get; init; }

    public Guid? CreatedByUserId { get; init; }

    [ForeignKey("CreatedByUserId")]
    public User? User { get; init; }

    public Guid FamilyId { get; init; }

    [Required]
    public Family Family { get; init; }

    public bool OneTimer { get; set; }

    public ICollection<WorkEvent>? WorkEvent { get; set; }
    public Chore(string name, Duration duration, Interval interval, bool oneTimer = false)
    {
        this.Name = name;
        this.Duration = duration;
        this.Interval = interval;
        this.OneTimer = oneTimer;
    }


}