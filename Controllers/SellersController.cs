using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Data;
using WebApplicationRazor.Models;
using WebApplicationRazor.Models.ViewModels;
using WebApplicationRazor.Services;

namespace WebApplicationRazor.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: Sellers
        public IActionResult Index()
        {
              List<Seller> list = _sellerService.FindAll();
              return View(list);
        }

        public IActionResult Create()
        {
            ICollection<Department> departments = _departmentService.FindAll();
            var ViewModel = new SellerFormViewModel() { Departments=departments};
            return View(ViewModel);
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

    }
}
