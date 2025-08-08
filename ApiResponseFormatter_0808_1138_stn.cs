// 代码生成时间: 2025-08-08 11:38:55
 * This class is designed to handle the common task of formatting responses for API endpoints.
 * It provides a way to encapsulate the format of the response, making it easier to maintain
 * and extend in the future.
 */

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YourNamespace
{
    /// <summary>
    /// Utility class for formatting API responses.
    /// </summary>
    public class ApiResponseFormatter
    {
        /// <summary>
        /// Formats a successful API response.
        /// </summary>
        /// <param name="data">The data to include in the response.</param>
        /// <returns>A formatted API response.</returns>
        public static Dictionary<string, object> Success(object data)
        {
            return new Dictionary<string, object>
            {
                {"status", "success"},
                {"data", data}
            };
        }

        /// <summary>
        /// Formats an API response with an error.
        /// </summary>
        /// <param name="message">The error message to include in the response.</param>
        /// <param name="code">The error code.</param>
        /// <returns>A formatted API response with an error.</returns>
        public static Dictionary<string, object> Error(string message, int code = 500)
        {
            return new Dictionary<string, object>
            {
                {"status", "error"},
                {"message", message},
                {"code", code}
            };
        }

        /// <summary>
        /// Formats an API response with pagination information.
        /// </summary>
        /// <param name="data">The paginated data to include in the response.</param>
        /// <param name="totalCount">The total count of items.</param>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <returns>A formatted API response with pagination.</returns>
        public static Dictionary<string, object> Paginated(object data, int totalCount, int currentPage, int pageSize)
        {
            return new Dictionary<string, object>
            {
                {"status", "success"},
                {"data", data},
                {"pagination", new Dictionary<string, int>
                {
                    {"totalCount", totalCount},
                    {"currentPage", currentPage},
                    {"pageSize", pageSize}
                }}
            };
        }
    }
}
