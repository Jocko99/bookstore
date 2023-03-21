using Application.DTO.Users;
using Application.Searches.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public interface IGetUserQuery : IQuery<UserSearch,PageResponse<UserDto>>
    {
    }
}
