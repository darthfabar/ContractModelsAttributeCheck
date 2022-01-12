using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SampleWebApp.Contract.V2
{
    public class ProductV2
    {
        [JsonPropertyName("id")]
        public int Id { get; internal set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class PagingParametersV2
    {
        [JsonPropertyName("pageNo")]
        public int PageNo { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 20;
    }

    public class ProductCreateV2
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class ErrorResponseV2
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }


    public class DummyDict : Dictionary<string, string>    { }
}
