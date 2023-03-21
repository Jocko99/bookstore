using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches.Books
{
    public class BookSearch : PageSearch
    {
        public DateTime? FromReleaseDate { get; set; }
        public DateTime? ToReleaseDate { get; set; }
        public int? GenreId { get; set; }
    }
}
