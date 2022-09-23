using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Models;

namespace WebApplicationRazor.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Seller>? Seller { get; set; }
        public DbSet<SalesRecord>? SalesRecord { get; set; }
    }
}