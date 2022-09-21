using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Data;

namespace WebApplicationRazor.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DataContext _context;

        public DepartmentsController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Departments.ToListAsync());
        }
    }
}