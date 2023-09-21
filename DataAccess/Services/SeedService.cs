using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class SeedService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeedService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task Seed()
        {
             if(!_unitOfWork.Product.GetAllAsync().Result.Any())
            {
                //Agregar 10 productos

                var productsData = new List<Product>
                {
                    new Product {Name = "Producto 1",Description = "Descripcion del producto 1",Price = 100,Quantity = 10},
                    new Product {Name = "Producto 2",Description = "Descripcion del producto 2",Price = 200,Quantity = 20},
                    new Product {Name = "Producto 3",Description = "Descripcion del producto 3",Price = 300,Quantity = 30},
                    new Product {Name = "Producto 4",Description = "Descripcion del producto 4",Price = 400,Quantity = 40},
                    new Product {Name = "Producto 5",Description = "Descripcion del producto 5",Price = 500,Quantity = 50},
                    new Product {Name = "Producto 6",Description = "Descripcion del producto 6",Price = 600,Quantity = 60},          
                    new Product {Name = "Producto 7",Description = "Descripcion del producto 7",Price = 700,Quantity = 70},
                    new Product {Name = "Producto 8",Description = "Descripcion del producto 8",Price = 800,Quantity = 80},
                    new Product {Name = "Producto 9",Description = "Descripcion del producto 9",Price = 900,Quantity = 90},
                    new Product {Name = "Producto 10",Description = "Descripcion del producto 10",Price = 1000,Quantity = 100}
                };


                var products = productsData.Select(prod => new Product
                {
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    Quantity = prod.Quantity
                }); 
                try
                {
                    _unitOfWork.Product.RemoveRange(_unitOfWork.Product.GetAllAsync().Result);
                    await _unitOfWork.Product.AddRangeAsync(products);
                    await _unitOfWork.Save();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message); 
                }
             }   
        }
    }
}
