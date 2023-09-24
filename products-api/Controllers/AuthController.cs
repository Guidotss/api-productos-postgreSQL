using DataAccess.Dtos;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace products_api.Controllers
{
    [Route("api/auth/")]
    [ApiController, Produces("application/json")]
    public class AuthController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userData)
        {
            try
            {
                var userFromDb = await _unitOfWork.User.GetUserByEmail(userData.Email);

                if (userFromDb != null)
                {
                    return Ok(new { ok = true, user = new { name = userFromDb.Name, email = userFromDb.Email } });
                }
                return BadRequest(new { ok = false, message = "User not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userData)
        {
            try
            {
                var userFromDb = await _unitOfWork.User.GetUserByEmail(userData.Email);

                if (userFromDb != null)
                {
                    return BadRequest(new { ok = false, message = "User already exists" });
                }

                var newUser = new Models.User
                {
                    Name = userData.Name,
                    Email = userData.Email,
                    Password = _unitOfWork.User.HashPassword(userData.Password)
                };

                await _unitOfWork.User.AddAsync(newUser);
                await _unitOfWork.Save();
                return Ok(new { ok = true, user = new { name = newUser.Name, email = newUser.Email } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }

    }
}
