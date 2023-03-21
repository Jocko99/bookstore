using Application.Commands.Books;
using Application.Exceptions;
using Domain.Entities;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Books
{
    public class EfDeleteBookCommand : EfCommand, IDeleteBookCommand
    {
        public EfDeleteBookCommand(BookContext context) : base(context)
        {
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 16;

        public string Name => "Delete book using Ef.";

        public void Execute(int request)
        {
            var book = Context.Books.Include(x => x.BookAuthors).Where(x => x.Id == request);
            if(book == null)
            {
                throw new EntityNotFoundException(request, typeof(Book));
            }
            Context.RemoveRange(book);
            Context.SaveChanges();
        }
    }
}
