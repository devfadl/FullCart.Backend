using FullCart.Application.Common.Dto;

using System.Net;

namespace FullCart.Application.Common.Shared;

public class Result<T>
{

    internal Result(T data, IEnumerable<string> errors, HttpStatusCode StatusCode)
    {
        Data = data;
        Succeeded = (StatusCode == HttpStatusCode.OK);
        Errors = errors.ToArray();
        HttpStatusCode = StatusCode;
    }

    public T Data { get; set; }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public static Result<T> Success(T data)
    {
        return new Result<T>(data, Array.Empty<string>(), HttpStatusCode.OK);
    }
    public static Result<T> Unauthorized(string msg)
    {
        return new Result<T>(default(T), new string[] { msg }, HttpStatusCode.Unauthorized);
    }
    public static Result<T> NotFound(string msg)
    {
        return new Result<T>(default(T), new string[] { msg }, HttpStatusCode.NotFound);
    }

    public static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(default(T), errors, HttpStatusCode.BadRequest);
    }

    internal static Result<UserBriefDto> Success(UserBriefDto group)
    {
        throw new NotImplementedException();
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(default(T), new string[] { error }, HttpStatusCode.BadRequest);
    }
}
