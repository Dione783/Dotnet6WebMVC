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

        public List<Department> FindAll()
        {
            return _context.Departments.OrderBy(x => x.Name).ToList();
        }
    }
}
