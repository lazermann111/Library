﻿using Library3.DTO;
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
        IEnumerable<AuthorDto> GetAll();
        AuthorDto Get(string id);
        void Add(string name);
        void Remove(string id);
        bool Update(string authorId, string name);
    }
}
