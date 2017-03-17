using System;
using Library3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library3.Repositories
{
    interface IBookrepository
    {
        IEnumerable<Book> GetAll();
        Book Get(string id);
        Book Add(string name, string authorId);
        void Remove(string id);
        bool Update(string id, string name, string authorId);
    }
}
