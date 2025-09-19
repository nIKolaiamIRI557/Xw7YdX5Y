// 代码生成时间: 2025-09-19 09:12:25
using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// ApiResponseFormatter.cs
// 一个用于格式化API响应的工具类
public class ApiResponseFormatter : ActionFilterAttribute
{
    // 在Action执行后调用
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        // 检查是否有异常发生
        if (context.Exception != null)
        {
            // 格式化异常信息
            var errorResponse = new
            {
                success = false,
                message = "An error occurred",
                data = context.Exception.Message
            };
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
        else if (!context.HttpContext.Response.Headers.ContainsKey("Content-Type"))
        {
            // 格式化成功的响应
            var resultData = context.Result as ObjectResult;
            if (resultData != null)
            {
                var responseData = new
                {
                    success = true,
                    data = resultData.Value
                };
                context.Result = new OkObjectResult(responseData);
            }
        }
    }
}

// 使用示例
// 在Controller中使用ApiResponseFormatter属性来格式化响应
public class SampleController : ControllerBase
{
    [ApiResponseFormatter]
    public IActionResult GetSampleData()
    {
        // 假设这里是一些业务逻辑操作
        return Ok(new { data = "Sample Data" });
    }
}