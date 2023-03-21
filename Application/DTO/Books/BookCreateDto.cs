using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Books
{
    public class BookCreateDto : BookDto
    {
        public IEnumerable<BookAuthorDto> Authors { get; set; } = new List<BookAuthorDto>();
    }
}
