namespace UriQueryHelper;

public class UriQuery
{
    public Dictionary<string, List<string>> Parse(string query)
    {
        var result = new Dictionary<string, List<string>>();

        foreach (var parameter in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var items = parameter.Split('=');
            if (items.Length != 2 || string.IsNullOrEmpty(items[0]))
            {
                throw new ArgumentException(
                    $"'{parameter}' is not a valid parameter expression", nameof(query));
            }

            var key = items[0].TrimEnd(']').TrimEnd('[');
            var value = items[1];

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
}
