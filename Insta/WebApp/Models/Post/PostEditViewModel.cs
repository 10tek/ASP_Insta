using System;

namespace WebApp.Models.Post
{
    public class PostEditViewModel
    {
        public Guid Id { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public string PhotoPath { get; set; }
    }
}