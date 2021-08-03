using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
