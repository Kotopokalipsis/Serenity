using Application.Common.Responses;

namespace Application.Common.Services.ResponseHelper;

public static class ResponseHelper<T> where T : class
{
    public static ErrorResponse<T> GetRefreshTokenErrorResponse() => new()
    {
        StatusCode = 400,
        Errors = new Dictionary<string, List<string>>{{"ValidationError", new List<string> {"Refresh token not valid"}}},
    };
    
    public static ErrorResponse<T> GetAccessDeniedErrorResponse() => new()
    {
        StatusCode = 401,
        Errors = new Dictionary<string, List<string>>{{"LoginError", new List<string> {"Access denied"}}},
    };
    
    public static ErrorResponse<T> GetBadRequestErrorResponse() => new()
    {
        StatusCode = 400,
        Errors = new Dictionary<string, List<string>>{{"BadRequest", new List<string> {"Bad request"}}},
    };
    
    public static ErrorResponse<T> GetEmailIsAlreadyUsedErrorResponse() => new()
    {
        StatusCode = 400,
        Errors = new Dictionary<string, List<string>>{{"CreateError", new List<string> {"Email is already used"}}},
    };
}