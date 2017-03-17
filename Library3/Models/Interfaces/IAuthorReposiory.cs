using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library3.Models
{
    interface IAuthorReposiory
    {
        IEnumerable<Author> GetAll();
        Author Get(int id);
        Author Add(Book item);
        void Remove(int id);
        bool Update(Author item);
    }
}
