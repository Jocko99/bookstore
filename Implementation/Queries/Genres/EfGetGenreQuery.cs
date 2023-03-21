using Application.DTO.Genres;
using Application.Queries;
using Application.Queries.Genre;
using Application.Searches.Genre;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.Genres
{
    public class EfGetGenreQuery : EfQuery, IGetGenreQuery
    {
        public EfGetGenreQuery(BookContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator, Role.User };

        public int Id => 8;

        public string Name => "Get genre using Ef.";

        public PageResponse<GenreDto> Execute(GenreSearch search)
        {
            var genre = Context.Genres.AsQueryable();
            if(!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
            {
                genre = genre.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }
            return genre.Paged<GenreDto, Genre>(search, Mapper);
        }
    }
}
