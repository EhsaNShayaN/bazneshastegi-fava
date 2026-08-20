namespace Bazneshastegi.Application.Services.Provider;

public class BaseResult<T>
{
    public string Message { get; set; } = string.Empty;
    public T[] ItemList { get; set; } = [];
    public string Id { get; set; } = string.Empty;
    public bool Status { get; set; }
    public object? NoneObject { get; set; }
    public string Error { get; set; } = string.Empty;
    public string InnerException { get; set; } = string.Empty;

    public string GetErrorMessage()
    {
        var result = this.Message;
        if (!string.IsNullOrWhiteSpace(result)) return result;

        result = this.Error;
        if (!string.IsNullOrWhiteSpace(result)) return result;

        return result;
    }
}