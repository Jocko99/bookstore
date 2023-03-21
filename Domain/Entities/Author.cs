using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CoverPhoto { get; set; }
        public string Biography { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();
    }
}
