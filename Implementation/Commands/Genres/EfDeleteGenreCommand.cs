using Application.Commands.Genres;
using Application.Exceptions;
using Domain.Entities;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Genres
{
    public class EfDeleteGenreCommand : EfCommand, IDeleteGenreCommand
    {
        public EfDeleteGenreCommand(BookContext context) : base(context)
        {
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 7;

        public string Name => "Delete genre using Ef.";

        public void Execute(int request)
        {
            var genre = Context.Genres.Include(x => x.Books).FirstOrDefault(x => x.Id == request);
            if(genre == null)
            {
                throw new EntityNotFoundException(request, typeof(Genre));
            }
            if (genre.Books.Any())
            {
                throw new ConflictException(request, typeof(Genre));
            }
            Context.Genres.Remove(genre);
            Context.SaveChanges();
        }
    }
}
