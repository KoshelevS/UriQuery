# UriQuery
UriQuery is an unitility class that can be used to parse and modify URI query strings.

## Usage
```csharp
[Test]
public void AddParameter()
{
    var actual = UriQuery.Parse("?param1=value1").With("param2", "value2").GetQuery();
    Assert.That(actual, Is.EqualTo("?param1=value1&param2=value2"));
}

[Test]
public void OverrideExistingParameter()
{
    var actual = UriQuery.Parse("?param1=value1").With("param1", "value2").GetQuery();
    Assert.That(actual, Is.EqualTo("?param1=value2"));
}

[Test]
public void AppendValueToParameter()
{
    var actual = UriQuery.Parse("?param1=value1").Append("param1", "value2").GetQuery();
    Assert.That(actual, Is.EqualTo("?param1[]=value1&param1[]=value2"));
}

[Test]
public void RemoveParameter()
{
    var actual = UriQuery.Parse("?param1=value1&param2=value2").Without("param1").GetQuery();
    Assert.That(actual, Is.EqualTo("?param2=value2"));
}

[Test]
public void RemoveValueFromMultiValuedParameter()
{
    var actual = UriQuery.Parse("?param[]=value1&param[]=value2&param[]=value3")
                         .Without("param", "value2")
                         .GetQuery();

    Assert.That(actual, Is.EqualTo("?param[]=value1&param[]=value3"));
}
```

See `UriQueryTests.cs` file for other usage samples.

# QueryBuilder
QueryBuilder is a helper class that can be used to modify Query property value of a UriBuilder class instance.

## Usage
```csharp
private const string BaseUri = "https://host.com:443/path";

[Test]
public void AddParameter()
{
    var target = new UriBuilder($"{BaseUri}?param1=value1");
    target.ModifyQuery().Set("param2", "value2").Done();

    Assert.That(target.Query, Is.EqualTo("?param1=value1&param2=value2"));
}

[Test]
public void OverrideExistingParameter()
{
    var target = new UriBuilder($"{BaseUri}?param1=value1");
    target.ModifyQuery().Set("param1", "value2").Done();

    Assert.That(target.Query, Is.EqualTo("?param1=value2"));
}

[Test]
public void AppendValueToParameter()
{
        var target = new UriBuilder($"{BaseUri}?param1=value1");
        target.ModifyQuery().Add("param1", "value2").Done();

        Assert.That(target.Query, Is.EqualTo("?param1[]=value1&param1[]=value2"));
}

[Test]
public void RemoveParameter()
{
        var target = new UriBuilder($"{BaseUri}?param1=value1&param2=value2");
        target.ModifyQuery().Remove("param1").Done();

        Assert.That(target.Query, Is.EqualTo("?param2=value2"));
}

[Test]
public void RemoveValueFromMultiValuedParameter()
{
        var target = new UriBuilder($"{BaseUri}?param[]=value1&param[]=value2&param[]=value3");
        target.ModifyQuery().Remove("param", "value2").Done();

        Assert.That(target.Query, Is.EqualTo("?param[]=value1&param[]=value3"));
}
```

See `QueryBuilderTests.cs` file for other usage samples.
