using Microsoft.AspNetCore.Mvc;
using WebApplicationRazor.Models;
using WebApplicationRazor.Services;

namespace WebApplicationRazor.Controllers;

public class SalesRecordsController : Controller{

    private readonly SalesRecordService _salesRecordService;
    public SalesRecordsController(SalesRecordService salesRecordsService)
    {   
        _salesRecordService=salesRecordsService;
    }

    public IActionResult Index(){
        return View();
    }

    public async Task<IActionResult> SimpleSearch(DateTime? minDate,DateTime? maxDate)
    {
        if(!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year,1,1);
        }
        if(!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }
        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
        List<SalesRecord> sales = await _salesRecordService.FindByDateAsync(minDate,maxDate);
        return View(sales);
    }

    public IActionResult GroupingSearch(){
        return View();
    }
}