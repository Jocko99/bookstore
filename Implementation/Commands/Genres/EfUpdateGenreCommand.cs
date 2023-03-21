using Application.Commands.Genres;
using Application.DTO.Genres;
using Application.Exceptions;
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
    public class EfUpdateGenreCommand : EfCommand, IUpdateGenreCommand
    {
        private readonly UpdateGenreValidator _validate;
        private readonly IMapper _mapper;
        public EfUpdateGenreCommand(BookContext context, UpdateGenreValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 6;

        public string Name => "Edit genre using Ef.";

        public void Execute(GenreDto request)
        {
            var genre = Context.Genres.Find(request.Id);
            if(genre == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Genre));
            }
            _validate.ValidateAndThrow(request);
            _mapper.Map(request, genre);
            Context.SaveChanges();
        }
    }
}
