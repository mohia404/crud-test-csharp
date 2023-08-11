using System.Text;
using System.Text.Json;
using Blazored.Toast.Services;
using Mc2.CrudTest.Presentation.Shared.Http;

namespace Mc2.CrudTest.Presentation.Client.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly IToastService _toastService;

    public HttpService(IHttpClientFactory httpClientFactory, IToastService toastService)
    {
        _toastService = toastService;
        _httpClient = httpClientFactory.CreateClient("backend");
        _apiUrl = _httpClient.BaseAddress + "api/";
    }

    public async Task<T?> HttpGetAsync<T>(string uri)
        where T : class
    {
        HttpResponseMessage result = await _httpClient.GetAsync($"{_apiUrl}{uri}");
        if (result.IsSuccessStatusCode) return await FromHttpResponseMessageAsync<T>(result);
        await CatchProblem(result);
        return null;
    }

    public async Task<string?> HttpGetAsync(string uri)
    {
        HttpResponseMessage result = await _httpClient.GetAsync($"{_apiUrl}{uri}");
        if (result.IsSuccessStatusCode) return await result.Content.ReadAsStringAsync();
        await CatchProblem(result);
        return null;
    }

    public async Task<T?> HttpDeleteAsync<T>(string uri, object id)
        where T : class
    {
        HttpResponseMessage result = await _httpClient.DeleteAsync($"{_apiUrl}{uri}/{id}");
        if (result.IsSuccessStatusCode) return await FromHttpResponseMessageAsync<T>(result);
        await CatchProblem(result);
        return null;
    }

    public async Task<bool> HttpDeleteAsync(string uri, object id)
    {
        HttpResponseMessage result = await _httpClient.DeleteAsync($"{_apiUrl}{uri}/{id}");
        if (!result.IsSuccessStatusCode)
        {
            await CatchProblem(result);
            return false;
        }

        _toastService.ShowSuccess("The delete operation completed successfully.");

        return true;
    }

    public async Task<T?> HttpPostAsync<T>(string uri, object dataToSend)
        where T : class
    {
        StringContent content = ToJson(dataToSend);

        HttpResponseMessage result = await _httpClient.PostAsync($"{_apiUrl}{uri}", content);
        if (!result.IsSuccessStatusCode)
        {
            await CatchProblem(result);
            return null;
        }

        T? model = await FromHttpResponseMessageAsync<T>(result);

        _toastService.ShowSuccess("The operation completed successfully.");

        return model;
    }

    public async Task<bool> HttpPostAsync(string uri, object dataToSend)
    {
        StringContent content = ToJson(dataToSend);

        HttpResponseMessage result = await _httpClient.PostAsync($"{_apiUrl}{uri}", content);
        if (!result.IsSuccessStatusCode)
        {
            await CatchProblem(result);
            return false;
        }

        _toastService.ShowSuccess("The operation completed successfully.");

        return true;
    }

    public async Task<T?> HttpPutAsync<T>(string uri, object dataToSend)
        where T : class
    {
        StringContent content = ToJson(dataToSend);

        HttpResponseMessage result = await _httpClient.PutAsync($"{_apiUrl}{uri}", content);
        if (!result.IsSuccessStatusCode)
        {
            await CatchProblem(result);
            return null;
        }

        T? model = await FromHttpResponseMessageAsync<T>(result);

        _toastService.ShowSuccess("The operation completed successfully.");

        return model;
    }

    public async Task<bool> HttpPutAsync(string uri, object dataToSend)
    {
        StringContent content = ToJson(dataToSend);

        HttpResponseMessage result = await _httpClient.PutAsync($"{_apiUrl}{uri}", content);
        if (!result.IsSuccessStatusCode)
        {
            await CatchProblem(result);
            return false;
        }

        _toastService.ShowSuccess("The update operation completed successfully.");

        return true;
    }


    private static StringContent ToJson(object obj)
    {
        return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
    }

    private static async Task<T?> FromHttpResponseMessageAsync<T>(HttpResponseMessage result)
    {
        return JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    private async Task CatchProblem(HttpResponseMessage result)
    {
        string? mediaType = result.Content.Headers.ContentType?.MediaType;
        if (mediaType != null && mediaType.Equals("application/problem+json", StringComparison.InvariantCultureIgnoreCase))
        {
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string output = await result.Content.ReadAsStringAsync();

            if (output.Contains("https://tools.ietf.org/html/rfc7231#section-6.5.1"))
            {
                ValidationProblemDetailsWithErrors? validationError = JsonSerializer.Deserialize<ValidationProblemDetailsWithErrors>(output, options);

                if (validationError is null)
                    return;

                foreach (KeyValuePair<string, string[]> problem in validationError.Errors)
                {
                    _toastService.ShowError(string.Join(" | ", problem.Value));
                }
                return;
            }

            ProblemDetailsWithErrors? error = JsonSerializer.Deserialize<ProblemDetailsWithErrors>(output, options);

            if(error is null)
                return;

            _toastService.ShowError(error.Title);
        }
    }
}