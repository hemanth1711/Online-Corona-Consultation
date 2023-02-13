using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Consultation.Models
{
    public class DoctorProfile    //// hemanth
    {
        public int id { get; set; }

        [NotMapped]
        public IFormFile dImage { get; set; }

        public string docImageUrl { get; set; }

        [DisplayName("Doctor Name")]
        public string Docname { get; set; }

        [DisplayName("Speciality")]
        public string speciality { get; set; }
        
        [DisplayName("Email")]
        public string email{ get; set; }

        [DisplayName("Fees")]
        public int fees { get; set; }
        [DisplayName("Available Slots")]
        public string avail { get; set; }

    }


    public class Appointment        //    hemanth  
    {
        public int id { get; set; }

        public DateTime Date { get; set; }
        [ForeignKey("patient")]
        public int pid { get; set; }
        public PatientProfile patient { get; set; }

        [ForeignKey("doctor")]

        public int did { get; set; }
        public DoctorProfile doctor { get; set; }
    }

    public class Medicine     // mayank
    {
        public int id { get; set; }

        [DisplayName("Medicine Name")]
        public string mname { get; set; }

        [DisplayName("Medicine Price")]
        public int mprice { get; set; }

        [DisplayName("Manufacturing Date")]
        public DateTime mfg { get; set; }

        [DisplayName("Expiry Date")]
        public DateTime exp { get; set; }
    }
    public class Department   // sakshi
    {
        public int id { get; set; }

        [DisplayName("Department Name")]
        public string dname { get; set; }


    }
    public class Employee   // sakshi
    {
        public int id { get; set; }

        [DisplayName("Employee Name")]
        public string Empname { get; set; }
        [ForeignKey("department")]
        public int DepId { get; set; }
        public Department department { get; set; }
    }
    
    public class PatientProfile     // nandini
    {
        public int id { get; set; }

        [DisplayName("Patient Name")]
        public string pname { get; set; }

        [NotMapped]
        public IFormFile pImage { get; set; }

        public string pImageUrl { get; set; }

        [DisplayName("Residential Address")]
        public string address { get; set; }

        [DisplayName("Mobile Number")]
        public string Mobile{ get; set; }

        public string Email { get; set; }

    }
    //public class Patientreport
    //{
    //    public int id { get; set; }


    //    [ForeignKey("patient")]
    //    public int pid { get; set; }
    //    public PatientProfile patient { get; set; }

    //    [ForeignKey("doctor")]

    //    public int did { get; set; }
    //    public DoctorProfile doctor { get; set; }

    //    [ForeignKey("medicine")]

    //    public int mid { get; set; }
    //    public Medicine medicine { get; set; }

    //}
    
    // appointment

    public class Feedback  //
    {
        public int id { get; set; }
        [ForeignKey("patient")]
        public int pid { get; set; }
        public PatientProfile patient{ get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        public byte rating { get; set; }
        public DateTime feedbackTime { get; set; }
    }

    public class Service //  nandini
    {
        public int id { get; set; }

        [DisplayName("Name of Service")]
        public string name { get; set; }

        [DisplayName("Description")]
        public string serviceDetail { get; set; }
    }

    public class Billing   // hemanth
    {
        public int id { get; set; }

        public DateTime billingdate { get; set; }
        [ForeignKey("medicine")]
        public int mid { get; set; }

        public string mdesc { get; set; }

        public Medicine medicine { get; set; }

        [ForeignKey("patient")]
        public int pid { get; set; }
        public PatientProfile patient { get; set; }

        [ForeignKey("doctor")]

        public int did { get; set; }
        public DoctorProfile doctor { get; set; }

        [DisplayName("Total Fees")]
        public int totalFee { get; set; }

    }
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options) { }
        public DbSet<DoctorProfile> doctorsProfiles { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Medicine> medicines { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PatientProfile> patientProfiles { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Billing> billings { get; set; }
    }

}
