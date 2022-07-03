using MicroservicesDemo.Errors;
using MicroservicesDemo.Queries;
using MicroservicesDemo.Users.Queries.Authentication;
using MicroservicesDemo.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicesDemo.Users.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuthController : ControllerBase
    {
        private readonly QueryManager QueryManager;
        private readonly JwtService JwtService;
        private readonly AuthSettings AuthSettings;

        public OAuthController(
            QueryManager queryManager,
            JwtService jwtService,
            AuthSettings authSettings)
        {
            QueryManager = queryManager ?? throw new ArgumentNullException(nameof(queryManager));
            JwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            AuthSettings = authSettings ?? throw new ArgumentNullException(nameof(authSettings));
        }

        [Route("token")]
        [HttpPost]
        public async Task<TokenResult> Authenticate(
            [FromForm(Name = "grant_type")] string grantType,
            [FromForm(Name = "username")] string username,
            [FromForm(Name = "password")] string password
        )
        {
            var result = await QueryManager
                .HandleAsync<AuthenticateQuery, AuthenticateResult>(new AuthenticateQuery
                {
                    GrantType = grantType,
                    UserName = username,
                    Password = password
                });

            var token = JwtService.GenerateSecurityToken(result.FirstName, result.LastName, result.Role.ToString(), result.Email, result.UserId);
            return new TokenResult
            {
                AccessToken = token,
                ExpiresIn = (int)TimeSpan.FromMinutes(AuthSettings.ExpirationInMinutes).TotalSeconds
            };
        }
    }
}
