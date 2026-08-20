using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Infrastructure.Helpers;

internal static class HttpProxyHelper
{
    readonly static JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    };

    const string HttpProxyErrorCode = "Proxy.Error";

    public static PrimitiveResult<HttpRequestMessage> CreateHttpRequestMessage<T>(T req, string endpoint, HttpMethod httpMethod)
    {
        var result = new HttpRequestMessage(httpMethod, endpoint);

        if (httpMethod.Equals(HttpMethod.Post))
        {
            result.Content = new StringContent(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
        }

        return result;
    }
    public static async ValueTask<PrimitiveResult<TResponse>> SendRequest<TResponse>(
        HttpClient client,
        HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken)
    {
        try
        {
            var httpResponse = await client.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseContent ?? string.Empty))
            {
                return PrimitiveResult.Failure<TResponse>(HttpProxyErrorCode, "Invalid response");
            }
            var response = JsonSerializer.Deserialize<TResponse>(responseContent!, DefaultJsonSerializerOptions);
            if (response is null)
            {
                return PrimitiveResult.Failure<TResponse>(HttpProxyErrorCode, "Api null response");
            }
            if (!httpResponse.IsSuccessStatusCode)
            {
                return PrimitiveResult.Failure<TResponse>("", httpResponse.StatusCode.ToString());
            }
            return PrimitiveResult.Success(response!);
        }
        catch (Exception ex)
        {
            return PrimitiveResult.Failure<TResponse>(HttpProxyErrorCode, ex.Message);
        }
    }

    public static async ValueTask<PrimitiveResult<BaseResult<TResponse>>> SendBazneshastegiRequest<TResponse>(
        HttpClient client,
        HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken)
    {
        try
        {
            var httpResponse = await client.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseContent ?? string.Empty))
            {
                return PrimitiveResult.Failure<BaseResult<TResponse>>(HttpProxyErrorCode, "Invalid response");
            }
            var response = JsonSerializer.Deserialize<BaseResult<TResponse>>(responseContent!, DefaultJsonSerializerOptions);
            if (response is null)
            {
                return PrimitiveResult.Failure<BaseResult<TResponse>>(HttpProxyErrorCode, "Api null response");
            }
            var errorMessage = response?.GetErrorMessage();
            if (string.IsNullOrWhiteSpace(errorMessage))
                errorMessage = httpResponse.StatusCode.ToString();

            if (!httpResponse.IsSuccessStatusCode)
            {
                return PrimitiveResult.Failure<BaseResult<TResponse>>(
                    $"{HttpProxyErrorCode}.{httpResponse.StatusCode.ToString()}",
                    errorMessage);
            }
            if (!response!.Status)
            {
                return PrimitiveResult.Failure<BaseResult<TResponse>>(HttpProxyErrorCode, errorMessage);
            }
            return PrimitiveResult.Success(response!);
        }
        catch (Exception ex)
        {
            return PrimitiveResult.Failure<BaseResult<TResponse>>(HttpProxyErrorCode, ex.Message);
        }
    }


    public static ValueTask<PrimitiveResult<TResponse>> SendRequest<TRequest, TResponse>(
        Func<ValueTask<PrimitiveResult<HttpClient>>> clientBuilder,
        string endpoint,
        TRequest request,
        HttpMethod httpMethod,
        CancellationToken cancellationToken) =>
            clientBuilder.Invoke()
            .Map(client => CreateHttpRequestMessage(request, endpoint, httpMethod).Map(m => (Client: client, Mesage: m)))
            .Map(a => SendRequest<TResponse>(a.Client, a.Mesage, cancellationToken));

    public static ValueTask<PrimitiveResult<BaseResult<TResponse>>> SendBazneshastegiRequest<TRequest, TResponse>(
        Func<ValueTask<PrimitiveResult<HttpClient>>> clientBuilder,
        string endpoint,
        TRequest request,
        HttpMethod httpMethod,
        CancellationToken cancellationToken) =>
            clientBuilder.Invoke()
            .Map(client => CreateHttpRequestMessage(request, endpoint, httpMethod).Map(m => (Client: client, Mesage: m)))
            .Map(a => SendBazneshastegiRequest<TResponse>(a.Client, a.Mesage, cancellationToken));

    public static ValueTask<PrimitiveResult<TResponse>> PostRequest<TRequest, TResponse>(
        Func<ValueTask<PrimitiveResult<HttpClient>>> clientBuilder,
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken) =>
            SendRequest<TRequest, TResponse>(clientBuilder, endpoint, request, HttpMethod.Post, cancellationToken);

    public static ValueTask<PrimitiveResult<TResponse>> GetRequest<TRequest, TResponse>(
        Func<ValueTask<PrimitiveResult<HttpClient>>> clientBuilder,
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken) =>
            SendRequest<TRequest, TResponse>(clientBuilder, endpoint, request, HttpMethod.Get, cancellationToken);

    public static ValueTask<PrimitiveResult<BaseResult<TResponse>>> PostBazneshastegiRequest<TRequest, TResponse>(
        Func<ValueTask<PrimitiveResult<HttpClient>>> clientBuilder,
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken) =>
            SendBazneshastegiRequest<TRequest, TResponse>(clientBuilder, endpoint, request, HttpMethod.Post, cancellationToken);

    public static ValueTask<PrimitiveResult<BaseResult<TResponse>>> GetBazneshastegiRequest<TRequest, TResponse>(
        Func<ValueTask<PrimitiveResult<HttpClient>>> clientBuilder,
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken) =>
            SendBazneshastegiRequest<TRequest, TResponse>(clientBuilder, endpoint, request, HttpMethod.Get, cancellationToken);
}
