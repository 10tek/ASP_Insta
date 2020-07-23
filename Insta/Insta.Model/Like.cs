using System;

namespace Insta.Model
{
    public class Like : EntityModel
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
    }
}