using System;

namespace Insta.Model
{
    public abstract class EntityModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
