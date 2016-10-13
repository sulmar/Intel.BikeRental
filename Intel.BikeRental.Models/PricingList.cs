using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.Models
{
    public class PricingList : Base
    {
        public int PricingListId { get; set; }

        public decimal Price { get; set; }
    }
}
