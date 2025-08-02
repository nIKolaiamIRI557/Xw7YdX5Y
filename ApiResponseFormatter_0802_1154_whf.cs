// 代码生成时间: 2025-08-02 11:54:42
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// ApiResponseFormatter.cs
// 该类提供了API响应格式化的工具方法。
namespace YourNamespace
{
    public class ApiResponseFormatter
    {
        private readonly DbContext _dbContext;

        // 构造函数
        public ApiResponseFormatter(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // 格式化API响应
        public string FormatResponse<T>(T data) where T : class
        {
            try
            {
                // 将数据转换为JSON字符串
                var serializedData = System.Text.Json.JsonSerializer.Serialize(data);

                // 构建格式化的API响应
                return ${"{{"success": true, "data": {serializedData}}}}";
            }
            catch (Exception ex)
            {
                // 错误处理
                return ${"{{"success": false, "message": "{ex.Message}"}}}";
            }
        }

        // 格式化API错误响应
        public string FormatErrorResponse(Exception ex)
        {
            // 构建格式化的错误响应
            return ${"{{"success": false, "message": "{ex.Message}"}}}";
        }
    }
}
