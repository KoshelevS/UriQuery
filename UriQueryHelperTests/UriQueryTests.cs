using UriQueryHelper;

namespace UriQueryHelperTests;

public class UriQueryTests
{
    private static readonly UriQuery target = new();

    [Test]
    public void QueryRoundtripTest()
    {
        var query = "?param1[]=value11&param1[]=value12&param%21=%20%21%22%23%24%25%26%27";
        Assert.That(target.Serialize(target.Parse(query)), Is.EqualTo(query));
    }
}
