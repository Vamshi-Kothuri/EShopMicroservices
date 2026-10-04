using BuildingBlocks.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest Request) : IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginatedResult<OrderDto> Ordes);
}
