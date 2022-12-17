using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anime.Data;
using Anime.Models;
using Microsoft.AspNetCore.Authorization;

namespace asp_project.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Authorize]
public class AnimeController : ControllerBase
{
    private readonly AnimeContext _context;
    private static IWebHostEnvironment? _webHostEnvironment;

    public AnimeController(AnimeContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: api/Anime
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AnimeObject>>> GetAnimeObjects(int? genreId)
    {
        return genreId switch
        {
            null => await _context.AnimeObjects.ToListAsync(),
            _ => await _context.AnimeObjects.Where(anime => anime.Genre == genreId).ToListAsync()
        };
    }

    // GET: api/Anime/5
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("{id:int}"), AllowAnonymous]
    public async Task<ActionResult<AnimeObject>> GetAnimeObject(int id)
    {
        var animeObject = await _context.AnimeObjects.FindAsync(id);

        if (animeObject == null)
        {
            return NotFound();
        }

        return animeObject;
    }

    // POST: api/Anime
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost, Authorize(Roles = "user, moderator, superuser")]
    public async Task<ActionResult<AnimeObject>> PostAnimeObject([FromForm] AnimeObjectViewModel viewModel)
    {
        if (_webHostEnvironment == null)
        {
            return StatusCode(415, "Failed to load media file");
        }
            
        var fileName = DateTime.Now.Ticks + ".png";
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Media", fileName);

        await using var fileSteam = new FileStream(filePath, FileMode.Create);
        await viewModel.Image.CopyToAsync(fileSteam);
            
        var newObject = new AnimeObject
        {
            Id = 0,
            Title = viewModel.Title,
            Genre = viewModel.Genre,
            Image = $"/media/{fileName}",
            Description = viewModel.Description,
            Info = viewModel.Info
        };
            
        _context.AnimeObjects.Add(newObject);
        await _context.SaveChangesAsync();

        return StatusCode(401, "Anime was created");
    }

    // PUT: api/Anime/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:int}"), Authorize(Roles = "moderator, superuser")]
    public async Task<IActionResult> PutAnimeObject(int id, AnimeObject animeObject)
    {
        if (id != animeObject.Id)
        {
            return BadRequest();
        }
        _context.Entry(animeObject).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AnimeObjectExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return Ok("Anime was changed");
    }

    // DELETE: api/Anime/5
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id:int}"), Authorize(Roles = "moderator, superuser")]
    public async Task<IActionResult> DeleteAnimeObject(int id)
    {
        var animeObject = await _context.AnimeObjects.FindAsync(id);
        if (animeObject == null)
        {
            return NotFound();
        }

        _context.AnimeObjects.Remove(animeObject);
        await _context.SaveChangesAsync();

        return Ok("Anime was deleted");
    }

    private bool AnimeObjectExists(int id)
    {
        return _context.AnimeObjects.Any(e => e.Id == id);
    }
}