namespace Application.DTOs.Stock
{
    public class StockOperationResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<StockMissingItemDTO> MissingItems { get; set; } = new();

        public static StockOperationResultDTO Ok(string message) => new()
        {
            Success = true,
            Message = message
        };

        public static StockOperationResultDTO Fail(string message, List<StockMissingItemDTO>? missingItems = null) => new()
        {
            Success = false,
            Message = message,
            MissingItems = missingItems ?? new List<StockMissingItemDTO>()
        };
    }
}
