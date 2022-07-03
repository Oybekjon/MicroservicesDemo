using MicroservicesDemo.Items.DataAccess;
using MicroservicesDemo.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Items.Queries.InsertItem
{
    public class InsertItemQueryHandler : BusinessLogicQueryHandler<InsertItemQuery, InsertItemResult>
    {
        private readonly ItemsContext Context;

        public InsertItemQueryHandler(ItemsContext context)
        {
            Context = context;
        }

        public override Task<InsertItemResult> HandleAsync(InsertItemQuery input)
        {
            throw new NotImplementedException();
        }
    }
}
