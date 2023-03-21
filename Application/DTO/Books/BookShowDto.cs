using Application.DTO.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Books
{
    public class BookShowDto : BookDto
    {
        public string Genre { get; set; }
        public IEnumerable<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    }
}
