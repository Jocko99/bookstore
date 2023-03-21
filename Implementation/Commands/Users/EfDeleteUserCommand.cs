using Application.Commands.Users;
using Application.Exceptions;
using Domain.Entities;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Users
{
    public class EfDeleteUserCommand : EfCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(BookContext context) : base(context)
        {
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin };

        public int Id => 11;

        public string Name => "Delete user using Ef.";

        public void Execute(int request)
        {
            var user = Context.Users.Find(request);
            if(user == null)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }
            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
