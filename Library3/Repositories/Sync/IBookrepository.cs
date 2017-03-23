using System;
using Library3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library3.DTO;

namespace Library3.Repositories
{
    public interface IBookrepository
    {
        IEnumerable<BookDto> GetAll();
        BookDto Get(string id);
        void Add(string name, string authorId);
        void Remove(string id);
        bool Update(string id, string name, string authorId);
    }
}
