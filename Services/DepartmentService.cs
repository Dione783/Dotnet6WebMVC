using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Data;
using WebApplicationRazor.Models;

namespace WebApplicationRazor.Services
{
    public class DepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Departments.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
