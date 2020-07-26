using System;

namespace WebApp.Models.Comment
{
    public class CommentCreateViewModel
    {
        public string Text { get; set; }
        public Guid PostId { get; set; }
        public string CommentAuthor { get; set; }
    }
}