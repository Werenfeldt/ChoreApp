namespace Test;

public class WorkEventRepositoryTests : Setup, IDisposable
{

    [Fact]
    public void CreateWorkEvent_GivenWorkEventDTO_ReturnsWorkEventDTO()
    {
        // Given
        var user = _userRepository.ReadUserByIdAsync(_userId);
        var expected = new CreateWorkEventDTO
        {

        };

        // When

        // Then
    }
}