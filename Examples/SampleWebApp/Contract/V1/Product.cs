namespace SampleWebApp.Contract.V1
{
    public class Product
    {
        public int Id { get; internal set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PagingParameters
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; } = 20;
    }

    public class ProductCreate
    {
        public string Name { get; set; } = string.Empty;
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
