namespace SocialMediaApplication.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }

        public string CommentBody { get; set; }
        public int NumberOfLikes { get; set; }

    }
}
