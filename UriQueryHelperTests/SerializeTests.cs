using UriQueryHelper;

namespace UriQueryHelperTests;

public class SerializeTests
{
    [Test]
    public void SerializesEmptyParameters()
    {
        var target = new UriQuery();
        Assert.That(target.Serialize(), Is.EqualTo("?"));
    }

    [Test]
    public void SerializesOneParameterValue()
    {
        var target = new UriQuery();
        target.AddParameter("param", "value");

        Assert.That(target.Serialize(), Is.EqualTo("?param=value"));
    }

    [Test]
    public void SerializesOneValuelessParameter()
    {
        var target = new UriQuery();
        target.AddParameter("param", "");

        Assert.That(target.Serialize(), Is.EqualTo("?param="));
    }

    [Test]
    public void SerializesOneMultivalueParameter()
    {
        var target = new UriQuery();
        target.AddParameter("param", "value1");
        target.AddParameter("param", "value2");
        target.AddParameter("param", "value3");

        Assert.That(target.Serialize(), Is.EqualTo("?param[]=value1&param[]=value2&param[]=value3"));
    }

    [Test]
    public void SerializesMultipleParameters()
    {
        var target = new UriQuery();
        target.AddParameter("param1", "value1");
        target.AddParameter("param2", "value2");
        target.AddParameter("param3", "value3");

        Assert.That(target.Serialize(), Is.EqualTo("?param1=value1&param2=value2&param3=value3"));
    }

    [Test]
    public void EncodesSpecialCharacters()
    {
        var target = new UriQuery();
        target.AddParameter("param!", " !\"#$%&'()*+,/:;=?@[]");

        Assert.That(
            target.Serialize(),
            Is.EqualTo("?param%21=%20%21%22%23%24%25%26%27%28%29%2A%2B%2C%2F%3A%3B%3D%3F%40%5B%5D"));
    }
}
