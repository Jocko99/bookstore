using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Books
{
    public class BookDto : Dto
    {
        public string ISBN { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public string ReleaseDate { get; set; }
        
    }
}
