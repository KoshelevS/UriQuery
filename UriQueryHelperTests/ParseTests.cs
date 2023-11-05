using Utils.UriQueryHelper;

namespace Utils.UriQueryHelperTests;

public class ParseTests
{
    [TestCase("")]
    [TestCase("?")]
    public void ParsesEmptyQuery(string query)
    {
        var actual = UriQuery.Parse(query);

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.GetParameters(), Is.Empty);
    }

    [TestCase("param=value")]
    [TestCase("?param=value")]
    public void ParsesSingleParameterWithValue(string query)
    {
        var actual = UriQuery.Parse(query);

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.GetParameters(), Does.Contain(("param", "value")));
        Assert.That(actual.GetParameters(), Has.One.Items);
    }

    [Test]
    public void ParsesSingleValuelessParameter()
    {
        var actual = UriQuery.Parse("?param=");

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.GetParameters(), Does.Contain(("param", "")));
        Assert.That(actual.GetParameters(), Has.One.Items);
    }

    [Test]
    public void ParsesMultipleParametersWithValues()
    {
        var actual = UriQuery.Parse("?param1=value1&param2=value2&param3=value3");

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.GetParameters(), Does.Contain(("param1", "value1")));
        Assert.That(actual.GetParameters(), Does.Contain(("param2", "value2")));
        Assert.That(actual.GetParameters(), Does.Contain(("param3", "value3")));
        Assert.That(actual.GetParameters(), Has.Exactly(3).Items);
    }

    [Test]
    public void ParsesMultivaluedParameter_SimpleSyntax()
    {
        var actual = UriQuery.Parse("?param=value1&param=value2&param=value3");

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.GetParameters(), Does.Contain(("param", "value1")));
        Assert.That(actual.GetParameters(), Does.Contain(("param", "value2")));
        Assert.That(actual.GetParameters(), Does.Contain(("param", "value3")));
        Assert.That(actual.GetParameters(), Has.Exactly(3).Items);
    }

    [Test]
    public void ParsesMultivaluedParameter_SquareBracketsSyntax()
    {
        var actual = UriQuery.Parse("?param[]=value1&param[]=value2&param[]=value3");

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.GetParameters(), Does.Contain(("param", "value1")));
        Assert.That(actual.GetParameters(), Does.Contain(("param", "value2")));
        Assert.That(actual.GetParameters(), Does.Contain(("param", "value3")));
        Assert.That(actual.GetParameters(), Has.Exactly(3).Items);
    }

    [TestCase("?param", "param")]
    [TestCase("?param1&param2=value2", "param1")]
    [TestCase("?param1=value1&param2", "param2")]
    [TestCase("?=value", "=value")]
    [TestCase("?=value1&param2=value2", "=value1")]
    [TestCase("?param1=value1&=value2", "=value2")]
    public void ParsesReportsInvalidParameters(string query, string invalidExpression)
    {
        var exception = Assert.Throws<ArgumentException>(() => UriQuery.Parse(query));

        Assert.That(
            exception.Message,
            Is.EqualTo($"'{invalidExpression}' is not a valid parameter expression (Parameter 'query')"));
    }

    [Test]
    public void CannotParseNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => UriQuery.Parse(null!));
        Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'query')"));
    }

    [Test]
    public void DecodesSpecialCharacters()
    {
        UriQuery actual = UriQuery.Parse(
            "?param%21=%20%21%22%23%24%25%26%27%28%29%2A%2B%2C%2F%3A%3B%3D%3F%40%5B%5D");

        Assert.That(actual, Is.Not.Null);
        Assert.That(
            actual.GetParameters(), Does.Contain(("param!", " !\"#$%&'()*+,/:;=?@[]")));
    }
}
