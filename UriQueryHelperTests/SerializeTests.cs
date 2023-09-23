using UriQueryHelper;

namespace UriQueryHelperTests;

public class SerializeTests
{
    private static readonly UriQuery target = new();

    [Test]
    public void SerializesEmptyParameters()
    {
        var parameters = new Dictionary<string, List<string>>();
        Assert.That(target.Serialize(parameters), Is.EqualTo("?"));
    }

    [Test]
    public void SerializesOneParameterValue()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param"] = new() { "value" }
        };
        Assert.That(target.Serialize(parameters), Is.EqualTo("?param=value"));
    }

    [Test]
    public void SerializesOneValuelessParameter()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param"] = new()
        };
        Assert.That(target.Serialize(parameters), Is.EqualTo("?param="));
    }

    [Test]
    public void SerializesOneMultivalueParameter()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param"] = new() { "value1", "value2", "value3" }
        };

        Assert.That(
            target.Serialize(parameters),
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

        Assert.That(
            target.Serialize(parameters),
            Is.EqualTo("?param1=value1&param2=value2&param3=value3"));
    }

    [Test]
    public void CannotSerializeNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => target.Parse(null!));
        Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'query')"));
    }

    [Test]
    public void EncodesSpecialCharacters()
    {
        var parameters = new Dictionary<string, List<string>>
        {
            ["param!"] = new() { " !\"#$%&'()*+,/:;=?@[]" },
        };

        Assert.That(
            target.Serialize(parameters),
            Is.EqualTo("?param%21=%20%21%22%23%24%25%26%27%28%29%2A%2B%2C%2F%3A%3B%3D%3F%40%5B%5D"));
    }
}
