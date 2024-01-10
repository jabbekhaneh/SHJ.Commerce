using Newtonsoft.Json;

namespace SHJ.Commerce.Application.Test.Configurations.Extentions;
public static class HttpResponseMessageExtensions
{
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

