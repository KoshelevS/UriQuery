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
        var actual = target.Parse("?param=value");

        Assert.That(actual, Does.ContainKey("param").WithValue(new[] { "value" }));
        Assert.That(actual, Has.One.Items);
    }

    [Test]
    public void ParsesSingleValuelessParameter()
    {
        var actual = target.Parse("?param=");

        Assert.That(actual, Does.ContainKey("param").WithValue(new[] { "" }));
        Assert.That(actual, Has.One.Items);
    }

    [Test]
    public void ParsesMultipleParametersWithValues()
    {
        var actual = target.Parse("?param1=value1&param2=value2&param3=value3");

        Assert.That(actual, Does.ContainKey("param1").WithValue(new[] { "value1" }));
        Assert.That(actual, Does.ContainKey("param2").WithValue(new[] { "value2" }));
        Assert.That(actual, Does.ContainKey("param3").WithValue(new[] { "value3" }));
        Assert.That(actual, Has.Exactly(3).Items);
    }

    [Test]
    public void ParsesMultivaluedParameter_SimpleSyntax()
    {
        var actual = target.Parse("?param=value1&param=value2&param=value3");

        Assert.That(actual, Does.ContainKey("param").WithValue(new[] { "value1", "value2", "value3" }));
        Assert.That(actual, Has.One.Items);
    }

    [Test]
    public void ParsesMultivaluedParameter_SquareBracketsSyntax()
    {
        var actual = target.Parse("?param[]=value1&param[]=value2&param[]=value3");

        Assert.That(actual, Does.ContainKey("param").WithValue(new[] { "value1", "value2", "value3" }));
        Assert.That(actual, Has.One.Items);
    }
}
