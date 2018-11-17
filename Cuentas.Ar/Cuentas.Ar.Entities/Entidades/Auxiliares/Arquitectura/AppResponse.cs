using System.Collections.Generic;

namespace Cuentas.Ar.Entities
{
    public class AppResponse<TData> : AppResponse where TData : new()
    {
        public AppResponse()
        {
            Data = new TData();
        }

        public TData Data { get; set; }
    }

    public class AppResponse
    {
        public AppResponse()
        {
            Success = true;
            IsValid = true;
        }

        public bool Success { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }
    }

    public class DataChart
    {
        public bool estado { get; set; }

        public List<string> data { get; set; }

        public List<string> labels { get; set; }

        public int Total { get; set; }
    }

    public class DataJson
    {
        public int value { get; set; }

        public string label { get; set; }
    }
}
