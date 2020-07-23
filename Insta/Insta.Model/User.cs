using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insta.Model
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
