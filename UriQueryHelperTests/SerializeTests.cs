using UriQueryHelper;

namespace UriQueryHelperTests;

public class SerializeTests
{
    [Test]
    public void SerializesEmptyParameters()
    {
        var parameters = new Dictionary<string, List<string>>();
        var target = new UriQuery(parameters);
        Assert.That(target.Serialize(), Is.EqualTo("?"));
    }

    [Test]
    public void SerializesOneParameterValue()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param"] = new() { "value" }
        };
        var target = new UriQuery(parameters);
        Assert.That(target.Serialize(), Is.EqualTo("?param=value"));
    }

    [Test]
    public void SerializesOneValuelessParameter()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param"] = new()
        };
        var target = new UriQuery(parameters);
        Assert.That(target.Serialize(), Is.EqualTo("?param="));
    }

    [Test]
    public void SerializesOneMultivalueParameter()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param"] = new() { "value1", "value2", "value3" }
        };
        var target = new UriQuery(parameters);

        Assert.That(
            target.Serialize(),
            Is.EqualTo("?param[]=value1&param[]=value2&param[]=value3"));
    }

    [Test]
    public void SerializesMultipleParameters()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param1"] = new() { "value1" },
            ["param2"] = new() { "value2" },
            ["param3"] = new() { "value3" },
        };
        var target = new UriQuery(parameters);

        Assert.That(
            target.Serialize(),
            Is.EqualTo("?param1=value1&param2=value2&param3=value3"));
    }

    [Test]
    public void EncodesSpecialCharacters()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param!"] = new() { " !\"#$%&'()*+,/:;=?@[]" },
        };
        var target = new UriQuery(parameters);

        Assert.That(
            target.Serialize(),
            Is.EqualTo("?param%21=%20%21%22%23%24%25%26%27%28%29%2A%2B%2C%2F%3A%3B%3D%3F%40%5B%5D"));
    }
}
