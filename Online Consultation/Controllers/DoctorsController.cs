using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Consultation.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Online_Consultation.Controllers
{
    public class DoctorsController : Controller
    {

        public DoctorDbContext doctorDbContext;
        public IHostingEnvironment _env;

        public DoctorsController(DoctorDbContext doctorDbContext, IHostingEnvironment env)
        {
            this.doctorDbContext = doctorDbContext;
            this._env = env;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(doctorDbContext.doctorsProfiles.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DoctorProfile doctorProfile)
        {
            var nam = Path.Combine(_env.WebRootPath + "/Images", Path.GetFileName(doctorProfile.dImage.FileName));
            doctorProfile.dImage.CopyTo(new FileStream(nam, FileMode.Create));
            doctorProfile.docImageUrl = "Images/" + doctorProfile.dImage.FileName;
            doctorDbContext.Add(doctorProfile);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(DoctorProfile));
        }

        public IActionResult Edit(int? id)
        {
            var doctors = doctorDbContext.doctorsProfiles.FirstOrDefault(e => e.id == id);
            return View(doctors);
        }

        [HttpPost]
        public IActionResult Edit(DoctorProfile doctorProfile)
        {
            if (doctorProfile.dImage != null)
            {
                var nam = Path.Combine(_env.WebRootPath + "/Images", Path.GetFileName(doctorProfile.dImage.FileName));
                doctorProfile.dImage.CopyTo(new FileStream(nam, FileMode.Create));
                doctorProfile.docImageUrl = "Images/" + doctorProfile.dImage.FileName;
            }
            doctorDbContext.Update(doctorProfile);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(DoctorProfile));
        }

        public IActionResult Details(int? id)
        {
            return View(doctorDbContext.doctorsProfiles.FirstOrDefault(e => e.id == id));
        }

        public IActionResult Delete(int? id)
        {
            return View(doctorDbContext.doctorsProfiles.FirstOrDefault(e => e.id == id));
        }
        [HttpPost]
        public IActionResult Delete(DoctorProfile doctorProfile)
        {
            doctorDbContext.Remove(doctorProfile);
            doctorDbContext.SaveChanges();
            return RedirectToAction(nameof(DoctorProfile));
        }
        public IActionResult DoctorProfile()
        {
            return View(doctorDbContext.doctorsProfiles.ToList());
        }
    }
}
