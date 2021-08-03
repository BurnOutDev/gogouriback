using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Authors;

namespace Application.Abstractions
{
    public interface IAuthorService
    {
        Task<JsonResponse<ICollection<AuthorResponse>>> GetAllAsync();
        Task<JsonResponse> CreateAsync(AuthorCreateRequest request);
        Task<JsonResponse> DeleteAsync(AuthorDeleteRequest request);
        Task<JsonResponse<AuthorResponse>> GetByIdAsync(AuthorByIdRequest request);
        Task<JsonResponse> UpdateAsync(AuthorUpdateRequest request);
    }
}