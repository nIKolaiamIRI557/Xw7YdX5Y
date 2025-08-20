// 代码生成时间: 2025-08-21 00:40:47
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDataTransformerApp
{
    // 定义一个用于JSON数据转换的类
    public class JsonDataTransformer
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public JsonDataTransformer()
        {
            // 初始化JSON序列化选项
            _serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        // 将JSON字符串转换为指定的对象类型
        public T ConvertFromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("The JSON string cannot be null or empty.", nameof(json));
            }

            try
            {
                return JsonSerializer.Deserialize<T>(json, _serializerOptions);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to deserialize the JSON string.", ex);
            }
        }

        // 将对象转换为JSON字符串
        public string ConvertToJson<T>(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "The data object cannot be null.");
            }

            try
            {
                return JsonSerializer.Serialize(data, _serializerOptions);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to serialize the data to JSON.", ex);
            }
        }
    }
}
