using Intel.BikeRental.DAL.Interceptors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.DAL
{
    public class Configuration : DbConfiguration
    {
        public Configuration()
        {
            this.AddInterceptor(new NLogCommandInterceptor());

            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(10, TimeSpan.FromSeconds(5)));

        }
    }
}
