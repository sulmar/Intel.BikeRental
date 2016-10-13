using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel.BikeRental.Models
{
    public class User : Base
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public int UserKey { get; set; }

        // public bool IsActive { get; set; }

         public bool Gender { get; set; }


        public bool IsLogged { get; set; }


        public Parameters Parameters { get; set; }

        public string SerializedParameters { get; set; }

        public byte[] RowVersion { get; set; }


    }


   public class Parameters
    {
        public int P1 { get; set; }
        public int P2 { get; set; }


        public override string ToString()
        {
            return $"<xml><p1>{P1}</p1><p2>{P2}</p2></xml>";
        }
    }
}
