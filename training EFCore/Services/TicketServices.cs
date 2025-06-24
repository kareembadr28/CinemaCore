using System.Linq;
using Microsoft.EntityFrameworkCore;
using training_EFCore.models;

namespace training_EFCore.Services
{
    public class TicketService
    {
        private readonly Context context;

        public TicketService(Context context)
        {
            this.context = context;

        }

        public void AddTicket(int seatNumber, string customerName, double price, int showTimeId)
        {
            var ticket = new Ticket
            {
                SeatNumber = seatNumber,
                CustomerName = customerName,
                Price = price,
                ShowTimeID = showTimeId
            };
            context.Tickets.Add(ticket);
        }

        public void RemoveTicket(int id)
        {
            var ticket = context.Tickets.Find(id);
            if (ticket != null)
            {
                context.Tickets.Remove(ticket);
            }
        }

        public void UpdateTicket(int id, int seatNumber, string customerName, double price)
        {
            var ticket = context.Tickets.Find(id);
            if (ticket != null)
            {
                ticket.SeatNumber = seatNumber;
                ticket.CustomerName = customerName;
                ticket.Price = price;
            }
        }

        public Ticket GetTicket(int id) => context.Tickets.Find(id);

        public IEnumerable<Ticket> GetAllTickets() =>
            context.Tickets.AsNoTracking();

        public IEnumerable<Ticket> GetTicketsWithShowTime_Eager() =>
            context.Tickets
                   .Include(t => t.ShowTime)
                   .ThenInclude(s => s.Movie)
                   .AsNoTracking();

        public Ticket GetTicketWithShowTime_Explicit(int id)
        {
            var ticket = context.Tickets.Find(id);
            if (ticket != null)
            {
                context.Entry(ticket)
                       .Reference(t => t.ShowTime)
                       .Load();

                context.Entry(ticket.ShowTime)
                       .Reference(s => s.Movie)
                       .Load();
            }

            return ticket;
        }

        public IEnumerable<Ticket> GetTicketsByPriceRange(double min, double max) =>
            context.Tickets
                   .Where(t => t.Price >= min && t.Price <= max)
                   .AsNoTracking();
        public IEnumerable<Ticket> GetTicketsByMovie(int movieId) =>
            context.Tickets
                   .Include(t => t.ShowTime)
                   .Where(t => t.ShowTime.MovieID == movieId)
                   .AsNoTracking();

        public int GetTotalTicketCount() => context.Tickets.Count();
        public double GetAverageTicketPrice() => context.Tickets.Average(t => t.Price);
        public double GetMaxTicketPrice() => context.Tickets.Max(t => t.Price);
        public void SaveChanges() => context.SaveChanges();

    }
}
