using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Queries
{
    /// <summary>
    /// Query manager that simplifies resolving and calling IQueryHandler instances
    /// </summary>
    public class QueryManager
    {
        private readonly IServiceProvider Provider;

        public QueryManager(IServiceProvider provider)
        {
            Provider = provider;
        }
        public IQueryHandler<T, TResult> Resolve<T, TResult>() where T : IQuery<TResult>
        {
            return (IQueryHandler<T, TResult>)Provider.GetService(typeof(IQueryHandler<T, TResult>));
        }

        public async Task<TResult> HandleAsync<T, TResult>(T input) where T : IQuery<TResult>
        {
            return await Resolve<T, TResult>().HandleAsync(input);
        }

        public TResult Handle<T, TResult>(T input) where T : IQuery<TResult>
        {
            return Resolve<T, TResult>().Handle(input);
        }
    }
}
