using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.DTO
{
    public class BookDto : BookBaseDto
    { 
        public virtual AuthorDto Author { get; set; }
    }
}