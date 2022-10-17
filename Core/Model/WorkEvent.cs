namespace ChoreApp.Core;

public class WorkEvent
{
    [Key]
    public Guid Id { get; init; }
    public Guid ChoreId { get; init; }
    public Guid AssignedToUserId { get; init; }

    public Guid? DoneByUserId { get; set; }

    [ForeignKey("DoneByUserId")]
    public User? DoneByUser { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateDone { get; set; }

    [Required]
    [ForeignKey("AssignedToUserId")]
    public User AssignedToUser { get; init; }
    [Required]
    public Chore Chore { get; init; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; init; }

    public WorkEvent() { }

}