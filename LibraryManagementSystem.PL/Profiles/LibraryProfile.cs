using AutoMapper;
using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.PL.Models;

namespace LibraryManagementSystem.PL.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<AuthorViewModel, Author>()
                .ForMember(d => d.Id, o => o.Ignore());
            CreateMap<Author, AuthorViewModel>();

            CreateMap<BookViewModel,Book>()
                .ForMember(d=>d.Id,o=>o.Ignore());
            CreateMap<Book, BookViewModel>();
        }
    }
}
