using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class ListUsers
    {
        //public string Avatar { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Online { get; set; }
        public DateTime Dlogin { get; set; }
        public string IP { get; set; }
        public Int32 TotalLogins { get; set; }
    }
}
