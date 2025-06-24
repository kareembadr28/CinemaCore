using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_EFCore.models
{
    public class Ticket
    {
        public int ID { get; set; }
        public int SeatNumber { get; set; }
        public string CustomerName { get; set; }
        public double Price { get; set; }
        public int ShowTimeID { get; set; }
        public virtual ShowTime ShowTime{ get; set; }

    }
}
