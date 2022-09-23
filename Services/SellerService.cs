using WebApplicationRazor.Data;
using WebApplicationRazor.Models;
using Microsoft.EntityFrameworkCore;
using WebApplicationRazor.Services.Exceptions;

namespace WebApplicationRazor.Services
{
    public class SellerService
    {
        private readonly DataContext _context;

        public SellerService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.Include(obj => obj.Department).ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int Id){
            try
            {
                if (!_context.Seller.Any(x => x.Id == Id))
                {
                    throw new NotFoundException("Id not Found");
                }
                return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(s => s.Id == Id);
            }
            catch (ApplicationException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task RemoveAsync(int Id){
            try{
                Seller seller = await _context.Seller.FindAsync(Id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }catch(DbUpdateException e){
                throw new IntegrityException(e.Message);
            }
            
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if(!hasAny)
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbConcurrencyException(exception.Message);
            }
        }
    }
}
