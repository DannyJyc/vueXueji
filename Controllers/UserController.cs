using System.Linq;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        /// <summary>
        /// 返回到登录首页
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回到注册首页
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 接受到注册首页的post进行对数据的判断，后完成注册
        /// </summary>
        /// <param name="name">类型 string，对应 Name</param>
        /// <param name="username">类型 string，对应 UserName</param>
        /// <param name="telephone">类型 string，对应Telephone</param>
        /// <param name="power">类型 string，对应视图的power</param>
        /// <param name="password">类型 string，对应password</param>
        /// <returns>返回字符串：“username”用户名重名“name”姓名重名“ok”注册成功“ero”出错</returns>
        [HttpPost]
        public ActionResult Register(string name, string username, string telephone, string power, string password)
        {
            return Content(UserDal.Register(name,username,telephone,power,password));
        }

        /// <summary>
        /// 用于主页登录时判断是否登录成功
        /// </summary>
        /// <param name="username">类型 string，用户名</param>
        /// <param name="password">类型 string，密码</param>
        /// <returns>返回字符串“1”为登陆成功“0”登录失败</returns>
        public ActionResult List(string username,string password)
        {
            return Content(UserDal.UserPower(username, password));
        }

        /// <summary>
        /// 从登录主页，登录成功以后为Session添加信息{Power,Id,Name}
        /// </summary>
        /// <param name="power">类型 String，登陆成功以后传过来的身份</param>
        /// <param name="username">类型 String，登录成功以后传过来的用户名</param>
        /// <returns>返回字符串“1”为添加正常“0”添加异常（出错）</returns>
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