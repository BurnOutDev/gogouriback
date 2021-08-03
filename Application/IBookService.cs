using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Books;

namespace Application
{
    public interface IBookService
    {
        Task<JsonResponse<ICollection<BookResponse>>> GetAllAsync();
        Task<JsonResponse> CreateAsync(BookCreateRequest request);
        Task<JsonResponse> DeleteAsync(BookDeleteRequest request);
        Task<JsonResponse<BookResponse>> GetByIdAsync(BookByIdRequest request);
        Task<JsonResponse> UpdateAsync(BookUpdateRequest request);
    }
}