using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            long totalOrders = await dbContext.Orders.LongCountAsync(cancellationToken);
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                .Skip(query.Request.PageIndex * query.Request.PageSize)
                .Take(query.Request.PageSize)
                .ToListAsync();

            var ordersDto = orders.ToOrderDtoList();
            var paginatedResult = new PaginatedResult<OrderDto>(query.Request.PageIndex, query.Request.PageSize, totalOrders, ordersDto);
            return new GetOrdersResult(paginatedResult);
        }
    }
}
