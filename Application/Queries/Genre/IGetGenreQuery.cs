using Application.DTO.Genres;
using Application.Searches.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Genre
{
    public interface IGetGenreQuery : IQuery<GenreSearch,PageResponse<GenreDto>>
    {
    }
}
