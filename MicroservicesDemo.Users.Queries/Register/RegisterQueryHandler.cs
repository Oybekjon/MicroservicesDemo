using MicroservicesDemo.Errors;
using MicroservicesDemo.Queries;
using MicroservicesDemo.Users.DataAccess;
using MicroservicesDemo.Users.DataAccess.Model;
using MicroservicesDemo.Users.Queries.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Users.Queries.Register
{
    public class RegisterQueryHandler : BusinessLogicQueryHandler<RegisterQuery, RegisterResult>
    {
        private readonly MainContext Context;
        private readonly IQueryHandler<HashPasswordQuery, HashPasswordResult> PasswordHasher;

        public RegisterQueryHandler(
            MainContext context,
            IQueryHandler<HashPasswordQuery, HashPasswordResult> passwordHasher)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            PasswordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }


        public override async Task<RegisterResult> HandleAsync(RegisterQuery input)
        {
            Guard.ParamNotNull(input, nameof(input));
            Guard.PropertyNotNullOrEmpty(input.Email, nameof(input.Email));
            Guard.PropertyNotNullOrEmpty(input.Password, nameof(input.Password));
            Guard.PropertyNotNullOrEmpty(input.PasswordConfirmation, nameof(input.PasswordConfirmation));
            Guard.IsTrue<ParameterInvalidException>(input.Password == input.PasswordConfirmation, "Passwords don't match");
            Guard.IsTrue<ParameterInvalidException>(input.Password.Length >= 6, "Password is too short");

            // Input is ok
            var exists = Context.Users.Any(x => x.Email == input.Email);
            if (exists)
            {
                throw new DuplicateEntryException("Email already exists");
            }
            var hashResult = await PasswordHasher.HandleAsync(new HashPasswordQuery
            {
                PlainPassword = input.Password
            });
            var user = new User
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                PasswordHash = hashResult.PasswordHash,
                Email = input.Email,
                UserType = DataAccess.Model.RoleType.User
            };

            Context.Users.Add(user);
            Context.SaveChanges();
            return new RegisterResult();
        }
    }
}
