using UriQueryHelper;

namespace UriQueryHelperTests;

public class UriQueryTests
{
    [Test]
    public void QueryRoundtrip()
    {
        var query = "?param1[]=value11&param1[]=value12&param%21=%20%21%22%23%24%25%26%27";
        Assert.That(UriQuery.Parse(query).Serialize(), Is.EqualTo(query));
    }

    [Test]
    public void AddParameter()
    {
        var data = UriQuery.Parse("?param1=value1");
        data.AddParameter("param2", "value2");

        Assert.That(data.Serialize(), Is.EqualTo("?param1=value1&param2=value2"));
    }

    [Test]
    public void AddValueToParameter()
    {
        var data = UriQuery.Parse("?param=value1");
        data.AddParameter("param", "value2");

        Assert.That(data.Serialize(), Is.EqualTo("?param[]=value1&param[]=value2"));
    }

    [Test]
    public void RemoveSingleValuedParameter()
    {
        var data = UriQuery.Parse("?param1=value1&param2=value2");
        data.RemoveParameter("param1");

        Assert.That(data.Serialize(), Is.EqualTo("?param2=value2"));
    }

    [Test]
    public void RemoveMultiValuedParameter()
    {
        var data = UriQuery.Parse("?param1[]=value11&param1[]=value12&param1[]=value13&param2=value21");
        data.RemoveParameter("param1");

        Assert.That(data.Serialize(), Is.EqualTo("?param2=value21"));
    }

    [Test]
    public void RemoveValueFromMultiValuedParameter()
    {
        var data = UriQuery.Parse("?param[]=value1&param[]=value2&param[]=value3");
        data.RemoveParameter("param", "value2");

        Assert.That(data.Serialize(), Is.EqualTo("?param[]=value1&param[]=value3"));
    }
}
