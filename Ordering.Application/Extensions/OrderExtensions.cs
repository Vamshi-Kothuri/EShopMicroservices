using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderDto(
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
                ));
        }
    }
}
