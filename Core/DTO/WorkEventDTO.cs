namespace Core;
public record WorkEventDTO(Guid Id, Guid ChoreId, DateTime DateDone);

public record CreateWorkEventDTO
{

}

public record UpdateWorkEventDTO : CreateWorkEventDTO
{
}