using Utils.UriQueryHelper;

namespace Utils.UriQueryHelperTests;

public class UriQueryTests
{
    private const string BaseUri = "https://host.com:443/path";

    [Test]
    public void AddQuery()
    {
        var target = new UriBuilder(BaseUri);

        target.Query = UriQuery.Parse(target.Query).Add("param", "value").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param=value"));
    }

    [Test]
    public void AddParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).Add("param2", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1=value1&param2=value2"));
    }

    [Test]
    public void AddValueToParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).Add("param1", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1[]=value1&param1[]=value2"));
    }

    [Test]
    public void RemoveSingleValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1&param2=value2");

        target.Query = UriQuery.Parse(target.Query).RemoveAll("param1").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param2=value2"));
    }

    [Test]
    public void RemoveMultiValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1[]=value11&param1[]=value12&param2=value21");

        target.Query = UriQuery.Parse(target.Query).RemoveAll("param1").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param2=value21"));
    }

    [Test]
    public void RemoveValueFromMultiValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2&param[]=value3");

        target.Query = UriQuery.Parse(target.Query).Remove("param", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param[]=value1&param[]=value3"));
    }

    [Test]
    public void RemoveQuery()
    {
        var target = new UriBuilder($"{BaseUri}?param=value");

        target.Query = UriQuery.Parse(target.Query).Remove("param", "value").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo(BaseUri));
    }

    [Test]
    public void RemoveMultiValuedQuery()
    {
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2");

        target.Query = UriQuery.Parse(target.Query).RemoveAll("param").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo(BaseUri));
    }

    [Test]
    public void With_AddsParameters()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).With("param2", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1=value1&param2=value2"));
    }

    [Test]
    public void With_OverridesExistingParameters()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).With("param1", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1=value2"));
    }

    [Test]
    public void With_AddsMultivaluedParameters()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).With("param2", "value21", "value22").GetQuery();

        Assert.That(
            target.ToString(),
            Is.EqualTo($"{BaseUri}?param1=value1&param2[]=value21&param2[]=value22"));
    }

    [Test]
    public void Append_AppendsValueToParameters()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        target.Query = UriQuery.Parse(target.Query).Append("param1", "value2").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1[]=value1&param1[]=value2"));
    }

    [Test]
    public void Without_RemovesParameters()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1&param2=value2");

        target.Query = UriQuery.Parse(target.Query).Without("param1").GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param2=value2"));
    }
}
