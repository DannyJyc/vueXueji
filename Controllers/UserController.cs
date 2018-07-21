using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string Name, string Username, string Telephone, string Power, string Password)
        {
            return Content(UserDAL.Register(Name,Username,Telephone,Power,Password));
        }

        public ActionResult List(string Username,string Password)
        {
            return Content(UserDAL.UserPower(Username, Password));
        }

    }
}