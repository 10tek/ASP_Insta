using Insta.Model;
using System;

namespace WebApp.Models.Post
{
    public class PostDetailViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public string PhotoPath { get; set; }

        public User User { get; set; }
    }
}