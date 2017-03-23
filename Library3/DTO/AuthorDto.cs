using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library3.DTO
{
    public class AuthorDto : AuthorBaseDto
    {       
        public  ICollection<BookDto> Books { get; set; }
    }
}