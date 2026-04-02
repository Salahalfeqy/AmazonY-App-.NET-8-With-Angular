using AmazonY.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonY.API.Controllers
{

    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var category = await work.CategoryRepository.GetByIdAsync(100);
            if (category == null) { return NotFound(); }
            return Ok(category);
        }

        [HttpGet("Server-Error")]

        public async Task<ActionResult> GetServerError()
        {
            var category = await work.CategoryRepository.GetByIdAsync(100);
            category.Name = "";//
            return Ok(category);
        }
        [HttpGet("Bad-Request/{id}")]

        public async Task<ActionResult> GetBadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("Bad-Request")]

        public async Task<ActionResult> GetBadRequest()
        {
            return BadRequest();
        }

    }
}
