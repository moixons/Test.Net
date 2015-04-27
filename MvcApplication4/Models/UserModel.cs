using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcLogin.Models
{
    public class UserModel
    {
        [Required] //Dato requerido
        [EmailAddress] //Validar que se ingrese un email valido
        [StringLength(150)] //longitud maxima del campo
        [Display(Name="Email address ")] //Mensaje indicar obligatorio
        public string Email { get; set; }

        [Required] //Dato requerido
        [DataType(DataType.Password)] //Indicar dato  tipo password
        [StringLength(20, MinimumLength=6)] //Longitud minima y maxima
        [Display(Name = "Password ")] //Mensaje indicar obligatorio
        public string Password { get; set; }        

    }
}