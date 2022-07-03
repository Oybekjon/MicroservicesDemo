using MicroservicesDemo.Errors;
using MicroservicesDemo.Queries;
using MicroservicesDemo.Users.DataAccess;
using MicroservicesDemo.Users.Queries.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Users.Queries.Authentication
{
    public class AuthenticateQueryHandler : BusinessLogicQueryHandler<AuthenticateQuery, AuthenticateResult>
    {
        private readonly MainContext Context;
        private readonly IQueryHandler<HashPasswordQuery, HashPasswordResult> Hasher;

        public AuthenticateQueryHandler(MainContext context, IQueryHandler<HashPasswordQuery, HashPasswordResult> hasher)
        {
            Context = context;
            Hasher = hasher;
        }

        public override async Task<AuthenticateResult> HandleAsync(AuthenticateQuery input)
        {
            Guard.ParamNotNull(input, nameof(input));
            Guard.PropertyNotNullOrEmpty(input.UserName, nameof(input.UserName));
            Guard.PropertyNotNullOrEmpty(input.Password, nameof(input.Password));
            Guard.PropertyNotNullOrEmpty(input.GrantType, nameof(input.GrantType));
            Guard.IsTrue<Errors.ParameterInvalidException>(input.GrantType == "password", "\"password\" is the only allowed grant type");
            var hashedPassword = await Hasher.HandleAsync(new HashPasswordQuery { PlainPassword = input.Password });
            var user = Context.Users.FirstOrDefault(x => x.Email == input.UserName && x.PasswordHash == hashedPassword.PasswordHash);
            if (user == null)
            {
                throw new AuthException("User not found");
            }
            return new AuthenticateResult
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = (RoleType)(int)user.UserType,
                UserId = user.UserId,
                Email = user.Email
            };
        }
    }
}
