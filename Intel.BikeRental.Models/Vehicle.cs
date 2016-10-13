using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.Models
{
    public abstract class Vehicle : Base
    {
        public int VehicleId { get; set; }

        public string Color { get; set; }

        public string Number { get; set; }

        public bool Active { get; set; }
    }
}
