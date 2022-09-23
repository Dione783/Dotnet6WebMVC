using WebApplicationRazor.Data;
using WebApplicationRazor.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int Id){

            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(s => s.Id == Id);
        }

        public void Remove(int Id){
            Seller seller = _context.Seller.Find(Id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }
    }
}
