namespace Application.DTOs
{
    public class ErrorResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
