# UriQuery
UriQuery is an unitility class that can be used to modify URI query strings. Can be uses with `Uri` or `UriBuilder` class objects to modify their Query property.

# Usage
```csharp
private const string BaseUri = "https://host.com:443/path";

[Test]
public void AddParameter()
{
    var target = new UriBuilder($"{BaseUri}?param1=value1");

    target.Query = UriQuery.Parse(target.Query).Add("param2", "value2").GetQuery();

    Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param1=value1&param2=value2"));
}

[Test]
public void RemoveSingleValuedParameter()
{
    var target = new UriBuilder($"{BaseUri}?param1=value1&param2=value2");

    target.Query = UriQuery.Parse(target.Query).RemoveAll("param1").GetQuery();

    Assert.That(target.ToString(), Is.EqualTo($"{BaseUri}?param2=value2"));
}
```

See `UriQueryTests.cs` file for other usage samples.
