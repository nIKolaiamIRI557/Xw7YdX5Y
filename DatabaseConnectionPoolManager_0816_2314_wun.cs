// 代码生成时间: 2025-08-16 23:14:35
using System;
using System.Data;
using System.Data.Common;

namespace DatabaseConnectionPoolManagement
{
    /// <summary>
    /// Manages a pool of database connections.
    /// </summary>
    public class DatabaseConnectionPoolManager
    {
        private readonly string _connectionString;
        private readonly int _maxPoolSize;
        private int _activeConnections;

        /// <summary>
        /// Initializes a new instance of the DatabaseConnectionPoolManager class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="maxPoolSize">The maximum number of connections in the pool.</param>
        public DatabaseConnectionPoolManager(string connectionString, int maxPoolSize)
        {
            _connectionString = connectionString;
            _maxPoolSize = maxPoolSize;
            _activeConnections = 0;
        }

        /// <summary>
        /// Gets a database connection from the pool.
        /// </summary>
        /// <returns>A database connection object.</returns>
        public DbConnection GetConnection()
        {
            if (_activeConnections >= _maxPoolSize)
            {
                throw new InvalidOperationException("Connection pool has reached its maximum size.");
            }

            var connection = DbProviderFactories.GetFactoryClasses()
                .Select(factory => factory.CreateConnection())
                .FirstOrDefault(conn => conn.ConnectionString == _connectionString)?? new SqlConnection(_connectionString);

            connection.Open();
            _activeConnections++;
            return connection;
        }

        /// <summary>
        /// Returns a database connection back to the pool.
        /// </summary>
        /// <param name="connection">The database connection to return.</param>
        public void ReturnConnection(DbConnection connection)
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                throw new ArgumentException("Connection must be open to return it to the pool.");
            }

            connection.Close();
            _activeConnections--;
        }
    }
}
