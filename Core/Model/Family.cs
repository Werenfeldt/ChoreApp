namespace Core;

public class Family
{
    [Key]
    public Guid Id { get; init; }

    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<Chore>? Chores {get; set;}

    public ICollection<User>? Users {get; set;}
    public Family(string name)
    {
        this.Name = name;
    }
}