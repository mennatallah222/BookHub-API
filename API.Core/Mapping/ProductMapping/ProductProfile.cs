using API.Core.Features.Commands.Models;
using API.Core.Features.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Core.Mapping.ProductMapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetAllBooksResponses>()
                .ForMember(dest => dest.GenreNames, opt => opt.MapFrom(src => src.BookGenres != null && src.BookGenres.Any() ? src.BookGenres.Select(g => g.Genre.Name).ToList() : new List<string> { "No genres" }))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews.Select(c => c.Content).ToList()))
                ;


            CreateMap<CreateBookCommand, Product>()
                .ForMember(dest => dest.BookGenres, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (dest.BookGenres == null)
                    {
                        dest.BookGenres = new List<BookGenre>();
                    }
                    if (src.GenresNames != null && src.GenresNames.Any())
                    {
                        foreach (var c in src.GenresNames)
                        {
                            var category = new Category { Name = c };
                            var bookGenre = new BookGenre { Genre = category };
                            dest.BookGenres.Add(bookGenre);
                        }

                    }
                });

            CreateMap<UpdateBookCommand, Product>()
     .ForMember(dest => dest.BookGenres, opt => opt.Ignore())
     .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
     .AfterMap((src, dest) =>
     {
         if (src.BookGenres != null && src.BookGenres.Any())
         {
             dest.BookGenres = src.BookGenres.Select(gn => new BookGenre
             {
                 Genre = new Category { Name = gn }
             }).ToList();
         }

         // Logging to verify BookGenres
         Console.WriteLine("BookGenres after mapping:");
         foreach (var bg in dest.BookGenres)
         {
             Console.WriteLine($"Genre: {bg.Genre.Name}");
         }
     });


        }
    }
}
