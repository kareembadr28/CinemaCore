using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using training_EFCore.models;

namespace training_EFCore.Services
{
    public class ShowTimeService
    {
        private readonly Context context;

        public ShowTimeService(Context context)
        {
            this.context = context;

        }

        public void AddShowTime(DateTime startTime, int movieId)
        {
            var show = new ShowTime
            {
                StartTime = startTime,
                MovieID = movieId
            };
            context.ShowTimes.Add(show);
        }

        public void RemoveShowTime(int id)
        {
            var show = context.ShowTimes.Find(id);
            if (show != null)
            {
                context.ShowTimes.Remove(show);
            }
        }

        public void UpdateShowTime(int id, DateTime newTime)
        {
            var show = context.ShowTimes.Find(id);
            if (show != null)
            {
                show.StartTime = newTime;
            }
        }

        public ShowTime GetShowTime(int id) => context.ShowTimes.Find(id);


        public IEnumerable<ShowTime> GetAllShowTimes() =>context.ShowTimes.AsNoTracking();

        public IEnumerable<ShowTime> GetShowTimesWithMovies_Eager() =>
            context.ShowTimes
                   .Include(s => s.Movie)
                   .AsNoTracking();

        public ShowTime GetShowTimeWithMovie_Explicit(int id)
        {
            var show = context.ShowTimes.Find(id);
            if (show != null)
            {
                context.Entry(show)
                       .Reference(s => s.Movie)
                       .Load();
            }
            return show;
        }
        public IEnumerable<ShowTime> GetShowTimesForMovie(int movieId) =>
            context.ShowTimes
                   .Where(s => s.MovieID == movieId)
                   .Include(s => s.Movie)
                   .AsNoTracking();

        public int GetTotalShowTimeCount() => context.ShowTimes.Count();

        public bool IsShowTimeExist(int id) =>context.ShowTimes.Any(s => s.ID == id);
        public void SaveChanges() => context.SaveChanges();

    }
}
