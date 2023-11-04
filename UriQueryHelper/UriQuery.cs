using System.Text;

namespace UriQueryHelper;

public class UriQuery
{
    public Dictionary<string, List<string>> Parameters { get; }

    public UriQuery(Dictionary<string, List<string>> parameters)
    {
        Parameters = parameters;
    }

    public static UriQuery Parse(string query)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var result = new Dictionary<string, List<string>>();

        foreach (var parameter in GetParameters(query.TrimStart('?')))
        {
            (var key, var value) = GetKeyValue(parameter) ?? throw new ArgumentException(
                $"'{parameter}' is not a valid parameter expression", nameof(query));

            if (result.TryGetValue(key, out var values))
            {
                values.Add(value);
            }
            else
            {
                result.Add(key, new List<string> { value });
            }
        }

        return new UriQuery(result);
    }

    public string Serialize()
    {
        var builder = new StringBuilder("?");

        foreach (var kvp in Parameters)
        {
            var encodedKey = Uri.EscapeDataString(kvp.Key);
            var key = kvp.Value.Count > 1 ? $"{encodedKey}[]" : encodedKey;
            var values = kvp.Value.Count == 0 ? new List<string>() { "" } : kvp.Value;

            foreach (var value in values)
            {
                builder.Append($"{key}={Uri.EscapeDataString(value)}&");
            }
        }

        return builder.ToString().TrimEnd('&');
    }

    private static (string key, string value)? GetKeyValue(string parameter) => parameter.Split('=') switch
    {
    ["", var _] => null,
    [var key, var value] => (
        Uri.UnescapeDataString(key.TrimEnd('[', ']')),
        Uri.UnescapeDataString(value)),
        _ => null,
    };

    private static string[] GetParameters(string query)
    {
        return query.Split("&", StringSplitOptions.RemoveEmptyEntries);
    }
}
