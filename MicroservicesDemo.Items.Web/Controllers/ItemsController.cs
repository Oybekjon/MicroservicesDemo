using MicroservicesDemo.Items.Queries.InsertItem;
using MicroservicesDemo.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesDemo.Items.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly QueryManager QueryManager;

        public ItemsController(QueryManager queryManager)
        {
            QueryManager = queryManager ?? throw new ArgumentNullException(nameof(queryManager));
        }

        [Route("")]
        [HttpPost]
        public async Task<InsertItemResult> Register(InsertItemQuery input)
        {
            return await QueryManager.HandleAsync<InsertItemQuery, InsertItemResult>(input);
        }
    }
}
