using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using training_EFCore.models;

namespace training_EFCore.Services
{
    public class GenreService
    {
        private readonly Context context;

        public GenreService(Context context)
        {
            this.context = context;
        }

        public void AddGenre(string name)
        {
            var genre = new Genre
            {
                Name = name
            };
            context.Genres.Add(genre);
        }

        public void RemoveGenre(int id)
        {
            var genre = context.Genres.Find(id);
            if (genre != null)
            {
                context.Genres.Remove(genre);
            }
        }

        public void UpdateGenre(int id, string name)
        {
            var genre = context.Genres.Find(id);
            if (genre != null)
            {
                genre.Name = name;
            }
        }

        public Genre GetGenre(int id) => context.Genres.Find(id);

        public IEnumerable<Genre> GetAllGenres() =>context.Genres.AsNoTracking();
        public Genre GetGenreWithMovies_Explicit(int id)
        {
            var genre = context.Genres.Find(id); 
            if (genre != null)
            {
                context.Entry(genre)
                       .Collection(g => g.Movies)
                       .Load(); 
            }
            return genre;
        }

        public bool IsGenreExist(int id) =>context.Genres.Any(g => g.ID == id);
        public int GetTotalGenreCount() =>context.Genres.Count();



        public void SaveChanges() => context.SaveChanges();

    }
}
