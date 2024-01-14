using Utils.UriQueryHelper;

namespace Utils.UriQueryHelperTests;

public class QueryBuilderTests
{
    private const string BaseUri = "https://host.com:443/path";

    [Test]
    public void Set_AddsParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");
        target.ModifyQuery().Set("param2", "value2").Done();

        Assert.That(target.Query, Is.EqualTo("?param1=value1&param2=value2"));
    }

    [Test]
    public void Set_AddsParameterToEmptyQuery()
    {
        var target = new UriBuilder(BaseUri);
        target.ModifyQuery().Set("param", "value").Done();

        Assert.That(target.Query, Is.EqualTo("?param=value"));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Set_AddsEmptyParameter(string? value)
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");
        target.ModifyQuery().Set("param2", value!).Done();

        Assert.That(target.Query, Is.EqualTo("?param1=value1&param2="));
    }

    [Test]
    public void Set_AddsMultivaluedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");
        target.ModifyQuery().Set("param2", "value21", "value22").Done();

        Assert.That(target.Query, Is.EqualTo("?param1=value1&param2[]=value21&param2[]=value22"));
    }

    [Test]
    public void Set_OverridesExistingParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");
        target.ModifyQuery().Set("param1", "value2").Done();

        Assert.That(target.Query, Is.EqualTo("?param1=value2"));
    }

    [Test]
    public void Add_AppendsValueToParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");
        target.ModifyQuery().Add("param1", "value2").Done();

        Assert.That(target.Query, Is.EqualTo("?param1[]=value1&param1[]=value2"));
    }

    [Test]
    public void Add_AddsParameterToEmptyQuery()
    {
        var target = new UriBuilder(BaseUri);
        target.ModifyQuery().Add("param", "value").Done();

        Assert.That(target.Query, Is.EqualTo("?param=value"));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Add_AppendsEmptyValueToParameter(string? value)
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");
        target.ModifyQuery().Add("param1", value!).Done();

        Assert.That(target.Query, Is.EqualTo("?param1[]=value1&param1[]="));
    }

    [Test]
    public void Add_AppendsMultipleValuesToParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1");
        target.ModifyQuery().Add("param1", "value2", "value3").Done();

        Assert.That(target.Query, Is.EqualTo("?param1[]=value1&param1[]=value2&param1[]=value3"));
    }

    [Test]
    public void Remove_RemovesParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param1=value1&param2=value2");
        target.ModifyQuery().Remove("param1").Done();

        Assert.That(target.Query, Is.EqualTo("?param2=value2"));
    }

    [Test]
    public void Remove_RemovesValueFromMultiValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2&param[]=value3");
        target.ModifyQuery().Remove("param", "value2").Done();

        Assert.That(target.Query, Is.EqualTo("?param[]=value1&param[]=value3"));
    }

    [Test]
    public void Remove_RemovesMultipleValuesFromMultiValuedParameter()
    {
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2&param[]=value3");
        target.ModifyQuery().Remove("param", "value2", "value3").Done();

        Assert.That(target.Query, Is.EqualTo("?param=value1"));
    }

    [Test]
    public void Remove_RemovesQuery()
    {
        var target = new UriBuilder($"{BaseUri}?param=value");
        target.ModifyQuery().Remove("param").Done();

        Assert.That(target.Query, Is.Empty);
    }
}
