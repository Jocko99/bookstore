﻿using Application.DTO.Books;
using Application.Searches.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Books
{
    public interface IGetBookQuery : IQuery<BookSearch,PageResponse<BookShowDto>>
    {
    }
}
