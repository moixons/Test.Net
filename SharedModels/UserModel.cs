using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SharedModels.Models
{
    public class UserModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please provide NAME", AllowEmptyStrings = false)]
        [StringLength(50)]
        [Display(Name = "Name ")]
        public string Name { get; set; }

        [Display(Name = "Email address ")]
        [Required(ErrorMessage = "Please provide EMAIL ADRESS", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Please provide valid email")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        /*[RegularExpression(@"\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b")]*/
        public string Email { get; set; }

        [Display(Name = "Password ")]
        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be 6 char long.")]
        public string Password { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> BirthDay { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        public string IP { get; set; }
    }
}