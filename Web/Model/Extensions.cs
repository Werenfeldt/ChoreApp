
namespace ChoreApp.Web.Model;
using ChoreApp.Core;

public static class Extensions
{
    public static IActionResult ToActionResult(this Response response) => response switch
    {
        Response.Updated => new NoContentResult(),
        Response.Deleted => new NoContentResult(),
        Response.NotFound => new NotFoundResult(),
        _ => throw new NotSupportedException($"{response} not supported"),
    };

    public static ActionResult<T> ToActionResult<T>(this Option<T> option) where T : class
        => option.IsSome ? option.Value : new NotFoundResult();
}