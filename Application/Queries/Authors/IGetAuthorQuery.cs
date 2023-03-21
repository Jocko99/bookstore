using Application.DTO.Authors;
using Application.Searches.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Authors
{
    public interface IGetAuthorQuery : IQuery<AuthorSearch,PageResponse<AuthorDto>>
    {
    }
}
