using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Withcer.Models;
using Withcer.Services;

namespace Withcer.Controllers
{
    [Route("API/Controller")]
    public class CommentController : ControllerBase
    {
        private readonly Data _db;
        private readonly IJwtService _jwtService;
        private readonly ITokenService _tokenService;
        private readonly ICommentService _commentService;

        public CommentController( ITokenService tokenService, Data db, ICommentService commentService)
        {
            
            _tokenService = tokenService;
            _db = db;
            _commentService = commentService;
        }
        [HttpPost("addComment")]
        public IActionResult AddComment(CommentAddrequest commentAddrequest)
        { 
            if (commentAddrequest != null)
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

                var newComment = new Comments
                {
                    PostID = commentAddrequest.PostID,
                    Text = commentAddrequest.Text,

                };


                _commentService.AddComment(newComment, userId);


                return Ok($"Вы успешно оставили отзыв !");
            }
            return BadRequest();
        }

        [HttpPost("UpdateComment")]
        public IActionResult UpdateComment(CommentAddrequest commentAddrequest)
        {
            if (commentAddrequest != null)
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

                var newComment = new Comments
                {
                    PostID = commentAddrequest.PostID,
                    Text = commentAddrequest.Text,
            

                };


                _commentService.AddComment(newComment, userId);


                return Ok($"Вы успешно обновили отзыв !");
            }
            return BadRequest();
        }

        [HttpPut("DeleteComment")]
        public IActionResult DeleteComment(int CommentID)
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


            _commentService.DeleteComment(CommentID);


            return Ok($"Вы успешно удалили пост !");
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
