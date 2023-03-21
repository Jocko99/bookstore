using Application.DTO.Authors;
using Application.DTO.Books;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, BookShowDto>()
                .ForMember(src => src.Authors, opt => opt.MapFrom(x => x.BookAuthors.Select(y => new AuthorDto
                {
                    Id = y.Author.Id,
                    FirstName = y.Author.FirstName,
                    LastName = y.Author.LastName,
                    Biography = y.Author.Biography,
                    CoverPhoto = y.Author.CoverPhoto
                })))
                .ForMember(src => src.Genre, opt => opt.MapFrom(x => x.Genre.Name))
                .ForMember(src => src.ReleaseDate, opt => opt.MapFrom(x => x.ReleaseDate.ToShortDateString()));
            CreateMap<BookCreateDto, Book>()
                .ForMember(src => src.BookAuthors, opt => opt.MapFrom(x => x.Authors.Select(y => new BookAuthor
                {
                    BookId = x.Id,
                    AuthorId = y.AuthorId
                })));
            CreateMap<BookDto, Book>();
            CreateMap<BookAuthorDto, BookAuthor>();
        }
    }
}
