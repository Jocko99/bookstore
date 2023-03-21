using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book : Entity
    {
        public string ISBN { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new HashSet<BookAuthor>();
    }
}
