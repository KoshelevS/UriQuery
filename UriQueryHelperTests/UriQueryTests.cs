using Utils.UriQueryHelper;

namespace Utils.UriQueryHelperTests;

public class UriQueryTests
{
    private const string BaseUri = "https://host.com:443/path";

    [Test]
    public void With_AddsParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).With("param2", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1=value1&param2=value2"));
    }

    [Test]
    public void With_AddsParameterToEmptyQuery()
    {
        var target = new UriBuilder(BaseUri);

        target.Query = UriQuery.Parse(target.Query).With("param", "value").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param=value"));
    }

    [TestCase(null)]
    [TestCase("")]
    public void With_AddsEmptyParameter(string? value)
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).With("param2", value!).GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1=value1&param2="));
    }

    [Test]
    public void With_AddsMultivaluedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).With("param2", "value21", "value22").GetQuery();

        Assert.That(
            target.ToString(),
            Is.EqualTo($"{BaseUri}?param1=value1&param2[]=value21&param2[]=value22"));
    }

    [Test]
    public void With_OverridesExistingParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).With("param1", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1=value2"));
    }

    [Test]
    public void Append_AppendsValueToParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).Append("param1", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1[]=value1&param1[]=value2"));
    }

    [Test]
    public void Append_AddsParameterToEmptyQuery()
    {
        var target = new UriBuilder(BaseUri);

        target.Query = UriQuery.Parse(target.Query).Append("param", "value").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param=value"));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Append_AppendsEmptyValueToParameter(string? value)
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).Append("param1", value!).GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1[]=value1&param1[]="));
    }

    [Test]
    public void Append_AppendsMultipleValuesToParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).Append("param1", "value2", "value3").GetQuery();

        Assert.That(
            target.ToString(),
            Is.EqualTo($"{BaseUri}?param1[]=value1&param1[]=value2&param1[]=value3"));
    }

    [Test]
    public void Without_RemovesParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1&param2=value2");

        target.Query = UriQuery.Parse(target.Query).Without("param1").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param2=value2"));
    }

    [Test]
    public void Without_RemoveValueFromMultiValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2&param[]=value3");

        target.Query = UriQuery.Parse(target.Query).Without("param", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param[]=value1&param[]=value3"));
    }

    [Test]
    public void Without_RemoveMultipleValuesFromMultiValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2&param[]=value3");

        target.Query = UriQuery.Parse(target.Query).Without("param", "value2", "value3").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param=value1"));
    }

    [Test]
    public void Without_RemovesQuery()
    {
        var target = new UriBuilder($"{BaseUri}?param=value");

        target.Query = UriQuery.Parse(target.Query).Without("param").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo(BaseUri));
    }
}
