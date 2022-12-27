using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApplication.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostBody { get; set; }
        public int? UserId { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfComments { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
