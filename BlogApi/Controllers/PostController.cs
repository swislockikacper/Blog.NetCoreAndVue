using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.DTOs;
using BlogApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IDatabaseService dbService;

        public PostController(IDatabaseService dbService)
        {
            this.dbService = dbService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Posts")]
        public async Task<ActionResult<IEnumerable<Post>>> All()
            => Ok(await dbService.Posts());

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Posts/{id}")]
        public async Task<ActionResult<Post>> ById([FromRoute] int id)
            => Ok(await dbService.PostById(id));


        [HttpDelete]
        [Authorize]
        [Route("api/Posts/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await dbService.DeletePost(id);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("api/Posts")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Post>> Create([FromForm] Post post)
            => Ok(await dbService.CreatePost(post));

        [HttpPut]
        [Authorize]
        [Route("api/Posts")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Post>> Edit([FromForm] Post post)
            => Ok(await dbService.EditPost(post));
    }
}