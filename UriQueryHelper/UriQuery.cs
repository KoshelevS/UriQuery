namespace UriQueryHelper;

public class UriQuery
{
    public Dictionary<string, string[]> Parse(string query)
    {
        var cleanQuery = query.TrimStart('?');
        if (!string.IsNullOrWhiteSpace(cleanQuery))
        {
            var result = new Dictionary<string, string[]>();

            foreach (var parameter in cleanQuery.Split('&'))
            {
                var items = parameter.Split('=');
                result.Add(items[0], new[] { items[1] });
            }

            return result;
        }
        else
        {
            return new Dictionary<string, string[]>();
        }
    }
}
