using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
 
namespace Logintp.ViewModel
{
    public class Register
    {
        [Required]
        public string NAME { get; set; }
 
        [Required]
        public string EMAIL { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PASSEWORD { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime BIRTHDAY { get; set; }
 
        [Required]
        public string COUNTRY { get; set; }
    }
}