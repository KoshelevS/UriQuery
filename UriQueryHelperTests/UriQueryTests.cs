using UriQueryHelper;

namespace UriQueryHelperTests;

public class UriQueryTests
{
    private static readonly UriQuery target = new();

    [TestCase("")]
    [TestCase("?")]
    public void ParsesEmptyQuery(string query)
    {
        Assert.That(target.Parse(query), Is.Empty);
    }

    [Test]
    public void ParsesSingleParameterWithValue()
    {
        const string query = "?param=value";
        Assert.That(target.Parse(query), Does.ContainKey("param").WithValue(new[] { "value" }));
    }

    [Test]
    public void ParsesSingleValuelessParameter()
    {
        const string query = "?param=";
        Assert.That(target.Parse(query), Does.ContainKey("param").WithValue(new[] { "" }));
    }
}
