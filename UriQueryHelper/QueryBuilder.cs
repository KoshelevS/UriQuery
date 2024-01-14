namespace Utils.UriQueryHelper;

public class QueryBuilder
{
    private readonly UriBuilder builder;
    private readonly UriQuery query;

    public QueryBuilder(UriBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        this.builder = builder;
        this.query = UriQuery.Parse(builder.Query);
    }

    public QueryBuilder Set(string name, params string[] values)
    {
        query.With(name, values);
        return this;
    }

    public QueryBuilder Add(string name, params string[] values)
    {
        query.Append(name, values);
        return this;
    }

    public QueryBuilder Remove(string name)
    {
        query.Without(name);
        return this;
    }

    public QueryBuilder Remove(string name, params string[] values)
    {
        query.Without(name, values);
        return this;
    }

    public UriBuilder Done()
    {
        builder.Query = query.GetQuery();
        return builder;
    }
}
