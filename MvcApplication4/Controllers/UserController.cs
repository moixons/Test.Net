using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcLogin.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Realizar el llamado de la vista que contiene la GUI de Autenticación de la aplicación
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult logIn()
        {
            return View();
        }

        /// <summary>
        /// Verificar los datos sumunistrados por el usuario al realizar la peticion Post de envio de información 
        /// mediante la GUI de Autenticación de la aplicación. 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult logIn(Models.UserModel user)
        {
            if (ModelState.IsValid) //Verificar que el modelo de datos sea valido en cuanto a la definición de las propiedades
            {
                if (Isvalid(user.Email, user.Password))//Verificar que el email y clave exista utilizando el método privado 
                {

                    FormsAuthentication.SetAuthCookie(user.Email, false); //crea variable de usuario 
                    return RedirectToAction("Index", "Home");  //dirigir controlador home vista Index una vez se a autenticado en el sistema
                }
                else
                {
                    ModelState.AddModelError("", "Login data in incorrect"); //adicionar mensaje de error al model 
                }
            }
            return View(user);
        }


        /// <summary>
        /// Realizar el llamado de la vista que contiene la GUI, para registrarse en el sistema
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
            return View();
        }


        /// <summary>
        /// Verificar los datos sumunistrados por el usuario al realizar la peticion de envio de información 
        /// mediante la GUI para crear un nuevo usuario en el sistema 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Registration(Models.UserModel user)
        {
            if (ModelState.IsValid )
            {
                using (var db = new MainDbContext()) //crear objeo con refencia ala bd para crear el nuevo usuario
                {
                    var sysUser = db.SystemUsers.Create();
                    sysUser.Email = user.Email;
                    sysUser.Password = user.Password;
                    sysUser.PasswordSalt = user.Password;
                    sysUser.UserId = Guid.NewGuid();

                    db.SystemUsers.Add(sysUser);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }                
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect."); //adicionar mensaje de error al modelo 
            }
            return View();
        }

        /// <summary>
        /// Cerrar sesion del usuario autenticado
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        /// <summary>
        /// Metodo para validar el email y password del usuario, realiza la consulta a la bd
        /// </summary>
        /// <param name="Email">Email ingresado</param>
        /// <param name="password">Password ingresado</param>
        /// <returns>
        /// True:Usuario valido
        /// False Usuario Invalido
        /// </returns>
        private bool Isvalid(string Email, string password)
        {
            bool Isvalid = false;
            using (var db = new MainDbContext())
            {
                var user = db.SystemUsers.FirstOrDefault(u => u.Email == Email); //consultar el primer registro con los el email del usuario
                if (user !=null)
                {
                    if (user.Password == password)  //Verificar password del usuario
                    {
                        Isvalid = true;
                    }
                }
            }
            return Isvalid;
        }
    }
}
