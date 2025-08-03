using AuthService.Application.DTOs;

namespace AuthService.Application.Helpers
{
    public static class ResponseBuilder
    {
        public static ResponseDto<T> Success<T>(T data, string message = "Success", int status = 200)
        {
            return new ResponseDto<T>(status, true, message, data, null);
        }

        public static ResponseDto<T> Fail<T>(string message, object errors = null, int status = 400)
        {
            return new ResponseDto<T>(status, false, message, default, errors);
        }
    }
}
