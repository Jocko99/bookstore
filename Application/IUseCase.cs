using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUseCase
    {
        IEnumerable<Role> Roles { get; }
        int Id { get; }
        string Name { get; }
    }
}
