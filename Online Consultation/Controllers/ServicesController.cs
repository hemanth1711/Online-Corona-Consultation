using Microsoft.AspNetCore.Mvc;
using Online_Consultation.Models;

namespace Online_Consultation.Controllers
{
    public class ServicesController : Controller
    {
        public DoctorDbContext _doctorDbContext;


        public ServicesController(DoctorDbContext doctorDbContext)
        {
            _doctorDbContext = doctorDbContext;

        }
        public IActionResult Index()
        {
            return View(_doctorDbContext.services.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {

            _doctorDbContext.Add(service);
            _doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            var doctors = _doctorDbContext.services.FirstOrDefault(e => e.id == id);
            return View(doctors);
        }

        [HttpPost]
        public IActionResult Edit(Service service)
        {
            _doctorDbContext.Update(service);
            _doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            return View(_doctorDbContext.services.FirstOrDefault(e => e.id == id));
        }
        public IActionResult Delete(int? id)
        {
            return View(_doctorDbContext.services.FirstOrDefault(e => e.id == id));
        }
        [HttpPost]
        public IActionResult Delete(Service service)
        {
            _doctorDbContext.Remove(service);
            _doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
