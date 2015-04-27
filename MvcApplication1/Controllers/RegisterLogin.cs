using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logintp.ViewModel;
using Logintp.Models;
 
namespace Logintp.Service
{
    public class RegisterLogin
    {
        //This method accepts object of Register ViewModel class.
        public void SaveDetails(Register register)
        {
            //Creating DataContext object.
            using (LoginTpEntities dataContext = new LoginTpEntities())
            {
                //Creating the UserDetails object which is our entity.
                UserDetails details = new UserDetails();
                details.NAME = register.NAME;
                details.EMAIL = register.EMAIL;
                details.PASSWORD = register.PASSWORD;
                details.COUNTRY = register.COUNTRY;
                details.BIRTHDAY = Convert.ToDateTime(register.BIRTHDAY);
                //Saving the details.
                dataContext.UserDetails.AddObject(details);
                dataContext.SaveChanges();
            }
        }
    }
}