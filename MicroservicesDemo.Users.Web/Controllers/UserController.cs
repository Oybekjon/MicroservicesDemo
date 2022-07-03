using MicroservicesDemo.Queries;
using MicroservicesDemo.Users.Queries.Register;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesDemo.Users.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly QueryManager QueryManager;
        public UserController(
            QueryManager queryManager)
        {
            QueryManager = queryManager ?? throw new ArgumentNullException(nameof(queryManager));
            
        }

        [Route("")]
        [HttpPost]
        public async Task<RegisterResult> Register(RegisterQuery input)
        {
            return await QueryManager.HandleAsync<RegisterQuery, RegisterResult>(input);
        }
    }
}
