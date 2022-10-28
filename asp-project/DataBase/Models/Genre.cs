using System;
using System.Collections.Generic;

namespace Anime.Models
{
    public partial class Genre
    {
        public Genre()
        {
            AnimeObjects = new HashSet<AnimeObject>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<AnimeObject> AnimeObjects { get; set; }
    }
}
