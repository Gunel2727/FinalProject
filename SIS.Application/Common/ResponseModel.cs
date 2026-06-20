using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application.Common
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ResponseModel<T> Ok(T data) => new()
        {
            Success = true,
            StatusCode = 200,
            Data = data
        };

        public static ResponseModel<T> Created(T data) => new()
        {
            Success = true,
            StatusCode = 201,
            Data = data
        };

        public static ResponseModel<T> Fail(int statusCode, string error) => new()
        {
            Success = false,
            StatusCode = statusCode,
            Errors = new List<string> { error }
        };

        public static ResponseModel<T> Fail(int statusCode, List<string> errors) => new()
        {
            Success = false,
            StatusCode = statusCode,
            Errors = errors
        };
    }
}
