using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.DTO;
using BlogApi.Interfaces;
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
        [Route("api/Posts")]
        public async Task<ActionResult<IEnumerable<Post>>> All()
            => Ok(await dbService.Posts());

        [HttpGet]
        [Route("api/Posts/{id}")]
        public async Task<ActionResult<Post>> ById([FromRoute] int id)
            => Ok(await dbService.PostById(id));


        [HttpDelete]
        [Route("api/Posts/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await dbService.DeletePost(id);
            return Ok();
        }

        [HttpPost]
        [Route("api/Posts")]
        public async Task<ActionResult<Post>> Create([FromBody] Post post)
            => Ok(await dbService.CreatePost(post));

        [HttpPut]
        [Route("api/Posts")]
        public async Task<ActionResult<Post>> Edit([FromBody] Post post)
            => Ok(await dbService.EditPost(post));
    }
}