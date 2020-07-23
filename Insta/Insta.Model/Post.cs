using System.Collections.Generic;

namespace Insta.Model
{
    public class Post : EntityModel
    {
        public string Data { get; set; }
        public string PhotoPath { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}