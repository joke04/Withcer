using Withcer.Models;

namespace Withcer.Services
{
    public interface ICommentService 
    {
        void AddComment(Comments comments, int userId);
        void UpdatComment(Comments comments);
        void DeleteComment(int comments);
    }


    public class CommentService : ICommentService
    {
        
        
        private readonly Data _link;
        public CommentService(Data link)
        {
            _link = link;
        }
        public void AddComment(Comments comments, int userId)
        {
        comments.UserID = userId;
        comments.CreatedAt = DateTime.Now;
        comments.DeletedAt = null;

        _link.Comments.Add(comments);
        _link.SaveChanges();
        }

        public void DeleteComment(int CommentID)
        {
            var post = _link.Comments.FirstOrDefault(x => x.CommentID == CommentID);
            {
                post.DeletedAt = DateTime.UtcNow;
            };

            _link.Comments.Update(post);
            _link.SaveChanges();

        }

        public void UpdatComment(Comments comments)
        {
            _link.Comments.Update(comments);
            _link.SaveChanges();
        }
    }
}
