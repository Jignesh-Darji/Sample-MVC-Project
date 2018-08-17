using SampleMVCMasterDetails.DataAccess;
using SampleMVCMasterDetails.DataAccess.Concrete;
using SampleMVCMasterDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMVCMasterDetails.Controllers
{
    public class HomeController : Controller
    {
        #region Private Variables

        EmployeeDataAccess empDataAccess=new EmployeeDataAccess();

        #endregion

       
        #region Action Method
        // GET: Home
        public ActionResult Index()
        {
            List<Employees> lstEmployee = new List<Employees>();
            lstEmployee = empDataAccess.GetAllEmployees().ToList();
            return View(lstEmployee);
        }
        [HttpGet]
        public ActionResult EditEmployee(string EmpId)
        {
            long empid = 0;
            long.TryParse(EmpId, out empid);
            Employees emp = new Employees();
            emp = empDataAccess.GetEmployeeById(empid);
            return View("AddEmployee",emp);
        }
        [HttpPost]
        public ActionResult EditEmployee(Employees emp)
        {
            if (ModelState.IsValid)
            {
                empDataAccess.AddUpdateEmployee(emp);
                List<Employees> lstEmployee = new List<Employees>();
                lstEmployee = empDataAccess.GetAllEmployees().ToList();
                return View("Index",lstEmployee);
                
            }
            else
            {
                return View("AddEmployee");
            }
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            Employees emp = new Employees();
            emp.EmpId = 0;
            return View(emp);
        }
        [HttpPost]
        public ActionResult AddEmployee(Employees emp)
        {
            
            if (ModelState.IsValid)
            {
                empDataAccess.AddUpdateEmployee(emp);
                List<Employees> lstEmployee = new List<Employees>();
                lstEmployee = empDataAccess.GetAllEmployees().ToList();
                return View("Index", lstEmployee);

            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult DeleteEmployee(string EmpId)
        {
            long empid = 0;
            long.TryParse(EmpId, out empid);
            Employees emp = new Employees();
            emp = empDataAccess.DeleteEmployeeById(empid);
            List<Employees> lstEmployee = new List<Employees>();
            lstEmployee = empDataAccess.GetAllEmployees().ToList();
            return View("Index", lstEmployee);
        }
        #endregion
    }
}