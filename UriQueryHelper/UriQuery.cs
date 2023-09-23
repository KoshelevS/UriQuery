namespace UriQueryHelper;

public class UriQuery
{
    public Dictionary<string, string[]> Parse(string query)
    {
        var result = new Dictionary<string, string[]>();

        foreach (var parameter in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var items = parameter.Split('=');
            result.Add(items[0], new[] { items[1] });
        }

        return result;
    }
}
