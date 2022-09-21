namespace RespayMLS.Extension
{
    public class ApiResponse
    {
        public string Code { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

    }
}
