using UriQueryHelper;

namespace UriQueryHelperTests;

public class UriQueryTests
{
    private static readonly UriQuery target = new();

    [SetUp]
    public void Setup()
    {
    }

    [TestCase("")]
    [TestCase("?")]
    public void ParsesEmptyQuery(string query)
    {
        Assert.That(target.Parse(query), Is.Empty);
    }
}
