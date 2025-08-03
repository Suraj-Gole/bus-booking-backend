namespace AuthService.Application.DTOs
{
    public class ResponseDto<T>
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public object Errors { get; set; }

        public ResponseDto(int status, bool success, string message, T data, object errors)
        {
            Status = status;
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }
    }
}
