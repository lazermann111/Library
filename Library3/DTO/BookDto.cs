using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.DTO
{
    public class BookDto
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual AuthorDto Author { get; set; }
    }
}