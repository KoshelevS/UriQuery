using System.Text;

namespace UriQueryHelper;

public class UriQuery
{
    public HashSet<Parameter> Parameters { get; }

    public UriQuery(HashSet<Parameter> parameters)
    {
        Parameters = parameters;
    }

    // TODO: Create a class to store parameter(s)
    // TODO: this class allows to keep multiple values + do encoding/decoding, building kvp string
    public static UriQuery Parse(string query)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var result = new HashSet<Parameter>();

        foreach (var token in SplitParameters(query.TrimStart('?')))
        {
            var parameter = TryParseParameter(token);
            if (parameter == null)
            {
                throw new ArgumentException(
                    $"'{token}' is not a valid parameter expression", nameof(query));
            }

            result.Add(parameter);
        }

        return new UriQuery(result);
    }

    public void AddParameter(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{name}' is not a valid parameter name", nameof(name));
        }

        Parameters.Add(new Parameter(name, value ?? string.Empty));
    }

    public void RemoveParameter(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{name}' is not a valid parameter name", nameof(name));
        }

        Parameters.RemoveWhere(x => x.Name == name);
    }

    public void RemoveParameter(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{name}' is not a valid parameter name", nameof(name));
        }

        Parameters.Remove(new Parameter(name, value ?? string.Empty));
    }

    public string Serialize()
    {
        var builder = new StringBuilder("?");

        foreach (var pair in Parameters)
        {
            builder.Append(pair.Serialize(Parameters.Count(x => x.Name == pair.Name) > 1))
                   .Append('&');
        }

        return builder.ToString().TrimEnd('&');
    }

    private static Parameter? TryParseParameter(string parameter) => parameter.Split('=') switch
    {
    ["", var _] => null,
    [var key, var value] => new Parameter(
        Uri.UnescapeDataString(key.TrimEnd('[', ']')),
        Uri.UnescapeDataString(value)),
        _ => null,
    };

    private static string[] SplitParameters(string query)
    {
        return query.Split("&", StringSplitOptions.RemoveEmptyEntries);
    }

    public record Parameter(string Name, string Value)
    {
        public string Serialize(bool isMultivalue) => isMultivalue
            ? $"{Uri.EscapeDataString(Name)}[]={Uri.EscapeDataString(Value)}"
            : $"{Uri.EscapeDataString(Name)}={Uri.EscapeDataString(Value)}";
    }
}
