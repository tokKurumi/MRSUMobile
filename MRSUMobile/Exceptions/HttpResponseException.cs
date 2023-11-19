namespace MRSUMobile.Exceptions
{
    [Serializable]
    public class HttpResponseException : Exception
    {
        public HttpResponseException()
        {
        }

        public HttpResponseException(HttpResponseMessage tokenResponse)
        {
            Response = tokenResponse;
        }

        public HttpResponseException(string message)
            : base(message)
        {
        }

        public HttpResponseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public HttpResponseMessage Response { get; private set; }

        public override string Message => $"Status code: {Response.StatusCode}";
    }
}