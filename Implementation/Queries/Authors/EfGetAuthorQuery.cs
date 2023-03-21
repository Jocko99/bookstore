using Application.DTO.Authors;
using Application.Queries;
using Application.Queries.Authors;
using Application.Searches.Authors;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.Authors
{
    public class EfGetAuthorQuery : EfQuery, IGetAuthorQuery
    {
        public EfGetAuthorQuery(BookContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator, Role.User };

        public int Id => 4;

        public string Name => "Get authors using Ef.";

        public PageResponse<AuthorDto> Execute(AuthorSearch search)
        {
            var author = Context.Authors.AsQueryable();
            if(!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
            {
                var criteria = search.Keyword.ToLower();
                author = author.Where(x => x.FirstName.ToLower().Contains(criteria) ||
                                           x.LastName.ToLower().Contains(criteria));
            }
            if (search.FromDate.HasValue)
            {
                author = author.Where(x => x.CreatedAt >= search.FromDate.Value);
            }
            if (search.ToDate.HasValue)
            {
                author = author.Where(x => x.CreatedAt <= search.ToDate.Value);
            }

            return author.Paged<AuthorDto, Author>(search, Mapper);
        }
    }
}
