using Microsoft.AspNetCore.Mvc;
using Anime.Data;
using Anime.Models;

namespace asp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AnimeContext _context;
        private static IWebHostEnvironment? _webHostEnvironment;

        public ImageController(AnimeContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;            
            _webHostEnvironment = webHostEnvironment;
        }

        // POST: api/Image
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostAnimeImage([FromForm] AnimeObjectViewModel animeObject)
        {
            if (_webHostEnvironment == null) return BadRequest();
            
            var fileName = DateTime.Now.Ticks + ".png";
            
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "DataBase\\Images\\Anime", fileName);
            //var webFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "DataBase\\Images\\Anime", fileName);

            await using var fileSteam = new FileStream(filePath, FileMode.Create);
            await animeObject.Image.CopyToAsync(fileSteam);

            return filePath;
        }
    }
}