using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Queries
{
    /// <summary>
    /// Primary query handler for encapsulating business logic
    /// </summary>
    /// <typeparam name="TQuery">
    /// Query input containing input parameters
    /// </typeparam>
    /// <typeparam name="TResult">Result of the query execution</typeparam>
    public abstract class BusinessLogicQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Synchronous handle method. By default implemented to call
        /// async version, however can be overriden in subclass to provide
        /// Synchronous implementation
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual TResult Handle(TQuery input)
        {
            return AsyncHelper.RunSync(() => HandleAsync(input));
        }

        /// <summary>
        /// Asynchronous handling implementation
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract Task<TResult> HandleAsync(TQuery input);
    }
}
