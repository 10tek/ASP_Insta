using System;

namespace Insta.Model
{
    public class Comment : EntityModel
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}