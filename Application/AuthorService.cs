using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Entities;
using Domain.Models;
using Domain.Models.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Persistence;

namespace Application
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(ApplicationDbContext context, IMapper mapper, ILogger<AuthorService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<JsonResponse<ICollection<AuthorResponse>>> GetAllAsync()
        {
            var authors = await _context.Authors
                .Where(t => !t.DeletedAt.HasValue)
                .AsNoTracking()
                .ProjectTo<AuthorResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new JsonResponse<ICollection<AuthorResponse>>(authors);
        }

        public async Task<JsonResponse<AuthorResponse>> GetByIdAsync(AuthorByIdRequest request)
        {
            var author = await _context.Authors
                .Where(t => !t.DeletedAt.HasValue)
                .Where(c => c.Id == request.Id)
                .AsNoTracking()
                .ProjectTo<AuthorResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            var result = new JsonResponse<AuthorResponse>(author);

            return result;
        }

        public async Task<JsonResponse> CreateAsync(AuthorCreateRequest request)
        {
            var author = _mapper.Map<Author>(request);

            await _context.AddAsync(author);
            await _context.SaveChangesAsync();

            return JsonResponse.IsSuccess();
        }

        public async Task<JsonResponse> UpdateAsync(AuthorUpdateRequest request)
        {
            try
            {
                var author = await _context.Authors.FindAsync(request.Id)
                             ?? throw new NullReferenceException("Author with given Id can not be found");

                author.Name = request.Name;

                _context.Update(author);
                await _context.SaveChangesAsync();
            }
            catch (NullReferenceException ex)
            {
                return JsonResponse.Error(ex.Message);
            }
            
            return JsonResponse.IsSuccess();
        }

        public async Task<JsonResponse> DeleteAsync(AuthorDeleteRequest request)
        {
            var result = new JsonResponse();

            try
            {
                var author = await _context.Authors.FindAsync(request.Id)
                    ?? throw new Exception("Author with given Id can not be found.");

                author.DeletedAt = DateTime.Now;

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