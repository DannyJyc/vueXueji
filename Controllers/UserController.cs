using System.Linq;
using System.Web.Mvc;
using testxueji.Models;
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
        public ActionResult Register(string name, string username, string telephone, string power, string password)
        {
            return Content(UserDal.Register(name,username,telephone,power,password));
        }

        public ActionResult List(string username,string password)
        {
            return Content(UserDal.UserPower(username, password));
        }

        [HttpPost]
        public ContentResult AddSession(string power,string username)
        {
            string temp = "0";

            using (var db = new XuejiContext())
            {

                switch (power)
                {
                    case "1":
                    case "t":
                        var teachers = db.Teacherses.SingleOrDefault(t => t.UserName == username);
                        Session["Power"] = power;
                        if (teachers != null)
                        {
                            Session["Id"] = teachers.Id;
                            Session["Name"] = teachers.Name;
                        }

                        temp = power == "1" ? "1" : "t";
                        break;;
                    case "l":
                        var lecturer = db.Lectureres.SingleOrDefault(l=>l.UserName==username);
                        Session["Power"] = power;
                        if (lecturer != null)
                        {
                            Session["Id"] = lecturer.Id;
                            Session["Name"] = lecturer.Name;
                        }
                        temp = "l";

                        break;
                    case "s":
                        var student = db.Studentses.SingleOrDefault(l => l.Number == username);
                        Session["Power"] = power;
                        if (student != null)
                        {
                            Session["Id"] = student.Id;
                            Session["Name"] = student.Name;
                        }
                        temp = "s";

                        break;
                    default:
                        break;
                }
                return Content(temp);

            }

        }
    }
}