// 代码生成时间: 2025-09-09 22:42:16
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// 实体类：购物车项
public class CartItem
{
    public int CartItemId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    // 关联属性：产品
    public virtual Product Product { get; set; }
}

// 实体类：产品
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    // 关联属性：购物车项
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}

// 数据库上下文
public class ShoppingCartContext : DbContext
{
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Product> Products { get; set; }

    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId);
    }
}

// 服务类：购物车服务
public class ShoppingCartService
{
    private readonly ShoppingCartContext _context;

    public ShoppingCartService(ShoppingCartContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // 添加商品到购物车
    public async Task AddItemToCartAsync(int productId, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0.");

        var product = await _context.Products.FindAsync(productId);
        if (product == null) throw new InvalidOperationException("Product not found.");

        var cartItem = new CartItem { ProductId = productId, Quantity = quantity };

        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
    }

    // 从购物车中移除商品
    public async Task RemoveItemFromCartAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem == null) throw new InvalidOperationException("CartItem not found.");

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
    }

    // 获取购物车中的商品列表
    public async Task<List<CartItem>> GetCartItemsAsync()
    {
        return await _context.CartItems.Include(ci => ci.Product).ToListAsync();
    }
}
