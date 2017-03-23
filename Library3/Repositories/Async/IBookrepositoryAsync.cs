using System;
using Library3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library3.DTO;

namespace Library3.Repositories.Async
{
    interface IBookrepositoryAsync
    {
        Task <IEnumerable<BookDto>> GetAll();
        Task <BookDto> Get(string id);
        Task Add(string name, string authorId);
        Task Remove(string id);
        Task<bool> Update(string id, string name, string authorId);
    }
}
