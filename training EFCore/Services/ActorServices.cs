using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using training_EFCore.models;

namespace training_EFCore.Services
{
    public class ActorService
    {
        private readonly Context context;

        public ActorService(Context context)
        {
            this.context = context; 
        }

        public void AddActor(string name, int age)
        {
            var actor = new Actor
            {
                Name = name,
                Age = age
            };
            context.Actors.Add(actor);
        }

        public void RemoveActor(int id)
        {
            var actor = context.Actors.Find(id);
            if (actor != null)
            {
                context.Actors.Remove(actor);
            }
        }

        public void UpdateActor(int id, string name, int age)
        {
            var actor = context.Actors.Find(id);
            if (actor != null)
            {
                actor.Name = name;
                actor.Age = age;
            }
        }

        public Actor GetActor(int id) => context.Actors.Find(id);

        public IEnumerable<Actor>GetActors()=>context.Actors.AsNoTracking();

        public IEnumerable<Actor> GetActorswithmovies() => context.Actors.Include(a=>a.Movies).AsNoTracking();
        public List<Actor> GetActorsUsingName(string name) => 
            context.Actors.Where(a => a.Name == name).Include(a => a.Movies).AsNoTracking().ToList();
        public IEnumerable<Actor> AgeRange(int firstAge, int secondAge)
        {
            var min = Math.Min(firstAge, secondAge);
            var max = Math.Max(firstAge, secondAge);

            return context.Actors
                          .Where(a => a.Age >= min && a.Age <= max)
                          .Include(a => a.Movies)
                          .AsNoTracking();
        }
        public int GetTotalActorCount() =>context.Actors.Count();

        public bool IsActorExist(int id)=>context.Actors.Any(a=>a.ID == id);


        public void SaveChanges()=>context.SaveChanges();
    }
}
