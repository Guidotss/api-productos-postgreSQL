using DataAccess.Repository.IRepository;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;


namespace products_api.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly SeedService seedService;
        public SeedController(IUnitOfWork unitOfWork)
        {
            seedService = new SeedService(unitOfWork);
        }
        [HttpGet]   
        public async Task<IActionResult>SeedData()
        {
            try
            {
                await seedService.Seed();
                return Ok(new { ok = true, message = "Seeding data was successful" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message }); 
            }
        }
        
    }
}
