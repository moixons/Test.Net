using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logintp.ViewModel;
using Logintp.Models;

namespace Logintp.Service
{
    public class Login : Controller
    {
        //This action method renders the Login View.
        public ActionResult Login()
        {
            return View();
        }

        //The login form is posted to this method.
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //Checking the state of model passed as parameter.
            if (ModelState.IsValid)
            {
                RegisterLogin login = new RegisterLogin();
                //Validating the user, whether the user is valid or not.
                bool isValidUser = login.IsValidUser(model);
                //If user is valid we are redirecting it to Welcome page.
                if (isValidUser)
                    return View("Welcome");
                else
                {
                    //If the username and password combination is not present in DB then error message is shown.
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                //If model state is not valid, the model with error message is returned to the View.
                return View(model);
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}