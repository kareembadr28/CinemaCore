using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_EFCore.models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Genre Genre { get; set; }

        public int GenreID { get; set; }
        public virtual ICollection<ShowTime> ShowTimes { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
    }
}
