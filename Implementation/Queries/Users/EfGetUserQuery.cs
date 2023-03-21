using Application.DTO.Users;
using Application.Queries;
using Application.Queries.Users;
using Application.Searches.Users;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.Users
{
    public class EfGetUserQuery : EfQuery, IGetUserQuery
    {
        public EfGetUserQuery(BookContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin };

        public int Id => 12;

        public string Name => "Get users using Ef.";

        public PageResponse<UserDto> Execute(UserSearch search)
        {
            var user = Context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
            {
                var criteria = search.Keyword.ToLower();
                user = user.Where(x => x.FirstName.ToLower().Contains(criteria) ||
                                       x.LastName.ToLower().Contains(criteria) ||
                                       x.Email.ToLower().Contains(criteria) ||
                                       x.UserName.ToLower().Contains(criteria));
            }
            if (search.FromDate.HasValue)
            {
                user = user.Where(x => x.CreatedAt >= search.FromDate.Value);
            }
            if (search.ToDate.HasValue)
            {
                user = user.Where(x => x.CreatedAt <= search.ToDate.Value);
            }
            return user.Paged<UserDto, User>(search, Mapper);
        }
    }
}
