using AutoMapper;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public abstract class EfQuery
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        protected EfQuery(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        protected BookContext Context => _context;
        protected IMapper Mapper => _mapper;
    }
}
