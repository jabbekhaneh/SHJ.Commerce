using Newtonsoft.Json;
using System.Text;

namespace SHJ.Commerce.Application.Test;
internal class HttpHelper
{
    public static StringContent GetJsonHttpContent(object items)
    {
        return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
    }
}
