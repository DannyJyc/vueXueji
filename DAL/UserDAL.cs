using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class UserDal
    {
        /// <summary>
        /// 输入用户名密码判断其职能
        /// </summary>
        /// <param name="username">类型 string，用户名</param>
        /// <param name="password">类型 string，密码</param>
        /// <returns>返回字符串“0”密码错误“1”领导“t”导员“s”学生“l(L)”讲师</returns>
        public static string UserPower(string username, string password)
        {
            var item ="0";
            using (var db = new XuejiContext())
            {
                var teachersSingle = db.Teacherses.SingleOrDefault(t => t.UserName == username && t.PassWord == password);
                if (teachersSingle != null)
                {
                    item = teachersSingle.PowerId == 1 ? "1" : "t";
                }
                else
                {
                    var lecturterSingle =
                        db.Lectureres.SingleOrDefault(l => l.UserName == username && l.PassWord == password);
                    if (lecturterSingle != null)
                    {
                        item = "l";
                    }
                    else
                    {
                        var studentsSingle =
                            db.Studentses.SingleOrDefault(s => s.Number == username && s.PassWord == password);
                        item = studentsSingle != null ? "s" : "0";
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// 注册，判断是否姓名/用户名是否重名
        /// </summary>
        /// <param name="name">类型 string，姓名</param>
        /// <param name="username">类型 string，用户名</param>
        /// <param name="telephone">类型 string，电话</param>
        /// <param name="power">类型 string，职能（“1”领导“t”导员“s”学生“l(L)”讲师）</param>
        /// <param name="password">类型 string，密码</param>
        /// <returns>返回字符串“0”异常“name”姓名重名“username”用户名重名“ok”注册成功“ero”出错</returns>
        public static string Register(string name, string username, string telephone,string power, string password)
        {
            var item = "0";
            using (var db = new XuejiContext())
            {
                var jtname = db.Teacherses.SingleOrDefault(t => t.Name == name);
                var jlname = db.Lectureres.SingleOrDefault(l => l.Name == name);
                if (jtname != null || jlname != null)
                {
                    item = "name";
                }
                else
                {
                    var jtusername = db.Teacherses.SingleOrDefault(t => t.UserName == username);
                    var jlusername = db.Lectureres.SingleOrDefault(l => l.UserName == username);
                    if (jtusername != null || jlusername != null)
                    {
                        item = "username";
                    }
                    else
                    {
                        if (power == "t")
                        {
                            var teachers = new Teachers
                            {
                                UserName = username,
                                PassWord = password,
                                PowerId = 0,
                                Telephone = telephone,
                                Name = name,
                                Status = 1
                            };
                            db.Teacherses.Add(teachers);
                            db.SaveChanges();
                            item = "ok";
                        }else if (power == "l")
                        {
                            var lecturer = new Lecturer
                            {
                                UserName = username,
                                PassWord = password,
                                Telephone = telephone,
                                Name = name,
                                Status = 1
                            };
                            db.Lectureres.Add(lecturer);
                            db.SaveChanges();
                            item = "ok";
                        }
                        else
                        {
                            item = "ero";
                        }
                    }
                }
            }
            return item;
        }
    }
}