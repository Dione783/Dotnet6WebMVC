using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Data;
using WebApplicationRazor.Models;

namespace WebApplicationRazor.Services;

public class SalesRecordService {

    private readonly DataContext context;
    public SalesRecordService(DataContext _context)
    {
        context=_context;
    }

    public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate,DateTime? maxDate)
    {
        var result = from obj in context.SalesRecord select obj;
        if(minDate.HasValue)
        {
            result = result.Where(x => x.Date >= minDate.Value);
        }
        if(maxDate.HasValue)
        {
            result = result.Where(x => x.Date <= maxDate);
        }
        return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
    }


}