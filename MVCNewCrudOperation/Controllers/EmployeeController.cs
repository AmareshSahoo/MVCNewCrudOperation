using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCNewCrudOperation.Models;
using System.Data.Entity;

namespace MVCNewCrudOperation.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DbModel db = new DbModel())
            {
                List<Employee> emplist = db.Employees.ToList<Employee>();
                return Json(new { data = emplist }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if(id==0)
            return View(new Employee());
            else
            {
                using (DbModel db= new DbModel())
                {
                    var emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                    return View(emp);
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            using (DbModel db=new DbModel())
            {
                if (emp.EmployeeID == 0)
                {
                db.Employees.Add(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Sucessfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Sucessfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DbModel db = new DbModel())
            {
                Employee emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success=true,message="Delete Sucessfully"},JsonRequestBehavior.AllowGet);
            }
        }
    }
}