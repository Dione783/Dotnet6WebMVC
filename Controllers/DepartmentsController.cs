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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }
    }
}