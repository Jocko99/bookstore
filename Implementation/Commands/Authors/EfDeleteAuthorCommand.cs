using Application.Commands.Authors;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Authors
{
    public class EfDeleteAuthorCommand : EfCommand, IDeleteAuthorCommand
    {
        public EfDeleteAuthorCommand(BookContext context) : base(context)
        {
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 3;

        public string Name => "Delete author using Ef";

        public void Execute(int request)
        {
            var author = Context.Authors.Include(x => x.BookAuthors).FirstOrDefault(x => x.Id == request);
            if(author == null)
            {
                throw new EntityNotFoundException(request, typeof(Author));
            }
            if (author.BookAuthors.Any())
            {
                throw new ConflictException(request, typeof(Author));
            }
            Context.Authors.Remove(author);
            Context.SaveChanges();
        }
    }
}
