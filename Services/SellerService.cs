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

        public List<Seller> FindAll()
        {
            return _context.Seller.Include(obj => obj.Department).ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int Id){
            try
            {
                if (!_context.Seller.Any(x => x.Id == Id))
                {
                    throw new NotFoundException("Id not Found");
                }
                return _context.Seller.Include(obj => obj.Department).FirstOrDefault(s => s.Id == Id);
            }
            catch (ApplicationException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public void Remove(int Id){
            Seller seller = _context.Seller.Find(Id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if(!_context.Seller.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbConcurrencyException(exception.Message);
            }
        }
    }
}
