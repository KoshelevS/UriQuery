using Utils.UriQueryHelper;

namespace Utils.UriQueryHelperTests;

public class UriQueryTests
{
    [Test]
    public void With_AddsParameter()
    {
        var actual = UriQuery.Parse("?param1=value1").With("param2", "value2").GetQuery();
        Assert.That(actual, Is.EqualTo("?param1=value1&param2=value2"));
    }

    [Test]
    public void With_AddsParameterToEmptyQuery()
    {
        var actual = new UriQuery().With("param", "value").GetQuery();
        Assert.That(actual, Is.EqualTo("?param=value"));
    }

    [TestCase(null)]
    [TestCase("")]
    public void With_AddsEmptyParameter(string? value)
    {
        var actual = UriQuery.Parse("?param1=value1").With("param2", value!).GetQuery();
        Assert.That(actual, Is.EqualTo("?param1=value1&param2="));
    }

    [Test]
    public void With_AddsMultivaluedParameter()
    {
        var actual = UriQuery.Parse("?param1=value1").With("param2", "value21", "value22").GetQuery();
        Assert.That(actual, Is.EqualTo("?param1=value1&param2[]=value21&param2[]=value22"));
    }

    [Test]
    public void With_OverridesExistingParameter()
    {
        var actual = UriQuery.Parse("?param1=value1").With("param1", "value2").GetQuery();
        Assert.That(actual, Is.EqualTo("?param1=value2"));
    }

    [Test]
    public void Append_AppendsValueToParameter()
    {
        var actual = UriQuery.Parse("?param1=value1").Append("param1", "value2").GetQuery();
        Assert.That(actual, Is.EqualTo("?param1[]=value1&param1[]=value2"));
    }

    [Test]
    public void Append_AddsParameterToEmptyQuery()
    {
        var actual = new UriQuery().Append("param", "value").GetQuery();
        Assert.That(actual, Is.EqualTo("?param=value"));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Append_AppendsEmptyValueToParameter(string? value)
    {
        var actual = UriQuery.Parse("?param1=value1").Append("param1", value!).GetQuery();
        Assert.That(actual, Is.EqualTo($"?param1[]=value1&param1[]="));
    }

    [Test]
    public void Append_AppendsMultipleValuesToParameter()
    {
        var actual = UriQuery.Parse("?param1=value1").Append("param1", "value2", "value3").GetQuery();
        Assert.That(actual, Is.EqualTo("?param1[]=value1&param1[]=value2&param1[]=value3"));
    }

    [Test]
    public void Without_RemovesParameter()
    {
        var actual = UriQuery.Parse("?param1=value1&param2=value2").Without("param1").GetQuery();
        Assert.That(actual, Is.EqualTo("?param2=value2"));
    }

    [Test]
    public void Without_RemovesValueFromMultiValuedParameter()
    {
        var actual = UriQuery.Parse("?param[]=value1&param[]=value2&param[]=value3")
                             .Without("param", "value2")
                             .GetQuery();

        Assert.That(actual, Is.EqualTo("?param[]=value1&param[]=value3"));
    }

    [Test]
    public void Without_RemovesMultipleValuesFromMultiValuedParameter()
    {
        var actual = UriQuery.Parse("?param[]=value1&param[]=value2&param[]=value3")
                             .Without("param", "value2", "value3")
                             .GetQuery();

        Assert.That(actual, Is.EqualTo("?param=value1"));
    }

    [Test]
    public void Without_RemovesQuery()
    {
        var actual = UriQuery.Parse("?param=value").Without("param").GetQuery();
        Assert.That(actual, Is.Empty);
    }
}
