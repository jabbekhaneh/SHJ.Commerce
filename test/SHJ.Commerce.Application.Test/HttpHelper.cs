using Newtonsoft.Json;
using System.Text;

namespace SHJ.Commerce.Application.Test;
internal static class HttpHelper
{
    public static StringContent GetJsonHttpContent(object items)
    {
        return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
    }


    public static async Task<T> DeserializeResponseAsync<T>(this Task<HttpResponseMessage> message)
        where T : class
    {
        var messageValue = await message;
        return await messageValue.DeserializeResponseAsync<T>();
    }

    public static async Task<T> DeserializeResponseAsync<T>(this HttpResponseMessage message)
        where T : class
    {
        var stringResult = await message.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(stringResult);
    }
}

public class HttpResponseViewModel<T>
{
    public HttpResponseViewModel()
    {
        Messages = new List<string>();
    }
    public bool IsSuccess { get; set; }

    public int Status { get; set; }

    public T? Result { get; set; }

    public List<string> Messages { get; set; }


}