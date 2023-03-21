using AutoMapper;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public abstract class EfCommand
    {
        private readonly BookContext _context;
        protected EfCommand(BookContext context)
        {
            _context = context;
        }

        protected BookContext Context => _context;
    }
}
