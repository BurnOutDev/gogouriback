using System.Collections.Generic;
using System.Threading.Tasks;

using Application;

using Domain.Entities;
using Domain.Models;
using Domain.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public Task<JsonResponse<ICollection<BookResponse>>> GetAll()
        {
            return _bookService.GetAllAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<JsonResponse>> ById(BookByIdRequest request)
        {
            return await _bookService.GetByIdAsync(request);
        }

        [HttpPost]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<JsonResponse>> Create(BookCreateRequest request)
        {
            return await _bookService.CreateAsync(request);
        }

        [HttpPut]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<JsonResponse>> Update(BookUpdateRequest request)
        {
            return await _bookService.UpdateAsync(request);
        }

        [HttpDelete]
        [Authorize(Role.Admin)]
        public async Task<ActionResult<JsonResponse>> Delete(BookDeleteRequest request)
        {
            return await _bookService.DeleteAsync(request);
        }
    }
}
