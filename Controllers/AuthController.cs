using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using API.Models;
using API.Models.Linq;

namespace API.Controllers
{
    public class AuthController : Controller
    {
        
        public API_User ActionLogin(Frontend_User f_User)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            API_User A = new API_User();
            string email = f_User.email;
            string password_hash = f_User.password_hash;
            if(context.User.Where(p=>p.email == email).Any())
            {
                var _user = context.User.Where(p => p.email == email).FirstOrDefault();
                A.id = _user.Id;
                A.name = _user.name;
            }
            if (!A.ValidatePassword(f_User.password_hash))
                throw new Exception();
            else
                A.GenerateAccesToken();
            return A;
         }
        public API_User ActionRegister(Frontend_User f_User)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            API_User A = new API_User();
            A.email = f_User.email;
            if (context.User.Where(p => p.email == f_User.email).Any())
                throw new Exception();
            else
            {
                A.name = f_User.email;
                A.GenerateAccesToken();
                var k = new User
                {
                    name = A.name,
                    email = f_User.email,
                    password_hash = f_User.password_hash
                };
                context.User.InsertOnSubmit(k);
                context.User.Context.SubmitChanges();
                return A;
            }
        }
    }
}
