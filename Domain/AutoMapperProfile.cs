using AutoMapper;

using Domain.Entities;
using System.Linq;
using System.Text.Json;
using Domain.Models.Authors;
using Domain.Models.Books;

namespace Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BookCreateRequest, Book>();
            CreateMap<Book, BookResponse>()
                .ForMember(desc => desc.AuthorName, src => src.MapFrom(x => x.Author.Name.FirstOrDefault()));

            CreateMap<AuthorCreateRequest, Author>();
            
            CreateMap<Author, AuthorResponse>();
        }
    }

    public static class MapperExtensions
    {
        public static JsonElement ResolveJson(this string json)
        {
            return JsonSerializer.Deserialize<JsonElement>(json);
        }
    }
}
