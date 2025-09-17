// 代码生成时间: 2025-09-17 10:38:07
 * The program includes error handling and follows C# best practices for maintainability and scalability.
 */
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonDataTransformerApp
{
    public class JsonDataTransformer
    {
        /// <summary>
        /// Convert JSON string data to object
        /// </summary>
        /// <typeparam name="T">The type of the object to convert to</typeparam>
        /// <param name="jsonData">The JSON data as a string</param>
        /// <returns>The converted object of type T</returns>
        public T ConvertJsonToObject<T>(string jsonData)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON deserialization errors
                Console.WriteLine($"Error deserializing JSON: {jsonEx.Message}");
                throw;
            }
        }

        /// <summary>
        /// Convert object to JSON string data
        /// </summary>
        /// <param name="data">The object to convert</param>
        /// <returns>The JSON representation of the object as a string</returns>
        public string ConvertObjectToJson(object data)
        {
            try
            {
                return JsonConvert.SerializeObject(data, Formatting.Indented);
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON serialization errors
                Console.WriteLine($"Error serializing object to JSON: {jsonEx.Message}");
                throw;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example usage of the JsonDataTransformer
            var transformer = new JsonDataTransformer();
            var exampleJson = "{"name":"John", "age":30}";
            try
            {
                // Convert JSON to object
                var person = transformer.ConvertJsonToObject<Person>(exampleJson);
                Console.WriteLine($"Person Name: {person.Name}, Age: {person.Age}");

                // Convert object back to JSON
                var jsonOutput = transformer.ConvertObjectToJson(person);
                Console.WriteLine("JSON Output:
" + jsonOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    // Example class to demonstrate the conversion
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
