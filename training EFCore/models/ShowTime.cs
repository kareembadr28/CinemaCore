using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_EFCore.models
{
    public class ShowTime
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }

        public int MovieID { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}
