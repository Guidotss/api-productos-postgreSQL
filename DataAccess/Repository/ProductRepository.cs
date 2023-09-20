using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db; 
        
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }

        public void Update(Product product)
        {
            var productFromDb = _db.Product.FirstOrDefault(prod => prod.Id == product.Id); 
            if(productFromDb != null)
            {
                productFromDb.Name = product.Name;
                productFromDb.Description = product.Description;
                productFromDb.Price = product.Price;
                productFromDb.Quantity = product.Quantity;

                _db.SaveChanges(); 
            }
        }
    }
}