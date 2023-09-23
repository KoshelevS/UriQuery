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

    [Test]
    public void ParsesMultipleParametersWithValues()
    {
        const string query = "?param1=value1&param2=value2&param3=value3";
        var actual = target.Parse(query);

        Assert.That(actual, Does.ContainKey("param1").WithValue(new[] { "value1" }));
        Assert.That(actual, Does.ContainKey("param2").WithValue(new[] { "value2" }));
        Assert.That(actual, Does.ContainKey("param3").WithValue(new[] { "value3" }));
    }
}
