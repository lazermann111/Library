using Library3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library3.Repositories
{
    interface IAuthorReposiory
    {
        IEnumerable<Author> GetAll();
        Author Get(string id);
        Author Add(string name);
        void Remove(string id);
        bool Update(string authorId, string name);
    }
}
