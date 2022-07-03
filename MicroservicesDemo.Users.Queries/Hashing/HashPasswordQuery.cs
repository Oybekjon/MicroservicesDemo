using MicroservicesDemo.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Users.Queries.Hashing
{
    public class HashPasswordQuery:IQuery<HashPasswordResult>
    {
        public string PlainPassword { get; set; }
    }
}
