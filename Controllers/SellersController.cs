using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            List<Seller> list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ICollection<Department> departments = await _departmentService.FindAllAsync();
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
        public async Task<IActionResult> Create(Seller seller)
        {
            if(ModelState.IsValid){
                List<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel(){Seller=seller,Departments=departments};
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),new { Message="Id Not Provided"});
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try{
                await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
            }catch(IntegrityException e){
                return RedirectToAction(nameof(Error),new {Message = e.Message});
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
                }
                Seller seller = await _sellerService.FindByIdAsync(id.Value);
                return View(seller);
            }
            catch(ApplicationException ex)
            {
                return RedirectToAction(nameof(Error),new {Message=ex.Message});
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id Not Provided" });
            }

            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if(seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Seller Not Provided" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel formViewModel = new SellerFormViewModel() { Seller=seller,Departments=departments };
            return View(formViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ?id,Seller seller)
        {
            if(ModelState.IsValid){
                List<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel(){Seller=seller,Departments=departments};
                return View(viewModel);
            }
            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException exception)
            {
                return RedirectToAction(nameof(Error), new { Message = exception.Message });
            }
        }
    }
}