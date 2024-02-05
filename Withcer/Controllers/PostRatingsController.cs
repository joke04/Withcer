using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Withcer.Models;
using Withcer.Services;

namespace Withcer.Controllers
{
    [Route("API/Controller")]
    public class PostRatingController : ControllerBase
    {
        private readonly Data _db;
        private readonly IJwtService _jwtService;
        private readonly ITokenService _tokenService;
        private readonly IPostRatingService _postRatingService;

        public PostRatingController(ITokenService tokenService, Data db, IPostRatingService postRatingService)
        {
            _tokenService = tokenService;
            _db = db;
            _postRatingService = postRatingService;
        }

        [HttpPost("AddPostRating")]
        public IActionResult AddPostRating(PostRatingAddRequest postRatingAddRequest)
        {
            if (postRatingAddRequest != null)
            {
                if (string.IsNullOrEmpty(_tokenService.Token))
                {
                    return Ok("Для начала войдите или зарегистрируйтесь");
                }

                int userId = ExtractUserIdFromToken(_tokenService.Token);

                if (!ModelState.IsValid)
                {
                    return BadRequest("Некорректные данные пользователя");
                }

                var newPostRating = new PostRatings
                {
                    PostID = postRatingAddRequest.PostID,
                    Rating = postRatingAddRequest.Rating,
                };

                _postRatingService.AddPostRating(newPostRating, userId);

                return Ok($"Вы успешно оставили рейтинг!");
            }
            return BadRequest();
        }

        [HttpPost("UpdatePostRating")]
        public IActionResult UpdatePostRating(PostRatingAddRequest postRatingAddRequest)
        {
            if (postRatingAddRequest != null)
            {
                if (string.IsNullOrEmpty(_tokenService.Token))
                {
                    return Ok("Для начала войдите или зарегистрируйтесь");
                }

                int userId = ExtractUserIdFromToken(_tokenService.Token);

                if (!ModelState.IsValid)
                {
                    return BadRequest("Некорректные данные пользователя");
                }

                var updatedPostRating = new PostRatings
                {
                    PostID = postRatingAddRequest.PostID,
                    Rating = postRatingAddRequest.Rating,
                };

                _postRatingService.UpdatePostRating(updatedPostRating);

                return Ok($"Вы успешно обновили рейтинг!");
            }
            return BadRequest();
        }

        [HttpPut("DeletePostRating")]
        public IActionResult DeletePostRating(int ratingId)
        {
            if (string.IsNullOrEmpty(_tokenService.Token))
            {
                return Ok("Для начала войдите или зарегистрируйтесь");
            }

            int userId = ExtractUserIdFromToken(_tokenService.Token);

            if (!ModelState.IsValid)
            {
                return BadRequest("Некорректные данные пользователя");
            }

            _postRatingService.DeletePostRating(ratingId);

            return Ok($"Вы успешно удалили рейтинг!");
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