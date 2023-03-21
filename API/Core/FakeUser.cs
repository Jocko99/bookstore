using Application;
using Domain.Entities;

namespace API.Core
{
    public class FakeUser : IApplicationActor
    {
        public int Id => 1;

        public string Identity => Role.Admin.ToString();

        public Role Role => Role.Admin;
    }
}
