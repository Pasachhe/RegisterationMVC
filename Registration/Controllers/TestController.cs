using Registration.Models;
using System;
using System.Collections.Generic;//list jaha pani use garna paiyne 
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registration.Controllers
{
    public class TestController : Controller, IDisposable // Inheritance
    {
        HREntities db = new HREntities();

        public ActionResult Index()
        {


            List<Department> list = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");


            List<SiteUser> Ulist = db.SiteUsers.ToList();
            ViewBag.SiteUserList = new SelectList(Ulist, "UserId", "UserName");


            List<EmployeeViewModel> listEmp = db.Employees.Where(x => x.IsDeleted == false).Select(x => new EmployeeViewModel { UserName = x.SiteUser.UserName, DepartmentName = x.Department.DepartmentName, Salary = x.Salary, EmployeeId = x.EmployeeId }).ToList();
            ViewBag.EmployeeList = listEmp;

            return View();
        }

        // Polymorphism  (fn OvRi)

        [HttpPost]
        public ActionResult Index(EmployeeViewModel model)
        {
            try
            {

                List<Department> Deplist = db.Departments.ToList();
                ViewBag.DepartmentList = new SelectList(Deplist, "DepartmentId", "DepartmentName");


                List<Designition> Deslist = db.Designitions.ToList();
                ViewBag.DepartmentList = new SelectList(Deslist, "DepartmentId", "DepartmentName");


                if (model.EmployeeId > 0)
                {
                    //update
                    Employee emp = db.Employees.SingleOrDefault(x => x.EmployeeId == model.EmployeeId && x.IsDeleted == false);

                    emp.DepartmentId = model.DepartmentId;
                    emp.UserId = model.UserId;
                    emp.DesId = model.DesId;
                    emp.Salary = model.Salary;
                    db.SaveChanges();


                }
                else
                {
                    //Insert
                    Employee emp = new Employee();
                    emp.Salary = model.Salary;
                    emp.UserId = model.UserId;
                    emp.DepartmentId = model.DepartmentId;
                    emp.DesId = model.DesId;
                    emp.IsDeleted = false;

                    db.Employees.Add(emp);
                    db.SaveChanges();

                }
                return View(model);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }





        public JsonResult DeleteEmployee(int EmployeeId)
        {
            
            bool result = false;
            Employee emp = db.Employees.SingleOrDefault(x => x.IsDeleted == false && x.EmployeeId == EmployeeId);
            if (emp != null)
            {
                emp.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }





        public ActionResult AddEditEmployee(int EmployeeId)
        {
            List<Department> Deplist = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(Deplist, "DepartmentId", "DepartmentName");

            List<SiteUser> Ulist = db.SiteUsers.ToList();
            ViewBag.SiteUserList = new SelectList(Ulist, "UserId", "UserName");

            List<Designition> Deslist = db.Designitions.ToList();
            ViewBag.DesignitionList = new SelectList(Deslist, "DesId", "DesName");

            EmployeeViewModel model = new EmployeeViewModel();

            if (EmployeeId > 0)
            {

                Employee emp = db.Employees.SingleOrDefault(x => x.EmployeeId == EmployeeId && x.IsDeleted == false);
                model.EmployeeId = emp.EmployeeId;
                model.DepartmentId = emp.DepartmentId;
                model.UserId = emp.UserId;
                model.Salary = emp.Salary;

            }
            return PartialView("Partial2", model);
        }


        public ActionResult GeneralUser()
        {


            int empl = Convert.ToInt32(Session["EmployeeId"]);
            List<Department> list = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");


            List<SiteUser> Ulist = db.SiteUsers.ToList();
            ViewBag.SiteUserList = new SelectList(Ulist, "UserId", "UserName");

            //List<Employee> Elist = db.Employees.Where(x => x.EmployeeId.Equals(empl)).ToList();
            var Elist = db.Employees.Where(x => x.EmployeeId.Equals(empl)).FirstOrDefault();
         //   ViewBag.EmpList = new SelectList(Elist, "EmployeeId", "EmployeeName");
         

            List<AttendanceViewModel> attendList = db.Attendances.Where(x => x.EmployeeId == empl).Select(x => new AttendanceViewModel {
                AttendanceId = x.AttendanceId,
                EmployeeName = x.Employee.SiteUser.UserName,
                Date = x.Date,
                InTime = x.InTime,
                OutTime = x.OutTime,
                WorkHours = x.WorkHours,
                Wage = x.Wage
            }).ToList();
            ViewBag.AttendanceList = attendList;

            return View();

            
        }
        public ActionResult SuperAdmin()
        {
            List<Department> list = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");


            List<SiteUser> Ulist = db.SiteUsers.ToList();
            ViewBag.SiteUserList = new SelectList(Ulist, "UserId", "UserName");


            List<AttendanceViewModel> attendList = db.Attendances.Select(x => new AttendanceViewModel { AttendanceId = x.AttendanceId, EmployeeName = x.Employee.SiteUser.UserName, Date = x.Date, InTime = x.InTime, OutTime = x.OutTime, WorkHours = x.WorkHours , Wage =x.Wage}).ToList();
            ViewBag.AttendanceList = attendList;

            return View();
        }


        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public JsonResult LoginUser(RegistrationViewModel model)
        {
            using (HREntities db = new HREntities())
            {



                SiteUser user = db.SiteUsers.SingleOrDefault(x => x.EmailId == model.EmailId && x.Password == model.Password);
                string result = "fail";

                if (user != null)
                {
                    var id = Convert.ToString(Guid.NewGuid());
                    var InTime = DateTime.Now;

                    Employee emp = db.Employees.SingleOrDefault(x => x.UserId == user.UserId);

                    double H1 = InTime.Hour;
                    double M1 = InTime.Minute;

                    var H11 = H1 + M1/60;

                    Session["UserId"] = user.UserId;
                    Session["UserName"] = user.UserName;
                    //from variables
                    Session["Intime"] = InTime;
                    Session["AttendanceId"] = id;
                    Session["EmployeeId"] = emp.EmployeeId;
                    Session["Salary"] = emp.Salary;

                    Session["H11"] = H11;

                    Attendance att = new Attendance();

                    att.AttendanceId = id;
                    att.EmployeeId = emp.EmployeeId;
                    att.Date = DateTime.Now;
                    att.InTime = DateTime.Now;
                    att.OutTime = null;
                    att.WorkHours = null;
                    att.Wage = null;
                    db.Attendances.Add(att);
                    db.SaveChanges();


                    if (user.RoleId == 3)
                    {
                        result = "GeneralUser";
                    }
                    else if (user.RoleId == 1)
                    {
                        result = "Admin";
                    }
                    else if (user.RoleId == 2)
                    {
                        result = "SuperAdmin";
                    }

                }
                return Json(result, JsonRequestBehavior.AllowGet); //means to allow HTTPget reqeust to HTTPpost

            }
        }


        public ActionResult Logout()
        {

            var id = Convert.ToString(Session["AttendanceId"]);
            HREntities db = new HREntities();
            Attendance att = db.Attendances.Where(x => x.AttendanceId == id).SingleOrDefault();
            //Employee emp = db.Employees.SingleOrDefault(x => x.UserId == User.UserId);

            var OutTime = DateTime.Now;
            var In = DateTime.Parse(Session["InTime"].ToString());
            double InHrs = double.Parse(Session["H11"].ToString());

            double H2 = OutTime.Hour;
            double M2 = OutTime.Minute;
            var OutHrs = H2  + M2/60;

            double salry = double.Parse(Session["Salary"].ToString());

            var WorkTime = OutHrs - InHrs;
            var DaySalary = WorkTime * salry;

            var dayWork = OutHrs - InHrs;

            //update


            att.OutTime = OutTime;
            att.WorkHours = dayWork;
            att.Wage = DaySalary;



            db.SaveChanges();


            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");

        }





        [HttpGet]
        public ActionResult Registration() { return View(); }

        [HttpPost]
        public JsonResult RegisterUser(RegistrationViewModel model)
        {


            SiteUser siteUser = new SiteUser();

            siteUser.UserName = model.UserName;

            siteUser.EmailId = model.EmailId;

            siteUser.Password = model.Password;

            siteUser.Address = model.Address;

            siteUser.RoleId = model.RoleId;

            db.SiteUsers.Add(siteUser);

            db.SaveChanges();


            return Json("Success", JsonRequestBehavior.AllowGet);

        }


    }

}