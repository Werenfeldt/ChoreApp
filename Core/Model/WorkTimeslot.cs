namespace ChoreApp.Core;

public class WorkTimeslot
{
    [Key]
    public Guid Id { get; init; }

    public Duration Duration { get; set; }

    public Guid UserId { get; init; }
    [Required]
    public User User { get; init; }

    public DayOfWeek Weekday { get; set; }

    public WorkTimeslot(Duration duration, DayOfWeek weekday)
    {
        this.Duration = duration;
        this.Weekday = weekday;
    }
}