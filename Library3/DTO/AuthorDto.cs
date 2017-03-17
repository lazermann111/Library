using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.DTO
{
    public class AuthorDto
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<BookDto> Books { get; set; }
    }
}