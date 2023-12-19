namespace CaliberFS.Template.Application.Common.Response
{
    public class ApiResponsePagination<T>(IList<T> data, int total)
    {
        public IList<T> Data { get; set; } = data;
        public int Total { get; set; } = total;
    }
}
