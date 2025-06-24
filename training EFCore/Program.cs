using Microsoft.EntityFrameworkCore;
using training_EFCore.Seeding;
using training_EFCore.Services;

namespace training_EFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            DataSeeder seeder = new DataSeeder(context);
            ActorService actorService = new ActorService(context);
            DbQueryExecutor executor = new DbQueryExecutor(context);
            GenreService genreService = new GenreService(context);
            MovieService movieService = new MovieService(context);
            ShowTimeService showTimeService = new ShowTimeService(context);
            TicketService ticketService = new TicketService(context);

            // Now U can handle any function 
        }

        }
    }

 