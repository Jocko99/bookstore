using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Authors
{
    public class AuthorDto : Dto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CoverPhoto { get; set; }
        public string Biography { get; set; }
    }
}
