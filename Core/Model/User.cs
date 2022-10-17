namespace ChoreApp.Core;

public class User
{
    [Key]
    public Guid Id { get; init; }

    [StringLength(50)]
    public string Name { get; init; }

    public int? Age { get; set; }

    public Guid FamilyId { get; init; }
    [Required]
    public Family Family { get; init; }

    public ICollection<Chore>? ChoresCreated { get; set; }
    public ICollection<WorkTimeslot>? WorkTimeslots { get; set; }
    
    [InverseProperty("DoneByUser")]
    public ICollection<WorkEvent>? WorkEventsDone { get; set; }

    [InverseProperty("AssignedToUser")]
    public ICollection<WorkEvent>? WorkEventsAssigned { get; set; }
    public User(Guid Id, string name)
    {
        this.Id = Id;
        this.Name = name;
    }
}