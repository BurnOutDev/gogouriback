using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class JsonResponse<T> : JsonResponse
    {
        public JsonResponse(T data)
        {
            Data = data;
        }

        public JsonResponse(bool success = true)
        {
            Success = success;
        }

        public T? Data { get; set; }
    }

    public class JsonResponse
    {
        public static JsonResponse Error(string message = "")
        {
            return new()
            {
                Message = message,
                Success = false
            };
        }

        public static JsonResponse IsSuccess() => new();

        public bool? Success { get; set; } = true;
        public string? Message { get; set; }
    }
}
