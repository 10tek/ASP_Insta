using Insta.Model;
using System;
using System.Collections.Generic;

namespace WebApp.Models.Post
{
    public class PostIndexViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Insta.Model.Comment> Comments { get; set; }
        public ICollection<Insta.Model.Like> Likes { get; set; }
        public string PhotoPath { get; set; }
        public string Data { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }
}