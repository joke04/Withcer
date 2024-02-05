using Withcer.Services;
using Withcer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Withcer.Controllers
{
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        
        private readonly Data _db;
        private readonly IJwtService _jwtService;
        private readonly ITokenService _tokenService;
        private readonly IPostService _postService;
        public PostController(IJwtService jwtService, ITokenService tokenService, Data db, IPostService postService)
        {
            _jwtService = jwtService;
            _tokenService = tokenService;
            _db = db;
            _postService = postService;
        }

        [HttpPost("AddPost")]

        public IActionResult AddPost ( PostAddRequest postAddRequest)
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

            var newPost = new Post
            {
                PostName = postAddRequest.PostName,
                Text = postAddRequest.Content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,

            };


            _postService.AddPost (newPost);
            

            return Ok($"Вы успешно создали пост !");
        }

        [HttpPut("UpdatePost")]
        public IActionResult UpdatePost(PostAddRequest postAddRequest)
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

            var editPost = new Post
            {
                PostName = postAddRequest.PostName,
                Text = postAddRequest.Content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,

            };


            _postService.UpdatePost(editPost);


            return Ok($"Вы успешно обновили пост !");
        }

        [HttpPut("DeletePost")]
        public IActionResult DeletePost(int PostId)
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


            _postService.DeletePost(PostId);


            return Ok($"Вы успешно удалили пост !");
        }


        [HttpPost("Find-Post-By-Index")]
        public IActionResult FindPostByIndex(int postId)
        {
            if (postId < 0)
            {
                return BadRequest("Введен неверный индекс!");
            }

            var post = _postService.GetPostById(postId);

            if (post == null)
            {
                return NotFound($"Спорта с индексом {postId} не существует!");
            }

            return Ok($"Спорт с индексом {postId} найден: {post.PostName}");
        }

        [HttpPost("Find-Post-By-Name")]
        public IActionResult AddPost(string postName)
        {
            if (postName == null)
            {
                return BadRequest("Введите имя вида спорта!");
            }

            var post = _postService.GetPostByName(postName);

            if (post == null)
            {
                return NotFound("Такого вида спорта нет!");
            }

            return Ok("Такой вид спорта есть!");
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
