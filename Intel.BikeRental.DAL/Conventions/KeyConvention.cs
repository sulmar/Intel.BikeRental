using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.DAL.Conventions
{
    public class KeyConvention : Convention
    {
        public KeyConvention()
        {
            Properties()
                .Where(p => p.Name.EndsWith("Key"))
                .Configure(p => p.IsKey());
        }
    }
}
