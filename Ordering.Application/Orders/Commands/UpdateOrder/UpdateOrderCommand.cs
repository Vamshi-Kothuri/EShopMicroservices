using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand
    (OrderDto Order): ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSucess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("OrderId is Required");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order Name is Required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer Name is Required");
        }
    }
}
