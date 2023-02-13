using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Consultation.Models;
using System.Diagnostics;
using System.Dynamic;
using Stripe.Infrastructure;
using Razorpay.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Online_Consultation.Controllers
{
    public class HomeController : Controller
    {
        public const string raz_key = "rzp_test_cfElqRcKNLh6aA";
        public const string raz_secret = "3QaoZiwflNJbQftFonT55elT";
        private readonly ILogger<HomeController> _logger;
        public DoctorDbContext _doctorDbContext;
        public HomeController(ILogger<HomeController> logger, DoctorDbContext doctorDbContext)
        {
            _logger = logger;
            _doctorDbContext = doctorDbContext;
        }

        public IActionResult Index()
        {
            //var user = UserManager.FindByEmail(Email);
            return View();
        }
        //Admin
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult getAppointment(int? id) 
        {
            DoctorProfile dc = _doctorDbContext.doctorsProfiles.FirstOrDefault(c => c.id == id);
            string us = HttpContext.User.Identity.Name.ToString();
            PatientProfile p = _doctorDbContext.patientProfiles.FirstOrDefault(p => p.Email == us);
            Appointment a = new Appointment();
            a.Date = DateTime.Now;
            a.pid = p.id;
            a.patient = p;
            a.doctor = dc;
            a.did = dc.id;
            return View(a);
        }
        public IActionResult ConfirmAppointment(int? id)
        {
            DoctorProfile dc = _doctorDbContext.doctorsProfiles.FirstOrDefault(c => c.id == id);
            string us = HttpContext.User.Identity.Name.ToString();
            PatientProfile p = _doctorDbContext.patientProfiles.FirstOrDefault(p => p.Email == us);

            //Create Card Object to create Token

            //Assign Card to Token Object and create Token  

            var oid = Createorder(dc);
            PayOptions pay = new PayOptions()
            {
                key = raz_key,
                Amountinsub = dc.fees * 100,
                currency = "INR",
                name = "Global Logic Health+",
                Description = "A Good",
                ImageUrl = "",
                Orderid = oid,
                productid = dc.id

            };
            return View(pay);
        }

        public IActionResult Success(PayOptions pay)
        {
           Billing b =  new Billing();
            var doctor = _doctorDbContext.doctorsProfiles.FirstOrDefault(p => p.id == pay.productid);
            string us = HttpContext.User.Identity.Name.ToString();
            PatientProfile p = _doctorDbContext.patientProfiles.FirstOrDefault(p => p.Email == us);
            b.pid = p.id;
            b.did = doctor.id;
            b.doctor = doctor;
            b.patient = p;
            b.billingdate = DateTime.UtcNow;
            b.mid = 1;
            b.mdesc = " ";
            b.totalFee = doctor.fees;
            _doctorDbContext.billings.Add(b);
            _doctorDbContext.SaveChanges();
            return View(b);
        }
        //[HttpPost]
        //public IActionResult Success(Appointment a)
        //{
        //    a.Date = DateTime.Now;
        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult Billing(int? id)
        {
            //string us = HttpContext.User.Identity.Name.ToString();
            //PatientProfile p = _doctorDbContext.patientProfiles.FirstOrDefault(p => p.Email == us);
            //DoctorProfile dc = _doctorDbContext.doctorsProfiles.FirstOrDefault(c => c.id == id);
            //Billing a = new Billing();
            //a.billingdate = DateTime.Now;
            //a.doctor = dc;
            //a.did = dc.id;
            //a.mid = 1;
            //a.billingdate = DateTime.UtcNow;
            //a.mid = 1;
            //a.pid = p.id;
            //a.mdesc = " ";
            //a.totalFee = dc.fees;
            //_doctorDbContext.billings.Add(a);
            //_doctorDbContext.SaveChanges();
            //int b = a.id;
            //a.id= b;
            return View();
        }

        //[HttpPost]
        //public IActionResult Billing(Billing b)
        //{
        //    Billing dc = _doctorDbContext.billings.FirstOrDefault(c => c.id == b.id);
        //    dc.mdesc = b.mdesc;
        //    _doctorDbContext.billings.Update(dc);
        //    _doctorDbContext.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        [Authorize]
        public IActionResult MyAppointments()
        {
            string us = HttpContext.User.Identity.Name.ToString();
            PatientProfile p = _doctorDbContext.patientProfiles.FirstOrDefault(p => p.Email == us);
            List<Billing> b = _doctorDbContext.billings.Where(c => c.pid == p.id).Include(d=>d.doctor).Include(p=>p.patient).OrderByDescending(p=>p.billingdate).ToList();
            return View(b);
        }







            public String Createorder(DoctorProfile products)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(raz_key, raz_secret);
                Dictionary<string, object> input = new Dictionary<string, object>();
                input.Add("amount", products.fees * 100);
                input.Add("currency", "INR");
                input.Add("receipt", "12121");

                Order ord_Res = client.Order.Create(input);
                var oid = ord_Res.Attributes["id"].ToString();
                return oid;

            }
            catch
            {
                return null;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}