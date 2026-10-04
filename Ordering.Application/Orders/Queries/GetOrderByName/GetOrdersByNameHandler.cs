using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            //Get Orders By Name using the dbContext
            var orders = await dbContext.Orders
                                .Include(o => o.OrderItems)
                                .AsNoTracking()
                                .Where(o => o.OrderName.Value.Contains(query.Name))
                                .OrderBy(o => o.OrderName)
                                .ToListAsync();
            //var orderDtos =  ProjectToOrdersDto(orders);
            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }

        private List<OrderDto> ProjectToOrdersDto(List<Order> orders)
        {
            List<OrderDto> result = new();
            foreach (var order in orders)
            {
                var orderDto = new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderName: order.OrderName.Value,
                    ShippingAddress: new AddressDto(
                        order.ShippingAddress.FirstName ?? "",
                        order.ShippingAddress.LastName ?? "",
                        order.ShippingAddress.EmailAddres ?? "",
                        order.ShippingAddress.Addressline ?? "",
                        order.ShippingAddress.Country ?? "",
                        order.ShippingAddress.State ?? "",
                        order.ShippingAddress.ZipCode ?? ""
                        ),
                    BillingAddress: new AddressDto(
                        order.BillingInformation.FirstName ?? "",
                        order.BillingInformation.LastName ?? "",
                        order.BillingInformation.EmailAddres ?? "",
                        order.BillingInformation.Addressline ?? "",
                        order.BillingInformation.Country ?? "",
                        order.BillingInformation.State ?? "",
                        order.BillingInformation.ZipCode ?? ""
                        ),
                    Payment: new PaymentDto(
                        order.Payment.CardName ?? "",
                        order.Payment.CardNumber ?? "",
                        order.Payment.Expiration ?? "",
                        order.Payment.CVV ?? "",
                        order.Payment.PaymentMethod ?? ""
                        ),
                    Status: order.Status,
                    OrderItems: order.OrderItems
                                .Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price))
                                .ToList()
                    );

                result.Add(orderDto);
            }
            return result;
        }
    }
}
