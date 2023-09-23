namespace UriQueryHelper;

public class UriQuery
{
    public Dictionary<string, List<string>> Parse(string query)
    {
        var result = new Dictionary<string, List<string>>();

        foreach (var parameter in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var items = parameter.Split('=');
            var key = items[0];
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
