// 代码生成时间: 2025-08-04 07:47:22
using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

// 定义实体
public class Order {
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

public class OrderItem {
    public int OrderItemId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}

// 订单上下文
public class OrderContext : DbContext {
    public DbSet<Order> Orders { get; set; }
}

// 订单处理服务
public class OrderProcessingService {
    private readonly OrderContext _context;

    public OrderProcessingService(OrderContext context) {
        _context = context;
    }

    public async Task CreateOrderAsync(Order order) {
        try {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        } catch (Exception ex) {
            // 错误处理
            Console.WriteLine("Error creating order: " + ex.Message);
        }
    }

    public async Task<Order> GetOrderAsync(int orderId) {
        try {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        } catch (Exception ex) {
            // 错误处理
            Console.WriteLine("Error retrieving order: " + ex.Message);
            return null;
        }
    }

    public async Task UpdateOrderAsync(Order order) {
        try {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        } catch (Exception ex) {
            // 错误处理
            Console.WriteLine("Error updating order: " + ex.Message);
        }
    }

    public async Task DeleteOrderAsync(int orderId) {
        try {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order != null) {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            } else {
                Console.WriteLine("Order not found");
            }
        } catch (Exception ex) {
            // 错误处理
            Console.WriteLine("Error deleting order: " + ex.Message);
        }
    }
}

class Program {
    static async Task Main(string[] args) {
        using (var context = new OrderContext()) {
            var service = new OrderProcessingService(context);

            // 创建订单
            var newOrder = new Order { CustomerName = "John Doe" };
            newOrder.OrderItems.Add(new OrderItem { ProductName = "Product A", Quantity = 1, Price = 100 });
            newOrder.OrderItems.Add(new OrderItem { ProductName = "Product B", Quantity = 2, Price = 150 });

            await service.CreateOrderAsync(newOrder);

            // 获取订单
            var order = await service.GetOrderAsync(newOrder.OrderId);
            Console.WriteLine($"Order for {order.CustomerName} with items: {string.Join(", ", order.OrderItems.Select(item => item.ProductName))}");

            // 更新订单
            order.OrderItems[0].Quantity = 3;
            await service.UpdateOrderAsync(order);

            // 删除订单
            await service.DeleteOrderAsync(newOrder.OrderId);
        }
    }
}