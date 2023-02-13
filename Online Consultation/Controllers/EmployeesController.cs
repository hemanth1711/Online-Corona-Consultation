using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Consultation.Models;

namespace Online_Consultation.Controllers
{
    public class EmployeesController : Controller
    {
        public DoctorDbContext doctorDbContext;

        public EmployeesController(DoctorDbContext doctorDbContext)
        {
            this.doctorDbContext = doctorDbContext;
           
        }
        public IActionResult Index()
        {

            return View(doctorDbContext.Employees.ToList());
        }
        public IActionResult Create()
        {
            ViewData["savecat"] = new SelectList(doctorDbContext.departments, "id", "dname");

            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employeeProfile)
        {

            doctorDbContext.Employees.Add(employeeProfile);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            var employees=doctorDbContext.Employees.FirstOrDefault(e =>e.id==id);
            return View(employees);
        }
        [HttpPost]
        public IActionResult Edit(Employee employeeProfile)
        {
            doctorDbContext.Update(employeeProfile);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Details(int? id)
        {
            return View(doctorDbContext.Employees.FirstOrDefault(e => e.id == id));
            
        }
        public IActionResult Delete(int? id)
        {
            return View(doctorDbContext.Employees.FirstOrDefault(e => e.id == id));
        }
        [HttpPost]
        public IActionResult Delete(Employee employeeProfile)
        {
            doctorDbContext.Remove(employeeProfile);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }



    }
}
