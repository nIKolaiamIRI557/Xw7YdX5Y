// 代码生成时间: 2025-10-10 02:09:44
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections.Concurrent;
using System.Threading.Tasks;

// DatabaseConnectionPoolManager is responsible for managing a pool of database connections.
public class DatabaseConnectionPoolManager
{
    private readonly ConcurrentBag<DbConnection> _connectionPool;
    private readonly string _connectionString;
    private readonly int _maxPoolSize;
    private readonly Type _dbContextType;

    // Constructor to initialize the connection pool manager with the connection string and maximum pool size.
    public DatabaseConnectionPoolManager(string connectionString, int maxPoolSize, Type dbContextType)
    {
        _connectionString = connectionString;
        _maxPoolSize = maxPoolSize;
        _dbContextType = dbContextType;
        _connectionPool = new ConcurrentBag<DbConnection>();
    }

    // Method to get a connection from the pool or create a new one if the pool is empty or has reached its maximum size.
    public async Task<DbConnection> GetConnectionAsync()
    {
        DbConnection connection;
        if (!_connectionPool.TryTake(out connection) || connection.State != System.Data.ConnectionState.Open)
        {
            // If the pool is empty or the connection is closed, create a new connection.
            connection = DbProviderFactories.GetFactory(_dbContextType).CreateConnection();
            connection.ConnectionString = _connectionString;
            await connection.OpenAsync();
        }
        return connection;
    }

    // Method to return a connection to the pool.
    public void ReturnConnection(DbConnection connection)
    {
        if (connection.State == System.Data.ConnectionState.Open && _connectionPool.Count < _maxPoolSize)
        {
            _connectionPool.Add(connection);
        }
        else
        {
            connection.Dispose();
        }
    }

    // Method to dispose of all connections in the pool when the manager is disposed.
    public void Dispose()
    {
        foreach (var connection in _connectionPool)
        {
            connection.Dispose();
        }
        _connectionPool.Clear();
    }
}

// Usage example:
/*
var connectionString = "YourConnectionStringHere";
var maxPoolSize = 10;
var dbContextType = typeof(YourDbContext);
var manager = new DatabaseConnectionPoolManager(connectionString, maxPoolSize, dbContextType);

try
{
    using (var connection = await manager.GetConnectionAsync())
    {
        // Use the connection to perform database operations.
    }
}
catch (Exception ex)
{
    // Handle any exceptions that occur.
}
finally
{
    manager.Dispose();
}
*/