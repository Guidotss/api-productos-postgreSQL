using DataAccess.Dtos;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace products_api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfwork;
        
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
            
        }
        
        [HttpGet]
        public async Task<IActionResult>GetAllProducts()
        {   
            try
            {
                var products = await _unitOfwork.Product.GetAllAsync();
                return Ok(new { ok = true, products }); 
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message }); 
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _unitOfwork.Product.GetAsync(id);
                if(product == null)
                {
                    return NotFound(new { ok = false, message = "Product with id: " + id + " not found" }); 
                }

                return Ok(new { ok = true, product }); 
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message }); 
            }
        }

        [HttpPost]
        public async Task<IActionResult>CreateProduct([FromBody] ProductDto product)
        {
            if(product == null)
            {
                return BadRequest(new { ok = false, message = "Product is required" }); 
            }

            try
            {
                var newProduct = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity
                };

                await _unitOfwork.Product.AddAsync(newProduct);
                await _unitOfwork.Save();
                return Ok(new { ok = true, message = "Product created successfully", newProduct });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message }); 
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct (Guid id, [FromBody] UpdateProductDto productData)
        {
            if(productData == null)
            {
                BadRequest(new { ok = false, message = "Product data is required" }); 
            }

            try
            {
                var product = _unitOfwork.Product.GetAsync(id).Result;
                if(product == null)
                {
                    return NotFound(new { ok = false, message = "Product with id: " + id + " not found" }); 
                }

                product.Name = productData.Name ?? product.Name;
                product.Description = productData.Description ?? product.Description;
                product.Price = productData.Price ?? product.Price;
                product.Quantity = productData.Quantity ?? product.Quantity;

                _unitOfwork.Product.Update(product);
                await _unitOfwork.Save();
                return Ok(new { ok = true, message = "Product updated successfully", product });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message }); 
            }   
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _unitOfwork.Product.GetAsync(id);
                Console.WriteLine(id); 
                if(product == null)
                {
                    return NotFound(new { ok = false, message = "Product with id: " + id + " not found" }); 
                }

                _unitOfwork.Product.Remove(product);
                await _unitOfwork.Save();
                return Ok(new { ok = true, message = "Product deleted successfully" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }
    }
}
