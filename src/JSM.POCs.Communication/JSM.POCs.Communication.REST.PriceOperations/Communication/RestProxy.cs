using CSharpFunctionalExtensions;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace JSM.POCs.Communication.REST.PriceOperations.Communication
{
    public record ServerSuccess();
    public record ServerError(int StatusCode, string Message);

    public class RestProxy
    {
        private const string ContentType = "application/json";

        public async Task<T> GetAsync<T>(HttpClient httpClient, string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);

            var response = await httpClient.SendAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public async Task<Result<ServerSuccess, ServerError>> PostAsync<T>(HttpClient httpClient, string path, T payload)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent(JsonConvert.SerializeObject(payload));
            request.Content.Headers.TryAddWithoutValidation(HeaderNames.ContentType, ContentType);

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
                return Result.Success<ServerSuccess, ServerError>(new ServerSuccess());

            return Result.Failure<ServerSuccess, ServerError>(new ServerError((int)response.StatusCode,
                                                              await response.Content.ReadAsStringAsync()));
        }
    }
}
