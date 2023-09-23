using UriQueryHelper;

namespace UriQueryHelperTests;

public class UriQueryTests
{
    private static readonly UriQuery target = new();

    [Test]
    public void QueryRoundtrip()
    {
        var query = "?param1[]=value11&param1[]=value12&param%21=%20%21%22%23%24%25%26%27";
        Assert.That(target.Serialize(target.Parse(query)), Is.EqualTo(query));
    }

    [Test]
    public void AddParameter()
    {
        var data = target.Parse("?param1=value1");
        data.Add("param2", new() { "value2" });

        Assert.That(target.Serialize(data), Is.EqualTo("?param1=value1&param2=value2"));
    }

    [Test]
    public void RemoveParameter()
    {
        var data = target.Parse("?param1=value1&param2=value2");
        data.Remove("param1");

        Assert.That(target.Serialize(data), Is.EqualTo("?param2=value2"));
    }

    [Test]
    public void AddValueToParameter()
    {
        var data = target.Parse("?param=value1");
        data["param"].Add("value2");

        Assert.That(target.Serialize(data), Is.EqualTo("?param[]=value1&param[]=value2"));
    }

    [Test]
    public void RemoveValueFromParameter()
    {
        var data = target.Parse("?param[]=value1&param[]=value2&param[]=value3");
        data["param"].Remove("value2");

        Assert.That(target.Serialize(data), Is.EqualTo("?param[]=value1&param[]=value3"));
    }
}
