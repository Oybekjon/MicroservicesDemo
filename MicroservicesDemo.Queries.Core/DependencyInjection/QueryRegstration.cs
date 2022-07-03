using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Queries.DependencyInjection
{
    public static class QueryRegstration
    {
        private static readonly Type ContainerType = typeof(IServiceProvider);

        private static readonly MethodInfo ContainerMethod = ContainerType.GetMethod("GetService");

        /// <summary>
        /// Registers all QueryHandler implementations automatically and 
        /// wraps them into logger decorator. Called upon startup
        /// </summary>
        public static void RegisterQueries(this IServiceCollection services, string queriesAssemblyName)
        {
            // Load queries assembly
            var assembly = Assembly.Load(queriesAssemblyName);
            // target interface to search for
            var interfaceType = typeof(IQueryHandler<,>);
            // Implementation base type
            var bllType = typeof(BusinessLogicQueryHandler<,>);
            // find all implementations (not equal to base class itself)
            var types = assembly.GetTypes().Where(x => x.InheritsOrImplements(bllType) && x != bllType).ToList();

            foreach (var type in types)
            {
                // Register the query
                services.RegisterQuery(type);
            }

            services.AddScoped<QueryManager>();
        }

        /// <summary>
        /// Creates a compiled factory method using Expressions
        /// Most performing variant, as fast as actually calling
        /// new SomeClass();
        /// </summary>
        /// <param name="type">Type to register</param>
        private static void RegisterQuery(this IServiceCollection services, Type type)
        {
            // Get all interfaces and 
            var interfaces = type.GetInterfaces();
            // retrieve the IQueryHandler
            var targetInt = interfaces.FirstOrDefault(x => x.Name == "IQueryHandler`2");
            // Extract generic arguments
            var args = targetInt.GetGenericArguments();
            // Final generic interface
            var queryInterface = typeof(IQueryHandler<,>).MakeGenericType(args);
            // Final generic logger
            var loggerType = typeof(LoggingDecorator<,>).MakeGenericType(args);
            // retrieval of logger constructor
            var loggerConstructor = loggerType.GetConstructors().First();
            // retrieval of the implementation constructor
            var implConstructor = type.GetConstructors().First();
            // parameter of the factory method
            var param = Expression.Parameter(ContainerType, "x");

            // build the "new" call for implementation
            var implExpr = BuildConstructor(param, implConstructor, null);
            var implConverted = Expression.Convert(implExpr, queryInterface);

            // Build logger "new" call
            var loggerImpl = BuildConstructor(param, loggerConstructor, implConverted);
            var loggerConverted = Expression.Convert(loggerImpl, queryInterface);

            // prepare the body of the factory method
            var body = Expression.Convert(loggerConverted, typeof(object));

            // Get method lambda expression
            var methodExpr = Expression.Lambda<Func<IServiceProvider, object>>(body, param);
            // compile into actual C# method
            var method = methodExpr.Compile();

            // Register the factory method with the interface type
            services.AddScoped(queryInterface, method);
        }

        /// <summary>
        ///  Builds a "new" call exression for a given constructor. 
        ///  Adds resolution for each parameter if needed
        /// </summary>
        /// <param name="containerParam"></param>
        /// <param name="ctor"></param>
        /// <param name="queryInstanceExpression"></param>
        /// <returns></returns>
        private static Expression BuildConstructor(Expression containerParam, ConstructorInfo ctor, Expression queryInstanceExpression)
        {
            var paramList = new List<Expression>();
            // Get all parameters that this constructor requires
            var ctorParams = ctor.GetParameters();
            // foreach parameter
            foreach (var param in ctorParams)
            {
                // build initialization
                // if the IQueryHandler`2 parameter is provided
                if (param.ParameterType.InheritsOrImplements(typeof(IQueryHandler<,>)) && queryInstanceExpression != null)
                {
                    paramList.Add(queryInstanceExpression);
                }
                else
                {
                    var paramTypeConst = Expression.Constant(param.ParameterType);
                    var resolveParam = Expression.Call(containerParam, ContainerMethod, paramTypeConst);
                    var convertExpr = Expression.Convert(resolveParam, param.ParameterType);

                    // adding to parameter list
                    paramList.Add(convertExpr);
                }
            }
            // expression new SomeQuery(x.Resolve(dependency1), x.Resolve(dependency2));
            return Expression.New(ctor, paramList);
        }
    }
}
