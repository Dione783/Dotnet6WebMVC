using WebApplicationRazor.Data;
using WebApplicationRazor.Models;

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

    }
}
