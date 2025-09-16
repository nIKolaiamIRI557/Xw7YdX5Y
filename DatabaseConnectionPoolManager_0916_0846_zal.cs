// 代码生成时间: 2025-09-16 08:46:40
using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.Linq;

// DatabaseConnectionPoolManager.cs
// 程序用于管理数据库连接池
public class DatabaseConnectionPoolManager
{
    private readonly ConcurrentQueue<DbConnection> connectionPool = new ConcurrentQueue<DbConnection>();
    private readonly string connectionString;

    // 构造函数初始化数据库连接字符串
    public DatabaseConnectionPoolManager(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // 获取数据库连接
    public DbConnection GetConnection()
    {
        if (connectionPool.TryDequeue(out DbConnection connection))
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }
        else
        {
            // 连接池为空时创建新连接
            return CreateNewConnection();
        }
    }

    // 释放数据库连接
    public void ReleaseConnection(DbConnection connection)
    {
        connection.Close();
        connectionPool.Enqueue(connection);
    }

    // 创建新的数据库连接
    private DbConnection CreateNewConnection()
    {
        var factory = DbProviderFactories.GetFactoryClasses().FirstOrDefault(f => f.InvariantName == "System.Data.SqlClient");
        if (factory == null)
        {
            throw new InvalidOperationException("No suitable factory found.");
        }
        return factory.CreateConnection();
    }

    // 初始化连接池
    public void InitializePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var connection = CreateNewConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            connectionPool.Enqueue(connection);
        }
    }
}
