using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using training_EFCore.models;

namespace training_EFCore.Services
{
    public class DbQueryExecutor
    {
        private readonly Context context;

        public DbQueryExecutor(Context context) => this.context = context;

        public IEnumerable<Actor> GetActorsWithMoreThan3Movie()
        {
            string query = @"
        SELECT a.*
        FROM Actors a
        INNER JOIN ActorMovie am ON a.ID = am.ActorsID
        GROUP BY a.ID, a.Name, a.Age
        HAVING COUNT(am.MoviesID) > 3";

            return context.Actors
                          .FromSqlRaw(query)
                          .AsNoTracking()
                          .ToList();
        }

        public int DeleteOldTickets()
        {
            var sql = "DELETE FROM Tickets WHERE Price < 60";
            return context.Database.ExecuteSqlRaw(sql);
        }

        public IEnumerable<Movie> ExecuteGetAllMoviesSP()
        {
            return context.Movies
                           .FromSqlRaw("EXEC GetAllMovies")
                           .AsNoTracking()
                           .ToList();
        }

        public IEnumerable<Actor> ExecuteGetActorsByAgeSP(int minAge)
        {
            return context.Actors
                          .FromSqlInterpolated($"EXEC GetActorsByMinAge {minAge}")
                          .AsNoTracking()
                          .ToList();
        }


    }
}
