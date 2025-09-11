// 代码生成时间: 2025-09-12 02:21:42
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

// DatabaseConnectionPoolManager 类负责管理数据库连接池
public class DatabaseConnectionPoolManager
{
    private readonly ConcurrentBag<DbConnection> connectionPool;
    private readonly string connectionString;
    private readonly Type connectionType;
    private readonly int poolSize;
    private readonly object poolLock = new object();

    // 构造函数初始化连接池
    public DatabaseConnectionPoolManager(string connectionString, Type connectionType, int poolSize)
    {
        this.connectionString = connectionString;
        this.connectionType = connectionType;
        this.poolSize = poolSize;
        this.connectionPool = new ConcurrentBag<DbConnection>();
    }

    // 从连接池获取一个DbConnection对象
    public DbConnection GetConnection()
    {
        return connectionPool.TryTake(out DbConnection connection) ? connection : OpenNewConnection();
    }

    // 打开一个新的数据库连接并返回
    private DbConnection OpenNewConnection()
    {
        try
        {
            DbConnection newConnection = Activator.CreateInstance(connectionType) as DbConnection;
            if (newConnection != null)
            {
                newConnection.ConnectionString = connectionString;
                newConnection.Open();
                return newConnection;
            }
            throw new InvalidOperationException($"Could not create a new connection of type {connectionType.Name}.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error opening new database connection: {ex.Message}", ex);
        }
    }

    // 将DbConnection对象归还到连接池
    public void ReturnConnection(DbConnection connection)
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            connection.Close();
            connectionPool.Add(connection);
        }
        else if (connection != null)
        {
            // 如果连接关闭或异常，则释放资源
            connection.Dispose();
        }
    }

    // 释放连接池中的所有连接
    public void ClearPool()
    {
        while (connectionPool.TryTake(out DbConnection connection))
        {
            connection.Dispose();
        }
    }
}
