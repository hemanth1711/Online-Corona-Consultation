using Microsoft.AspNetCore.Mvc;
using Online_Consultation.Models;

namespace Online_Consultation.Controllers
{
    public class MedicineController : Controller
    {
        public DoctorDbContext doctorDbContext;

        public MedicineController(DoctorDbContext doctorDbContext)
        {
            this.doctorDbContext = doctorDbContext;
        }
        public IActionResult Index()
        {
            return View(doctorDbContext.medicines.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Medicine med)
        {

            doctorDbContext.Add(med);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            var med = doctorDbContext.medicines.FirstOrDefault(e => e.id == id);
            return View(med);
        }

        [HttpPost]
        public IActionResult Edit(Medicine med)
        {

            doctorDbContext.Update(med);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            return View(doctorDbContext.medicines.FirstOrDefault(e => e.id == id));
        }


        public IActionResult Delete(int? id)
        {
            return View(doctorDbContext.medicines.FirstOrDefault(e => e.id == id));
        }
        [HttpPost]
        public IActionResult Delete(Medicine med)
        {
            doctorDbContext.Remove(med);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
