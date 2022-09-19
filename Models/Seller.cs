using System.Linq;
using System.Collections.Generic;

namespace WebApplicationRazor.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
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
