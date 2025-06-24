using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_EFCore.models
{
    public class Actor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
