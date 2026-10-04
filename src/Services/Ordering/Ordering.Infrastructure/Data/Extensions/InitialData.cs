using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("aa483fc5-60c0-40cd-a12f-56ad268a0b5a")), "Jane", "Jane.Foster@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("7ed9db57-8e8e-42e2-81ba-4ae9c1406814")), "Thor", "Thor.Alaxander@gmail.com")
            };

        //Products
        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("e17ab324-f5d5-4cfc-95e8-ac708a3d01e5")), "Oppo", 15000),
                Product.Create(ProductId.Of(new Guid("695a129d-7d98-4d65-b289-d9aedbefa0ba")), "Reno", 16000),
                Product.Create(ProductId.Of(new Guid("11de4ce0-db3b-4a8b-9f8e-2c95e96874f3")), "Lava", 17000),
                Product.Create(ProductId.Of(new Guid("d7dffa8c-ad21-4a23-a95a-378ed36e240b")), "Xiomi", 18000)
            };

        //Orders

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var shippingAddress = Address.Of("Jane", "Foster", "Jane.Foster@gmail.com", "Erie, TX", "US", "Texas", "12312");
                var billingAddress = Address.Of("Jane", "Foster", "Jane.Foster@gmail.com", "Erie, TX", "US", "Texas", "12312");
                var orderPayment1 = Payment.Of("Amex", "1234-5678-1234", "9/39", "121", "Card");
                var orderPayment2 = Payment.Of("ICICI", "5674-7895-2478", "11/29", "231", "Card");

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("aa483fc5-60c0-40cd-a12f-56ad268a0b5a")),
                    OrderName.Of("Order-1"),
                    shippingAddress,
                    billingAddress,
                    orderPayment1,
                    OrderStatus.Pending
                    );

                order1.Add(ProductId.Of(new Guid("e17ab324-f5d5-4cfc-95e8-ac708a3d01e5")), 3, 15000);
                order1.Add(ProductId.Of(new Guid("695a129d-7d98-4d65-b289-d9aedbefa0ba")), 5, 16000);

                var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("7ed9db57-8e8e-42e2-81ba-4ae9c1406814")),
                    OrderName.Of("Order-2"),
                    shippingAddress,
                    billingAddress,
                    orderPayment2,
                    OrderStatus.Pending
                    );

                order2.Add(ProductId.Of(new Guid("11de4ce0-db3b-4a8b-9f8e-2c95e96874f3")), 9, 17000);
                order2.Add(ProductId.Of(new Guid("d7dffa8c-ad21-4a23-a95a-378ed36e240b")), 14, 18000);

                return new List<Order> { order1, order2 };
            }

        }
    }
}
