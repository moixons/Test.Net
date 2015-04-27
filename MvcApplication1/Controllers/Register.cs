using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logintp.ViewModel;
using Logintp.Models;
 
namespace Logintp.Service
{
    public class Register : Controller
    {
        //This method is the first to call and renders the Register View.
        public ActionResult Register()
        {
            return View();
        }

        //The form's data in Register view is posted to this method. 
        //We have binded the Register View with Register ViewModel, so we can accept object of Register class as parameter.
        //This object contains all the values entered in the form by the user.
        [HttpPost]
        public ActionResult SaveRegisterDetails(Register registerDetails)
        {
            //We check if the model state is valid or not. We have used DataAnnotation attributes.
            //If any form value fails the DataAnnotation validation the model state becomes invalid.
            if (ModelState.IsValid)
            {
                //If the model state is valid i.e. the form values passed the validation then we are storing the User's details in DB.
                RegisterLogin reglog = new RegisterLogin();
                //Calling the SaveDetails method which saves the details.
                reglog.SaveDetails(registerDetails);
                ModelState.AddModelError("", "User Details Saved Successfully");
                return View("Register");
            }
            else
            {
                //If the validation fails, we are returning the model object with errors to the view, which will display the error messages.
                return View("Register", registerDetails);
            }
        }
    }
}