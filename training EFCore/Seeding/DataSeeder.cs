using System;
using System.Collections.Generic;
using training_EFCore.Services;
using training_EFCore.models;
using Microsoft.EntityFrameworkCore;

namespace training_EFCore.Seeding
{
    public class DataSeeder
    {
        private readonly Context context;
        private readonly GenreService genreService;
        private readonly MovieService movieService;
        private readonly ActorService actorService;
        private readonly ShowTimeService showTimeService;
        private readonly TicketService ticketService;

        public DataSeeder(Context context)
        {
            this.context = context;
               
            genreService = new GenreService(context);
            movieService = new MovieService(context);
            actorService = new ActorService(context);
            showTimeService = new ShowTimeService(context);
            ticketService = new TicketService(context);
        }
        public void SeedAll()
        {
            var genreIds = SeedGenres();
            var movieIds = SeedMovies(genreIds);
            var actorIds = SeedActors();
            SeedMovieActors(movieIds, actorIds);
            var showTimeIds = SeedShowTimes(movieIds);
            SeedTickets(showTimeIds);
            context.SaveChanges();
        }

        private List<int> SeedGenres()
        {
            var genres = new List<string> { "Action", "Drama", "Comedy", "Sci-Fi", "Horror" };
            foreach (var name in genres)
                genreService.AddGenre(name);
            return context.Genres.AsNoTracking().Select(g => g.ID).ToList();
        }

        private List<int> SeedMovies(List<int> genreIds)
        {
            var movieIds = new List<int>();
            for (int i = 1; i <= 20; i++)
            {
                string name = $"Movie {i}";
                string desc = $"Description of Movie {i}";
                int genreId = genreIds[i % genreIds.Count];
                movieService.AddMovie(name, desc, genreId);
            }
            return context.Movies.AsNoTracking().Select(m => m.ID).ToList();
        }

        private List<int> SeedActors()
        {
            for (int i = 1; i <= 15; i++)
            {
                string name = $"Actor {i}";
                int age = 20 + i;
                actorService.AddActor(name, age);
            }

            return context.Actors.AsNoTracking().Select(a => a.ID).ToList();
        }

        private void SeedMovieActors(List<int> movieIds, List<int> actorIds)
        {
            var rnd = new Random();

            foreach (var movieId in movieIds)
            {
                var movie = context.Movies.Find(movieId);
                movie.Actors = new List<Actor>();

                for (int i = 0; i < 3; i++)
                {
                    var actorId = actorIds[rnd.Next(actorIds.Count)];
                    var actor = context.Actors.Find(actorId);
                    if (!movie.Actors.Contains(actor))
                        movie.Actors.Add(actor);
                }
            }

            context.SaveChanges();
        }

        private List<int> SeedShowTimes(List<int> movieIds)
        {
            var showTimeIds = new List<int>();
            var random = new Random();

            for (int i = 1; i <= 25; i++)
            {
                DateTime startTime = DateTime.Now.AddDays(i).AddHours(i);
                int movieId = movieIds[random.Next(movieIds.Count)];
                showTimeService.AddShowTime(startTime, movieId);
            }

            return context.ShowTimes.AsTracking().Select(s => s.ID).ToList();
        }

        private void SeedTickets(List<int> showTimeIds)
        {
            var random = new Random();

            for (int i = 1; i <= 100; i++)
            {
                int seat = random.Next(1, 201);
                string customer = $"Customer_{i}";
                double price = random.Next(50, 151);
                int showId = showTimeIds[random.Next(showTimeIds.Count)];

                ticketService.AddTicket(seat, customer, price, showId);
            }
        }
    }
}
