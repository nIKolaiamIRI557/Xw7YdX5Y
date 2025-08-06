// 代码生成时间: 2025-08-07 02:21:54
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace SecureDatabaseAccess
{
    // 数据访问类
    public class SecureDatabaseAccess
    {
        private readonly DbContext _context;

        public SecureDatabaseAccess(DbContext context)
        {
            _context = context;
        }

        // 使用参数化查询防止SQL注入
        public IQueryable<T> QueryWithParameter<T>(string query, params object[] parameters)
        {
            // 检查query是否是有效的SQL查询语句
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be null or whitespace.", nameof(query));
            }

            // 使用DbRawSqlQuery<T>来执行参数化查询，防止SQL注入
            return _context.Database.SqlQuery<T>(query, parameters);
        }

        // 插入数据，使用Entity Framework的ChangeTracker防止SQL注入
        public void Insert<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        // 更新数据，使用Entity Framework的ChangeTracker防止SQL注入
        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // 删除数据，使用Entity Framework的ChangeTracker防止SQL注入
        public void Delete<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
