using System.Net;

namespace BookingRoom.Application.Features.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public int StatusCode { get; }
        public string Error { get; }
        public T Value { get; }

        private Result(bool isSuccess, string error, T value, HttpStatusCode statusCode)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
            StatusCode = (int)statusCode;
        }

        public static Result<T> Success(T value) => new(true, null, value, HttpStatusCode.OK);
        public static Result<T> Failure(HttpStatusCode statusCode, string error) => new(false, error, default, statusCode);
    }
}
