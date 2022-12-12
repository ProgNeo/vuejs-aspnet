using Anime.Data;
using Anime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_project.Controllers;

[Route("api/roles")]
[ApiController]
[Produces("application/json")]
public class RolesController : BaseController
{
    public RolesController(AnimeContext context) : base(context)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        return await _context.Roles
            .OrderBy(i => i.Name)
            .ToListAsync();
    }
}