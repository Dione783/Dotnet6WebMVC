using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Data;
using WebApplicationRazor.Models;
using WebApplicationRazor.Models.ViewModels;
using WebApplicationRazor.Services;
using WebApplicationRazor.Services.Exceptions;

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
            var ViewModel = new SellerFormViewModel() { Departments = departments };
            return View(ViewModel);
        }

        public IActionResult Error(string message)
        {
            ErrorViewModel error = new ErrorViewModel() { Message = message, RequestId =Activity.Current?.Id ?? HttpContext.TraceIdentifier};
            return View(error);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),new { Message="Id Not Provided"});
            }
            var obj = _sellerService.FindById(id.Value);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
                }
                Seller seller = _sellerService.FindById(id.Value);
                return View(seller);
            }
            catch(ApplicationException ex)
            {
                return RedirectToAction(nameof(Error),new {Message=ex.Message});
            }
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
            }

            Seller seller = _sellerService.FindById(id.Value);
            if(seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Seller Not Provided" });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel formViewModel = new SellerFormViewModel() { Seller=seller,Departments=departments };
            return View(formViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int ?id,Seller seller)
        {

            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException exception)
            {
                return RedirectToAction(nameof(Error), new { Message = exception.Message });
            }
        }
    }
}