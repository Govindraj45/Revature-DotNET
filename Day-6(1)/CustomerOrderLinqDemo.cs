using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesDemo
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal OrderAmount { get; set; }
    }

    public class CustomerOrderLinqDemo
    {
        public void Run()
        {
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, CustomerName = "Ketan" },
                new Customer { CustomerId = 2, CustomerName = "Chetan" },
                new Customer { CustomerId = 3, CustomerName = "Charu" }
            };

            var orders = new List<Order>
            {
                new Order { OrderId = 101, CustomerId = 1, OrderAmount = 120.50m },
                new Order { OrderId = 102, CustomerId = 1, OrderAmount = 80.00m },
                new Order { OrderId = 103, CustomerId = 2, OrderAmount = 45.75m },
                new Order { OrderId = 104, CustomerId = 2, OrderAmount = 150.00m },
                new Order { OrderId = 105, CustomerId = 2, OrderAmount = 60.00m }
            };

            // Join customers with orders
            var customerOrders = customers
                .GroupJoin(
                    orders,
                    c => c.CustomerId,
                    o => o.CustomerId,
                    (c, os) => new
                    {
                        Customer = c,
                        Orders = os.ToList(),
                        OrderCount = os.Count(),
                        TotalValue = os.Sum(x => x.OrderAmount)
                    })
                .ToList();

            foreach (var item in customerOrders)
            {
                Console.WriteLine($"Customer: {item.Customer.CustomerName} (ID {item.Customer.CustomerId})");
                Console.WriteLine($"Orders placed: {item.OrderCount}, Total value: {item.TotalValue}");

                foreach (var o in item.Orders)
                {
                    Console.WriteLine($"  Order {o.OrderId} Amount {o.OrderAmount}");
                }

                Console.WriteLine();
            }
        }
    }
}
