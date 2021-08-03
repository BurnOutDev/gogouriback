using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Entities;
using Domain.Models;
using Domain.Models.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Persistence;

namespace Application
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(ApplicationDbContext context, IMapper mapper, ILogger<BookService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JsonResponse<ICollection<BookResponse>>> GetAllAsync()
        {
            var books = await _context.Books
                .Where(t => !t.DeletedAt.HasValue)
                .AsNoTracking()
                .ProjectTo<BookResponse>(_mapper.Config)
                .ToListAsync();

            return new JsonResponse<ICollection<BookResponse>>(books);
        }

        public async Task<JsonResponse<BookResponse>> GetByIdAsync(BookByIdRequest request)
        {
            var book = await _context.Books
                .Where(t => !t.DeletedAt.HasValue)
                .Where(c => c.Id == request.Id)
                .AsNoTracking()
                .ProjectTo<BookResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            
            var result = new JsonResponse<BookResponse>(book);

            return result;
        }

        public async Task<JsonResponse> CreateAsync(BookCreateRequest request)
        {
            var book = _mapper.Map<Book>(request);

            await _context.AddAsync(book);
            await _context.SaveChangesAsync();

            return JsonResponse.IsSuccess();
        }

        public async Task<JsonResponse> UpdateAsync(BookUpdateRequest request)
        {
            try
            {
                var book = await _context.Books.FindAsync(request.Id)
                           ?? throw new NullReferenceException("Book with given Id can not be found");

                var updated =_mapper.Map(request, book);

                _context.Update(updated);
                await _context.SaveChangesAsync();
            }
            catch (NullReferenceException ex)
            {
                return JsonResponse.Error(ex.Message);
            }
            return JsonResponse.IsSuccess();
        }

        public async Task<JsonResponse> DeleteAsync(BookDeleteRequest request)
        {
            var result = new JsonResponse();

            try
            {
                var book = await _context.Books.FindAsync(request.Id)
                    ?? throw new Exception("Book with given Id can not be found.");

                book.DeletedAt = DateTime.Now;

                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = JsonResponse.Error(ex.Message);
            }
            return result;
        }
    }
}