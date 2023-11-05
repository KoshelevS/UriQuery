using UriQueryHelper;

namespace UriQueryHelperTests;

public class UriQueryTests
{
    private const string BaseUri = "https://host.com:443/path";

    [Test]
    public void AddQuery()
    {
        var target = new UriBuilder(BaseUri);

        var uriQuery = UriQuery.Parse(target.Query);
        uriQuery.AddParameter("param", "value");
        target.Query = uriQuery.GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param=value"));
    }

    [Test]
    public void AddParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        var uriQuery = UriQuery.Parse(target.Query);
        uriQuery.AddParameter("param2", "value2");
        target.Query = uriQuery.GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1=value1&param2=value2"));
    }

    [Test]
    public void AddValueToParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");

        var uriQuery = UriQuery.Parse(target.Query);
        uriQuery.AddParameter("param1", "value2");
        target.Query = uriQuery.GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1[]=value1&param1[]=value2"));
    }

    [Test]
    public void RemoveSingleValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1&param2=value2");

        var uriQuery = UriQuery.Parse(target.Query);
        uriQuery.RemoveParameter("param1");
        target.Query = uriQuery.GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param2=value2"));
    }

    [Test]
    public void RemoveMultiValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1[]=value11&param1[]=value12&param2=value21");

        var uriQuery = UriQuery.Parse(target.Query);
        uriQuery.RemoveParameter("param1");
        target.Query = uriQuery.GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param2=value21"));
    }

    [Test]
    public void RemoveValueFromMultiValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2&param[]=value3");

        var uriQuery = UriQuery.Parse(target.Query);
        uriQuery.RemoveParameter("param", "value2");
        target.Query = uriQuery.GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param[]=value1&param[]=value3"));
    }

    [Test]
    public void RemoveQuery()
    {
        var target = new UriBuilder($"{BaseUri}?param=value");

        var uriQuery = UriQuery.Parse(target.Query);
        uriQuery.RemoveParameter("param", "value");
        target.Query = uriQuery.GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?"));
    }

    [Test]
    public void RemoveMultiValuedQuery()
    {
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2");

        var uriQuery = UriQuery.Parse(target.Query);
        uriQuery.RemoveParameter("param");
        target.Query = uriQuery.GetQuery();

        Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?"));
    }
}
