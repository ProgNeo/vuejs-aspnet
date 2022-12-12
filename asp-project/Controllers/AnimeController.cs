using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anime.Data;
using Anime.Models;

namespace asp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : BaseController
    {
        private static IWebHostEnvironment? _webHostEnvironment;

        public AnimeController(AnimeContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Anime
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeObject>>> GetAnimeObjects(int? genreId)
        {
            return genreId switch
            {
                null => await _context.AnimeObjects.ToListAsync(),
                _ => await _context.AnimeObjects.Where(anime => anime.Genre == genreId).ToListAsync()
            };
        }

        // GET: api/Anime/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AnimeObject>> GetAnimeObject(int id)
        {
            var animeObject = await _context.AnimeObjects.FindAsync(id);

            if (animeObject == null)
            {
                return NotFound();
            }

            return animeObject;
        }

        // PUT: api/Anime/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAnimeObject(int id, AnimeObject animeObject)
        {
            if (!CheckSession())
            {
                return new JsonResult(new BaseResponse(false, "Access denied"));
            }
            
            var roles = await GetRolesByUser();
            
            if (!(roles.Contains(Roles.Moderator.ToString()) || roles.Contains(Roles.Superuser.ToString())))
            {
                return new JsonResult(new BaseResponse(false, "Access denied"));
            }
            
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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Anime
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnimeObject>> PostAnimeObject([FromForm] AnimeObjectViewModel viewModel)
        {
            if (!CheckSession())
            {
                return new JsonResult(new BaseResponse(false, "Access denied"));
            }
            
            var roles = await GetRolesByUser();
            
            if (!(roles.Contains(Roles.Moderator.ToString()) || roles.Contains(Roles.Superuser.ToString())))
            {
                return new JsonResult(new BaseResponse(false, "Access denied"));
            }

            if (_webHostEnvironment == null) return BadRequest();
            
            var fileName = DateTime.Now.Ticks + ".png";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Media", fileName);

            await using var fileSteam = new FileStream(filePath, FileMode.Create);
            await viewModel.Image.CopyToAsync(fileSteam);
            
            var newObject = new AnimeObject
            {
                Id = 0,
                Title = viewModel.Title,
                Genre = viewModel.Genre,
                Image = $"{Request.Scheme}://{Request.Host}/media/{fileName}",
                Description = viewModel.Description,
                Info = viewModel.Info
            };
            
            _context.AnimeObjects.Add(newObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetAnimeObject", 
                new { id = newObject.Id }, 
                newObject
            );
        }

        // DELETE: api/Anime/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnimeObject(int id)
        {
            if (!CheckSession())
            {
                return new JsonResult(new BaseResponse(false, "Access denied"));
            }
            
            var roles = await GetRolesByUser();
            
            if (!(roles.Contains(Roles.Moderator.ToString()) || roles.Contains(Roles.Superuser.ToString())))
            {
                return new JsonResult(new BaseResponse(false, "Access denied"));
            }
            
            var animeObject = await _context.AnimeObjects.FindAsync(id);
            if (animeObject == null)
            {
                return NotFound();
            }

            _context.AnimeObjects.Remove(animeObject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimeObjectExists(int id)
        {
            return (_context.AnimeObjects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
