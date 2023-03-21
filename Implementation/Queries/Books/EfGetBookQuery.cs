using Application.DTO.Books;
using Application.Queries;
using Application.Queries.Books;
using Application.Searches.Books;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.Books
{
    public class EfGetBookQuery : EfQuery, IGetBookQuery
    {
        public EfGetBookQuery(BookContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator, Role.User };

        public int Id => 16;

        public string Name => "Show books using Ef.";

        public PageResponse<BookShowDto> Execute(BookSearch search)
        {
            var books = Context.Books.Include(x => x.Genre)
                                     .Include(x => x.BookAuthors)
                                     .ThenInclude(x => x.Author) 
                                      .AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
            {
                var criteria = search.Keyword.ToLower();
                books = books.Where(x => x.Title.ToLower().Contains(criteria) ||
                                         x.Description.ToLower().Contains(criteria) ||
                                         x.BookAuthors.Any(y => y.Author.FirstName.ToLower().Contains(criteria)) ||
                                         x.BookAuthors.Any(y => y.Author.LastName.ToLower().Contains(criteria)));
                                         
            }
            if (search.FromReleaseDate.HasValue)
            {
                books = books.Where(x => x.ReleaseDate >= search.FromReleaseDate.Value);
            }
            if (search.ToReleaseDate.HasValue)
            {
                books = books.Where(x => x.ReleaseDate <= search.ToReleaseDate.Value);
            }
            if (search.GenreId.HasValue)
            {
                books = books.Where(x => x.GenreId == search.GenreId);
            }

            return books.Paged<BookShowDto, Book>(search, Mapper);
        }
    }
}
