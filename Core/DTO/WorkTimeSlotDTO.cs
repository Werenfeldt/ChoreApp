namespace Core;
public record WorkTimeSlotDTO(Guid Id, string Duration, string Weekday);

public record CreateWorkTimeSlotDTO
{
    public Duration Duration { get; set; }

    [Required]
    public UserDTO? User { get; set; }

    public DayOfWeek Weekday { get; set; }


}

public record UpdateWorkTimeSlotDTO : CreateWorkTimeSlotDTO
{
    public Guid Id { get; set; }
}