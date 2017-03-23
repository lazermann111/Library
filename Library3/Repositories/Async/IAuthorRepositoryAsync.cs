using Library3.DTO;
using Library3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library3.Repositories.Async
{
    interface IAuthorRepositoryAsync
    {
        Task <IEnumerable<AuthorDto>> GetAll(int page);
        Task <AuthorDto> Get(string id);
        Task Add(string name);
        Task Remove(string id);
        Task<bool> Update(string authorId, string name);
    }
}
