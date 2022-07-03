using MicroservicesDemo.Queries;
using MicroservicesDemo.Users.Queries.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Users.Queries.Hashing
{
    public class HashPasswordQueryHandler : BusinessLogicQueryHandler<HashPasswordQuery, HashPasswordResult>
    {
        private readonly HashConfig Config;

        public HashPasswordQueryHandler(HashConfig config)
        {
            Config = config;
        }

        public override Task<HashPasswordResult> HandleAsync(HashPasswordQuery input)
        {
            var helper = new EncryptionHelper();
            var encryptedHash = helper.EncryptedHash(input.PlainPassword, Config.Secret, EncryptionProvider.HMACSHA384);
            return Task.FromResult(new HashPasswordResult
            {
                PasswordHash = encryptedHash
            });
        }
    }
}
