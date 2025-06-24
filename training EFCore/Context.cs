using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using training_EFCore.models;


namespace training_EFCore
{
    public class Context :DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
        .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CinemaCore;Trusted_Connection=True;")
        .LogTo(Console.WriteLine, LogLevel.Information) //for see the query sent to database
        .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);//Apply all configuration in config folder

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }

    }
}
