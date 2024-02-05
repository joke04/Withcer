using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Withcer.Models;

namespace Withcer.Services
{
      public interface IPostService
      {
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int postId);
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int postId);
        Post GetPostByName(string name);
        
    }

    public class PostService : IPostService
    {
        private readonly Data _link;
        public PostService(Data link)
        {
            _link = link;
        }
        public void AddPost(Post post)
        {
            _link.Posts.Add(post);
            _link.SaveChanges();

        }
        public void UpdatePost(Post post)
        { 
            _link.Posts.Update(post);
            _link.SaveChanges();

        }

        public void DeletePost(int postId)
        {
            var post = _link.Posts.FirstOrDefault(x => x.PostId == postId);
            {
                post.DeletedAt = DateTime.UtcNow;
            };

            _link.Posts.Update(post);
            _link.SaveChanges();

        }


        public IEnumerable<Post> GetAllPosts()
        {
            return _link.Posts;
        }

        public Post GetPostById(int postId)
        {
            return _link.Posts.FirstOrDefault(x => x.PostId == postId);
        }

        public Post GetPostByName(string name)
        {
            return _link.Posts.FirstOrDefault(x => x.PostName == name);
        }



    }





}
