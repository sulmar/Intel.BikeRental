using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.Models
{
    public class Location : Base
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Location()
        {

        }

        public Location(double lat, double lng)
            : this()
        {
            this.Latitude = lat;
            this.Longitude = lng;
        }

       
    }
}
