using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
public static class SessionHelper
{
    public static void SetListObjectInSession<T>(this ISession session, string key, List<T> value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetListObjectFromSession<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}