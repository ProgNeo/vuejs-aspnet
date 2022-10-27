using System;
using System.Collections.Generic;

namespace Anime.Models
{
    public partial class AnimeObject
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Genre { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Info { get; set; }

        public virtual Genre? GenreNavigation { get; set; }
    }
}
