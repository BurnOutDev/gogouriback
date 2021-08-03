using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BaseController
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        [HttpGet]
        public Task<JsonResponse<ICollection<AuthorResponse>>> GetAll()
        {
            return _authorService.GetAllAsync();
        }

        [HttpGet("[action]")]
        public async Task<JsonResponse<AuthorResponse>> ById([FromRoute] AuthorByIdRequest request)
        {
            return await _authorService.GetByIdAsync(request);
        }
        
        [HttpPost]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<JsonResponse>> Create(AuthorCreateRequest request)
        {
            return await _authorService.CreateAsync(request);
        }
        
        [HttpPut]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<JsonResponse>> Update(AuthorUpdateRequest request)
        {
            return await _authorService.UpdateAsync(request);
        }
        
        [HttpDelete]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<JsonResponse>> Delete(AuthorDeleteRequest request)
        {
            return await _authorService.DeleteAsync(request);
        }
    }
}

