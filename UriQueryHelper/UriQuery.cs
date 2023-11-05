using System.Text;

namespace UriQueryHelper;

public class UriQuery
{
    private readonly HashSet<Parameter> parameters = new();

    public static UriQuery Parse(string query)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var result = new UriQuery();

        foreach (var token in SplitParameters(query.TrimStart('?')))
        {
            var (name, value) = TryParseParameter(token) ?? throw new ArgumentException(
                $"'{token}' is not a valid parameter expression", nameof(query));
            result.AddParameter(name, value);
        }

        return result;
    }

    public void AddParameter(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{name}' is not a valid parameter name", nameof(name));
        }

        parameters.Add(new Parameter(name, value ?? string.Empty));
    }

    public void RemoveParameter(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{name}' is not a valid parameter name", nameof(name));
        }

        parameters.RemoveWhere(x => x.Name == name);
    }

    public void RemoveParameter(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{name}' is not a valid parameter name", nameof(name));
        }

        parameters.Remove(new Parameter(name, value ?? string.Empty));
    }

    public IEnumerable<(string Name, string Value)> GetParameters()
    {
        return parameters.Select(pair => (pair.Name, pair.Value));
    }

    public string Serialize()
    {
        var builder = new StringBuilder("?");

        foreach (var pair in parameters)
        {
            builder.Append(pair.Serialize(parameters.Count(x => x.Name == pair.Name) > 1))
                   .Append('&');
        }

        return builder.ToString().TrimEnd('&');
    }

    private static (string Name, string Value)? TryParseParameter(string token) => token.Split('=') switch
    {
        { Length: 2 } pair when pair[0] != string.Empty => new(
            Uri.UnescapeDataString(pair[0].TrimEnd('[', ']')),
            Uri.UnescapeDataString(pair[1])),
        _ => null,
    };

    private static string[] SplitParameters(string query)
    {
        return query.Split("&", StringSplitOptions.RemoveEmptyEntries);
    }

    private record Parameter(string Name, string Value)
    {
        public string Serialize(bool isMultivalue) => isMultivalue
            ? $"{Uri.EscapeDataString(Name)}[]={Uri.EscapeDataString(Value)}"
            : $"{Uri.EscapeDataString(Name)}={Uri.EscapeDataString(Value)}";
    }
}
