using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.Models
{
    public class Station : Base
    {
        public int StationId { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public string Address { get; set; }

        public TimeSpan OpenTimeFrom { get; set; }

        public TimeSpan OpenTimeTo { get; set; }

        public byte Capacity { get; set; }

        public bool IsActive { get; set; }
    }
}
