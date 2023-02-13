using Microsoft.AspNetCore.Mvc;
using Online_Consultation.Models;

namespace Online_Consultation.Controllers
{
    public class DepartmentsController : Controller
    {
        public DoctorDbContext doctorDbContext;

        public DepartmentsController(DoctorDbContext doctorDbContext)
        {
            this.doctorDbContext = doctorDbContext;

        }
        public IActionResult Index()
        {
            return View(doctorDbContext.departments.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department departmentsection)
        {

            doctorDbContext.Add(departmentsection);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            var dep = doctorDbContext.departments.FirstOrDefault(e => e.id == id);
            return View(dep);
        }
        [HttpPost]
        public IActionResult Edit(Department departmentsection)
        {
            doctorDbContext.Update(departmentsection);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Details(int? id)
        {
            return View(doctorDbContext.departments.FirstOrDefault(e => e.id == id));

        }
        public IActionResult Delete(int? id)
        {
            return View(doctorDbContext.departments.FirstOrDefault(e => e.id == id));
        }
        [HttpPost]
        public IActionResult Delete(Department departmentsection )
        {
            doctorDbContext.Remove(departmentsection);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
