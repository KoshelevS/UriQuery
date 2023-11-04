using UriQueryHelper;

namespace UriQueryHelperTests;

public class SerializeTests
{
    [Test]
    public void SerializesEmptyParameters()
    {
        var parameters = new HashSet<UriQuery.Parameter>();
        var target = new UriQuery(parameters);
        Assert.That(target.Serialize(), Is.EqualTo("?"));
    }

    [Test]
    public void SerializesOneParameterValue()
    {
        var parameters = new HashSet<UriQuery.Parameter>
        {
            new("param", "value")
        };
        var target = new UriQuery(parameters);
        Assert.That(target.Serialize(), Is.EqualTo("?param=value"));
    }

    [Test]
    public void SerializesOneValuelessParameter()
    {
        var parameters = new HashSet<UriQuery.Parameter>
        {
            new("param", "")
        };
        var target = new UriQuery(parameters);
        Assert.That(target.Serialize(), Is.EqualTo("?param="));
    }

    [Test]
    public void SerializesOneMultivalueParameter()
    {
        var parameters = new HashSet<UriQuery.Parameter>
        {
            new("param", "value1"),
            new("param", "value2"),
            new("param", "value3"),
        };
        var target = new UriQuery(parameters);

        Assert.That(
            target.Serialize(),
            Is.EqualTo("?param[]=value1&param[]=value2&param[]=value3"));
    }

    [Test]
    public void SerializesMultipleParameters()
    {
        var parameters = new HashSet<UriQuery.Parameter>
        {
            new("param1", "value1"),
            new("param2", "value2"),
            new("param3", "value3"),
        };
        var target = new UriQuery(parameters);

        Assert.That(
            target.Serialize(),
            Is.EqualTo("?param1=value1&param2=value2&param3=value3"));
    }

    [Test]
    public void EncodesSpecialCharacters()
    {
        var parameters = new HashSet<UriQuery.Parameter>
        {
            new("param!", " !\"#$%&'()*+,/:;=?@[]"),
        };
        var target = new UriQuery(parameters);

        Assert.That(
            target.Serialize(),
            Is.EqualTo("?param%21=%20%21%22%23%24%25%26%27%28%29%2A%2B%2C%2F%3A%3B%3D%3F%40%5B%5D"));
    }
}
