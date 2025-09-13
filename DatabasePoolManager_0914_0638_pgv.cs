// 代码生成时间: 2025-09-14 06:38:48
using System;
using System.Data.Common;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

// DatabasePoolManager.cs 文件实现了一个简单的数据库连接池管理器
public class DatabasePoolManager
{
    private ConcurrentBag<DbConnection> _connectionPool;
    private readonly string _connectionString;
    private readonly int _maxConnections;
    private readonly Type _dbContextType;

    // 构造函数
    public DatabasePoolManager(string connectionString, int maxConnections, Type dbContextType)
    {
        _connectionString = connectionString;
        _maxConnections = maxConnections;
        _dbContextType = dbContextType;
        _connectionPool = new ConcurrentBag<DbConnection>();
    }

    // 从连接池获取一个可用的数据库连接
    public DbConnection GetConnection()
    {
        if (_connectionPool.IsEmpty)
        {
            // 如果连接池为空，则创建新的连接
            DbConnection connection = CreateNewConnection();
            _connectionPool.Add(connection);
            return connection;
        }
        else
        {
            // 从连接池中获取一个已存在的连接
            return _connectionPool.First();
        }
    }

    // 释放连接回连接池
    public void ReleaseConnection(DbConnection connection)
    {
        if (connection != null)
        {
            // 检查连接是否已关闭，如果未关闭，则关闭它
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            _connectionPool.Add(connection);
        }
    }

    // 创建新的数据库连接
    private DbConnection CreateNewConnection()
    {
        try
        {
            var factory = DbProviderFactories.GetFactory(_dbContextType);
            return factory.CreateConnection();
        }
        catch (Exception ex)
        {
            // 错误处理：创建连接失败时，抛出异常
            throw new InvalidOperationException("Failed to create a new database connection.", ex);
        }
    }

    // 清理连接池中所有连接
    public void ClearPool()
    {
        foreach (var connection in _connectionPool)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        _connectionPool.Clear();
    }
}
