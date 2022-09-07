namespace Core;

public interface IFamilyRepository
{
    Task<FamilyDTO> CreateFamilyAsync(CreateFamilyDTO user);
    Task<Option<FamilyDetailsDTO>> ReadFamilyByIdAsync(Guid id);
}