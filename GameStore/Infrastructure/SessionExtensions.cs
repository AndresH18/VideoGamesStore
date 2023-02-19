using System.Text.Json;

namespace GameStore.Infrastructure;

/// <summary>
/// /// Static methods to interact with <b>Session</b> storage.
/// </summary>
public static class SessionExtensions
{
    /// <summary>
    /// Stores the <paramref name="value"/> on the Session data using the <paramref name="key"/>
    /// </summary>
    /// <param name="session"></param>
    /// <param name="key">The key to retrieve the <paramref name="value"/></param>
    /// <param name="value">The <see cref="Object"/> to store in the Session storage</param>
    public static void SetJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    /// <summary>
    /// Retrieves <typeparamref name="T"/> from the Session storage
    /// </summary>
    /// <param name="session"></param>
    /// <param name="key">The key of the object</param>
    /// <typeparam name="T">The type to deserialize the object </typeparam>
    /// <returns>The deserialized object or null.</returns>
    /// <exception cref="JsonException">The JSON is invalid. - or - <typeparamref name="T"/> is not compatible with the JSON. - or - There is remaining data in the string beyond a single JSON value.</exception>
    /// <exception cref="NotSupportedException">There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/> for <typeparamref name="T"/> or its serializable members.</exception>
    public static T? GetObject<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        return data != null ? JsonSerializer.Deserialize<T>(data) : default;
    }
}