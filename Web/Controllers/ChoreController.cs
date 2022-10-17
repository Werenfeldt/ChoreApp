namespace ChoreApp.Web.Controllers;
using ChoreApp.Web.Model;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ProjectsController : ControllerBase
{
    //public Func<string> GetObjectId;
    private IChoreRepository _choreRepository;

    public ProjectsController(IChoreRepository repo)
    {
        _choreRepository = repo;
        // GetObjectId = () =>
        //     User.GetObjectId() == null ? "1" : User.GetObjectId();
    }

    [HttpGet]
    public async Task<IEnumerable<ChoreDTO>> Get()
    {
        return await _choreRepository.ReadAllChoresAsync(Guid.Parse("30bc356c-f2cf-42b6-961e-dfd178a50a66"));
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ChoreDTO), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ChoreDTO>> Get(Guid id)
        => (await _choreRepository.ReadChoreByIdAsync(id)).ToActionResult();

    [HttpPost]
    [Authorize(Roles = "Supervisor")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ChoreDTO), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(CreateChoreDTO chore)
    {
        //chore.CreatedByUserId = Guid.Parse(GetObjectId());

        var created = await _choreRepository.CreateChoreAsync(chore);

        return CreatedAtAction(nameof(Get), new { created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateChoreDTO chore)
        => (await _choreRepository.EditChoreAsync(id, chore)).ToActionResult();

    [HttpDelete("{id}")]
    [Authorize(Roles = "Supervisor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
        => (await _choreRepository.DeleteChoreByIdAsync(id)).ToActionResult();
}
