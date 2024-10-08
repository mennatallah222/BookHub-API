﻿using API.Infrastructure.Data;
using API.Infrastructure.Interfaces;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using ClassLibrary1.Data_ClassLibrary1.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Service.Implementations
{
    public class ProductService : IProductsService
    {
        private readonly IProductRepo _repo;
        private readonly ApplicationDBContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IProductRepo productRepo,
                              ApplicationDBContext dBContext,
                              IFileService fileService,
                              IHttpContextAccessor httpContextAccessor)
        {
            _repo = productRepo;
            _dbContext = dBContext;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repo.GetProductListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var products = await _repo.GetProductByIdAsync(id);

            return products;
        }

        public async Task<string> AddProductAsync(Product product, List<string> genreNames, IFormFile file)
        {
            var myContext = _httpContextAccessor.HttpContext.Request;
            var myBaseUrl = myContext.Scheme + "://" + myContext.Host;
            var imageUrl = await _fileService.UploadImage("Books", file);
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadTheImage": return "FailedToUploadTheImage";

            }
            product.Image = myBaseUrl + imageUrl;

            var existingGenres = await _dbContext.Categories.Where(c => genreNames.Contains(c.Name)).ToListAsync();
            foreach (var genre in existingGenres)
            {
                var existingGenre = existingGenres.FirstOrDefault(g => g.Name == genre.Name);
                if (existingGenres != null)
                {
                    product.BookGenres.Add(new BookGenre
                    {
                        Book = product,
                        Genre = existingGenre
                    });
                }
                else
                {
                    _dbContext.Categories.Add(new Category { Name = genre.Name });
                    product.BookGenres.Add(new BookGenre { Book = product, Genre = new Category { Name = genre.Name } });
                }
            }
            var prod = await _repo.GetTableNoTrasking().FirstOrDefaultAsync(x => x.Name.Equals(product.Name));

            if (prod != null)
            {
                return "Product already exists!";
            }
            await _repo.AddProduct(product);
            await _dbContext.SaveChangesAsync();
            return "Success";

        }
        public async Task<string> UpdateProductAsync(Product p, List<string> genreNames)
        {
            var existingGenres = await _dbContext.Categories.Where(c => genreNames.Contains(c.Name)).ToListAsync();

            _dbContext.Entry(p).State = EntityState.Detached;

            var existingProduct = await _dbContext.Products
                .Include(pr => pr.BookGenres)
                .FirstOrDefaultAsync(x => x.ProductId == p.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Name = p.Name;
                existingProduct.Description = p.Description;
                existingProduct.Price = p.Price;
                existingProduct.Quantity = p.Quantity;
                existingProduct.ISBN = p.ISBN;
                existingProduct.Image = p.Image;
                existingProduct.Author = p.Author;

                existingProduct.BookGenres.Clear();
                foreach (var genre in genreNames)
                {
                    var existingGenre = existingGenres.FirstOrDefault(g => g.Name == genre);
                    if (existingGenre != null)
                    {
                        existingProduct.BookGenres.Add(new BookGenre
                        {
                            Book = existingProduct,
                            Genre = existingGenre
                        });
                    }
                    else
                    {
                        var newGenre = new Category { Name = genre };
                        _dbContext.Categories.Add(newGenre);
                        existingProduct.BookGenres.Add(new BookGenre
                        {
                            Book = existingProduct,
                            Genre = newGenre
                        });
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return "Product is not found!";
            }
            return "Success";
        }



        public async Task<bool> IsNameExist(string name)
        {
            var res = _repo.GetTableNoTrasking().Where(x => x.Name.Equals(name)).FirstOrDefault();
            if (res != null) return true;
            return false;
        }

        public IQueryable<Product> GetProductsQueryable()
        {
            return _repo.GetTableNoTrasking().Include(p => p.BookGenres).Include(p => p.Reviews).AsQueryable();
        }

        public IQueryable<Product> FilterProductPaginationQueryable(ProductOrderingEnum orderingEnum, string search)
        {
            var queryable = _repo.GetTableNoTrasking().Include(p => p.BookGenres).Include(p => p.Reviews).AsQueryable();

            if (search != null)
            {
                queryable = queryable.Where(x => x.Name.Contains(search) ||
                                       x.BookGenres.Any(bg => bg.Genre.Name.Contains(search)) ||
                                       x.Price.ToString().Contains(search) ||
                                       x.Description.Contains(search));
            }

            switch (orderingEnum)
            {
                case ProductOrderingEnum.ProductId:
                    queryable = queryable.OrderBy(p => p.ProductId);
                    break;
                case ProductOrderingEnum.Price:
                    queryable = queryable.OrderBy(p => p.Price);
                    break;
                case ProductOrderingEnum.Quantity:
                    queryable = queryable.OrderBy(p => p.Quantity);
                    break;
                case ProductOrderingEnum.Name:
                    queryable = queryable.OrderBy(p => p.Name);
                    break;
                case ProductOrderingEnum.CategoryName:
                    queryable = queryable.OrderBy(p => p.BookGenres.FirstOrDefault().Genre.Name);
                    break;


            }

            return queryable;
        }


        public async Task<string> DeleteProductAsync(int productId)
        {
            var product = await _repo.GetProductByIdAsync(productId);
            if (product == null)
                return "Product is not found";

            await _repo.DeleteAsync(product);

            return "Product deleted successfully";
        }



    }
}
