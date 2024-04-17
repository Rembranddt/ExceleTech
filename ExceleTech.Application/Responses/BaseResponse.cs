using System.Net;

namespace ExceleTech.Application.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK; 
        public bool IsSuccess => Errors.Count == 0;
        public List<string> Errors { get; private set; }

        public BaseResponse() 
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        { 
            Errors.Add(error); 
        }
        
    }

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse() : base() { }

        public string Name = typeof(T).ToString();
        public T Data { get; private set; }
        
        public BaseResponse(T data) : base() 
        {
            Data = data;
        }
        
        public void AddData(T data)
        {
            Data = data;
        }
    }
}