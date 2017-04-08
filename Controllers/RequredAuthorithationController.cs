using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API.Models;
using API.Models.Linq;
using System.Configuration;

namespace API.Controllers
{
    public class RequredAuthorithationController : Controller
    {
        // GET: RequredAuthorithation
        protected API_User currentUser;
        public void beforeAction()
        {

        }
    }
}