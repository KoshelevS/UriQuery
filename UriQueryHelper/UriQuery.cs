namespace UriQueryHelper;

public class UriQuery
{
    public Dictionary<string, string[]> Parse(string query)
    {
        var cleanQuery = query.TrimStart('?');
        if (!string.IsNullOrWhiteSpace(cleanQuery))
        {
            var items = cleanQuery.Split('=');
            return new Dictionary<string, string[]>
            {
                [items[0]] = new[] { items[1] },
            };
        }
        else
        {
            return new Dictionary<string, string[]>();
        }
    }
}
