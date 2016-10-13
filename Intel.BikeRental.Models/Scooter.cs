using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.Models
{
    public class Scooter : Vehicle
    {
        public int EngineCapacity { get; set; }

        public byte MaxSpeed { get; set; }
    }
}
