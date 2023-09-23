using System.Text;

namespace UriQueryHelper;

public class UriQuery
{
    public Dictionary<string, List<string>> Parse(string query)
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

        return result;
    }

    public string Serialize(Dictionary<string, List<string>> data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        var builder = new StringBuilder("?");

        foreach (var kvp in data)
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

    private (string key, string value)? GetKeyValue(string parameter) => parameter.Split('=') switch
    {
        ["", var _] => null,
        [var key, var value] => (key.TrimEnd('[', ']'), value),
        _ => null,
    };

    private string[] GetParameters(string query)
    {
        return query.Split("&", StringSplitOptions.RemoveEmptyEntries);
    }
}
