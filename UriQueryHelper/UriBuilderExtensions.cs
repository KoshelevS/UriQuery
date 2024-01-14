namespace Utils.UriQueryHelper;

public static class UriBuilderExtensions
{
    public static QueryBuilder ModifyQuery(this UriBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        return new QueryBuilder(builder);
    }
}
