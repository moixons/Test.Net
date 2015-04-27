using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Utils
    {
        public static void validateUser(SharedModels.Models.UserModel user)
        {
            user.Password = Helpers.SHA1.Encode(user.Password);
            user.Email = user.Email.ToLower(); 
        }
    }
}
