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
    public class AnimeController : ControllerBase
    {
        private readonly AnimeContext _context;

        public AnimeController(AnimeContext context)
        {
            _context = context;
        }

        // GET: api/Anime
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeObject>>> GetAnimeObjects()
        {
            return await _context.AnimeObjects.ToListAsync();
        }

        // GET: api/Anime/5
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
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
        public async Task<ActionResult<AnimeObject>> PostAnimeObject(AnimeObject animeObject)
        {
            _context.AnimeObjects.Add(animeObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimeObject", new { id = animeObject.Id }, animeObject);
        }

        // DELETE: api/Anime/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimeObject(int id)
        {
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
