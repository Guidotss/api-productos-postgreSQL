using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


public class Product{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Product(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}

namespace products_api.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        // GET: api/<SeedController>
        [HttpGet]
        
        public JsonResult Get()
        {
            var products = new Product[]
            {
                new Product("product1", "description1", 1.99m),
                new Product("product2", "description2", 2.99m),
                new Product("product3", "description3", 3.99m),
                new Product("product4", "description4", 4.99m),
                new Product("product5", "description5", 5.99m),
                new Product("product6", "description6", 6.99m),
                new Product("product7", "description7", 7.99m),
                new Product("product8", "description8", 8.99m),
                new Product("product9", "description9", 9.99m),
                new Product("product10", "description10", 10.99m),
            };
            return new JsonResult(products);    
        }
    }
}
