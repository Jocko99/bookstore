using Application.Commands.Genres;
using Application.DTO.Genres;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Genres
{
    public class EfCreateGenreCommand : EfCommand, ICreateGenreCommand
    {
        private readonly CreateGenreValidator _validate;
        private readonly IMapper _mapper;
        public EfCreateGenreCommand(BookContext context, CreateGenreValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 5;

        public string Name => "Create genre using Ef.";

        public void Execute(GenreDto request)
        {
            _validate.ValidateAndThrow(request);
            Context.Genres.Add(_mapper.Map<Genre>(request));
            Context.SaveChanges();
        }
    }
}
