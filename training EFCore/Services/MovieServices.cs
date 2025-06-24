using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using training_EFCore.models;

namespace training_EFCore.Services
{
    public class MovieService
    {
        private readonly Context context;
        public MovieService(Context context)
        {
            this.context = context;

        }

        public void AddMovie(string name, string description, int genreid)
        {
            var movie = new Movie
            {
                Name = name,
                Description = description,
                GenreID = genreid
            };
            context.Movies.Add(movie);
        }

        public void RemoveMovie(int id)
        {
            var movie = context.Movies.Find(id);
            if (movie != null)
            {
                context.Movies.Remove(movie);
            }

        }

        public void UpdateMovie(int id, string name, string description)
        {
            var movie = context.Movies.Find(id);
            if (movie != null)
            {
                movie.Name = name;
                movie.Description = description;
            }
        }
        public Movie GetMovie(int id) => context.Movies.Find(id);

        public IEnumerable<Movie> GetMovies() =>
    context.Movies.AsNoTracking();

        public IEnumerable<Movie> GetMoviesWithGenre() =>
            context.Movies.Include(m => m.Genre).AsNoTracking();

        public IEnumerable<Movie> GetMoviesWithActors() =>
            context.Movies.Include(m => m.Actors).AsNoTracking();

        public Movie GetMovieWithActors(int id)
        {
            var movie = context.Movies.Find(id);
            if (movie != null)
            {
                context.Entry(movie)
                       .Collection(m => m.Actors)
                       .Load(); 
            }
            return movie;
        }

        public Movie GetMovieWithShowTimes(int id)
        {
            var movie = context.Movies.Find(id);
            if (movie != null)
            {
                context.Entry(movie)
                       .Collection(m => m.ShowTimes)
                       .Load(); 
            }
            return movie;
        }



        public List<Movie> GetMoviesByName(string name) =>
            context.Movies.Where(m => m.Name == name)
                          .Include(m => m.Genre)
                          .Include(m => m.Actors)
                          .AsNoTracking()
                          .ToList();

        public IEnumerable<Movie> GetMoviesByGenre(int genreId) =>
            context.Movies.Where(m => m.GenreID == genreId)
                          .Include(m => m.Genre)
                          .AsNoTracking();

        public int GetTotalMovieCount() =>
            context.Movies.Count();

        public bool IsMovieExist(int id) =>
            context.Movies.Any(m => m.ID == id);

        public void SaveChanges() => context.SaveChanges();

    }

}
