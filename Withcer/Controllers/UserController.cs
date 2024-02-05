using Withcer.Services;
using Withcer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Withcer.Controllers
{
    
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Data _db;
        private readonly IJwtService _jwtService;
        private readonly ITokenService _tokenService;
        public UserController(IJwtService jwtService, ITokenService tokenService, Data db)
        { 
            _jwtService = jwtService;
            _tokenService = tokenService;
            _db = db;
        }

        [HttpPost("Registration")]
        public IActionResult UserRegistration( UserRegistrationRequest userRegistrationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Некорректные данные пользователя");
            }

            var newUser = new User
            {
                FirstName = userRegistrationRequest.FirstName,
                LastName = userRegistrationRequest.LastName,
                Email = userRegistrationRequest.Email,
                Password = userRegistrationRequest.Password,
                Gender = userRegistrationRequest.Gender,
                DateOfBirth = userRegistrationRequest.DateOfBirth,
                Role = userRegistrationRequest.Role,
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            var tokenString = _jwtService.GenerateToken(newUser);
            _tokenService.Token = tokenString;

            return Ok($"Вы успешно зарегистрированы! Добро пожаловать, {newUser.FirstName} {newUser.LastName}!");
        }

        [HttpPost("EditData")]
        public IActionResult EditUser( UserRegistrationRequest userUpdateRequest)
        {
            if (string.IsNullOrEmpty(_tokenService.Token))
            {
                return Ok("Для начала войдите или зарегистрируйтесь");
            }
            int userId = ExtractUserIdFromToken(_tokenService.Token);
            var existingUser =_db.Users.FirstOrDefault(u => u.UserId == userId);
            if (existingUser == null)
            {
                return NotFound("Пользователь не найден");
            }


            existingUser.FirstName = userUpdateRequest.FirstName;
            existingUser.LastName = userUpdateRequest.LastName;
            existingUser.DateOfBirth = userUpdateRequest.DateOfBirth;
            existingUser.Gender = userUpdateRequest.Gender;

            _db.SaveChanges();


            return Ok($"Данные пользователя {existingUser.FirstName} {existingUser.LastName} успешно обновлены");
        }
        private int ExtractUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);

            var userIdClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == "sub");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            throw new InvalidOperationException("Unable to extract user id from token.");
        }

    }
}