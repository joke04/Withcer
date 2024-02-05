using Withcer.Models;

namespace Withcer.Services
{
    public interface IPostRatingService
    {
        void AddPostRating(PostRatings postRating, int userId);
        void UpdatePostRating(PostRatings postRating);
        void DeletePostRating(int ratingId);
    }

    public class PostRatingService : IPostRatingService
    {
        private readonly Data _link;

        public PostRatingService(Data link)
        {
            _link = link;
        }

        public void AddPostRating(PostRatings postRating, int userId)
        {
            postRating.UserID = userId;
            postRating.CreatedAt = DateTime.Now;
            postRating.DeletedAt = null;

            _link.PostRatings.Add(postRating);
            _link.SaveChanges();
        }

        public void DeletePostRating(int ratingId)
        {
            var postRating = _link.PostRatings.FirstOrDefault(x => x.RatingID == ratingId);

            if (postRating != null)
            {
                postRating.DeletedAt = DateTime.UtcNow;
                _link.PostRatings.Update(postRating);
                _link.SaveChanges();
            }
        }

        public void UpdatePostRating(PostRatings postRating)
        {
            _link.PostRatings.Update(postRating);
            _link.SaveChanges();
        }
    }
}