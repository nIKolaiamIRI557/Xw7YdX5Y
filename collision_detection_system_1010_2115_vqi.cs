// 代码生成时间: 2025-10-10 21:15:29
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// 数据模型
public class Entity {
    public int Id { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}

// 数据上下文
public class CollisionDbContext : DbContext {
    public DbSet<Entity> Entities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) {
        options.UseSqlServer("YourConnectionString");
    }
}

// 碰撞检测系统
public class CollisionDetectionSystem {
    private readonly CollisionDbContext _context;

    public CollisionDetectionSystem(CollisionDbContext context) {
        _context = context;
    }

    // 检测两个实体是否碰撞
    public bool IsColliding(Entity entity1, Entity entity2) {
        try {
            // 计算两个实体之间的距离
            float distance = CalculateDistance(entity1, entity2);

            // 如果距离小于或等于某个阈值，则认为发生碰撞
            return distance <= 1.0f;
        } catch (Exception ex) {
            // 处理异常
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    // 计算两个实体之间的距离
    private float CalculateDistance(Entity entity1, Entity entity2) {
        // 计算距离公式：sqrt((x2-x1)^2 + (y2-y1)^2)
        return (float)Math.Sqrt(Math.Pow(entity2.X - entity1.X, 2) + Math.Pow(entity2.Y - entity1.Y, 2));
    }

    // 检测所有实体之间的碰撞
    public List<Entity> DetectCollisions() {
        var allEntities = _context.Entities.ToList();
        var collidingEntities = new List<Entity>();

        for (int i = 0; i < allEntities.Count; i++) {
            for (int j = i + 1; j < allEntities.Count; j++) {
                if (IsColliding(allEntities[i], allEntities[j])) {
                    collidingEntities.Add(allEntities[i]);
                    collidingEntities.Add(allEntities[j]);
                }
            }
        }

        return collidingEntities;
    }
}

// 程序主入口
class Program {
    static void Main(string[] args) {
        using (var context = new CollisionDbContext()) {
            // 初始化碰撞检测系统
            var collisionSystem = new CollisionDetectionSystem(context);

            // 添加一些测试实体
            var entity1 = new Entity { X = 0, Y = 0 };
            var entity2 = new Entity { X = 0.5f, Y = 0.5f };
            var entity3 = new Entity { X = 1.1f, Y = 1.1f };

            context.Entities.AddRange(entity1, entity2, entity3);
            context.SaveChanges();

            // 检测碰撞
            var collidingEntities = collisionSystem.DetectCollisions();

            Console.WriteLine("Colliding entities:");
a            foreach (var entity in collidingEntities) {
                Console.WriteLine($"Entity {entity.Id} at ({entity.X}, {entity.Y})");
            }
        }
    }
}