using Anime.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_project.Controllers;

[Route("api/user")]
[ApiController]
[Produces("application/json")]
public class UsersController : BaseController
{
    public UsersController(AnimeContext context) : base(context)
    {
    }

    [HttpGet("roles")]
    public async Task<IEnumerable<string>> GetRoles()
    {
        return await GetRolesByUser();
    }
}