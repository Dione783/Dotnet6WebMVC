using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationRazor.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(60,MinimumLength = 3,ErrorMessage = "{0} Deve Conter no minimo {2} caracteres e no máximo {1} caracteres")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(60,MinimumLength = 3,ErrorMessage = "{0} Deve Conter no minimo {2} caracteres e no máximo {1} caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Display(Name="Base Salary")]
        [Required(ErrorMessage = "{0} Required")]
        [Range(0.01,50000.00,ErrorMessage = "{0} Deve Conter no minimo {2} caracteres e no máximo {1} caracteres")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        [Display(Name="Department")]
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; }

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSalles(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void removeSalles(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial,DateTime final)
        {
            return Sales.Where(sr => sr.Date.Date >= initial.Date && sr.Date.Date <= final.Date).Sum(sr => sr.Amount);
        }
    }
}
