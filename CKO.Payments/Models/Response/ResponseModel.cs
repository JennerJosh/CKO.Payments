using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace CKO.Payments.Models.Response
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorMessages ErrorDetails { get; set; }
        public object Model { get; set; }

        public static ResponseModel GetBaseResponse(int statusCode, bool isSuccess)
        {
            return new ResponseModel
            {
                StatusCode = statusCode,
                IsSuccess = isSuccess,
                Model = null,
                ErrorDetails = null,
            };
        }

        public static ResponseModel GetSuccessResponse(object model)
        {
            var rm = GetBaseResponse(StatusCodes.Status200OK, true);
            rm.Model = model;

            return rm;
        }
        public static ResponseModel GetErrorResponse(int statusCode, string[] errorMessages)
        {
            var rm = GetBaseResponse(statusCode, false);
            rm.ErrorDetails = new ErrorMessages()
            {
                Message = errorMessages
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToArray()
            };

            return rm;
        }

        public static ResponseModel GetErrorResponse(int statusCode, string errorMessage)
        {
            var errorMessages = new[] { errorMessage };
            return GetErrorResponse(statusCode, errorMessages);
        }

        public static ResponseModel GetErrorResponse(Exception exception)
        {
            var errorMessages = new[] { exception.Message, exception.InnerException?.Message ?? String.Empty };
            return GetErrorResponse(StatusCodes.Status500InternalServerError, errorMessages);
        }

        public class ErrorMessages
        {
            public string[] Message { get; set; }
        }

    }
}
