using MicroservicesDemo.Errors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Queries
{
    // this is logging decorator and encapsulates the most common logging 
    // routines
    public class LoggingDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> Decoratee;
        private readonly ILogger<IQueryHandler<TQuery, TResult>> Logger;
        public LoggingDecorator(IQueryHandler<TQuery, TResult> decoratee, ILogger<IQueryHandler<TQuery, TResult>> logger)
        {
            Guard.ArgNotNull(decoratee, nameof(decoratee));
            Guard.ArgNotNull(logger, nameof(logger));
            Decoratee = decoratee;
            Logger = logger;
        }

        public TResult Handle(TQuery input)
        {
            return AsyncHelper.RunSync(() => HandleAsync(input));
        }

        public async Task<TResult> HandleAsync(TQuery input)
        {
            try
            {
                return await Decoratee.HandleAsync(input);
            }
            catch (DebugException ex)
            {
                Logger.LogDebug(ex, ex.Message);
                throw;
            }
            catch (ErrorException ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
            catch (FatalException ex)
            {
                Logger.LogCritical(ex, ex.Message);
                throw;
            }
            catch (WarningException ex)
            {
                Logger.LogWarning(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Unknown exception {ex.Message}");
                throw;
            }
        }
    }
}
