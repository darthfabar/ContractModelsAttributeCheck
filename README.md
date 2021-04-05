# Contract Models Attribute Checks

[![GitHub Actions Status](https://github.com/darthfabar/ContractModelAttributeChecks/workflows/Build/badge.svg?branch=main)](https://github.com/darthfabar/ContractModelAttributeChecks/actions)

[![GitHub Actions Build History](https://buildstats.info/github/chart/darthfabar/ContractModelAttributeChecks?branch=main&includeBuildsFromPullRequest=false)](https://github.com/darthfabar/ContractModelAttributeChecks/actions)

# What does this Package do?
This package enables you to find all used types in an OpenApi contract and check if every DTO has properties that are decorated with attributes like the JsonPropertyNameAttribute. 

## ValidationResults
Each property get's a AttributeCheckResult that looks like this:

```cs
    public class AttributeCheckResult
    {
        /// <summary>
        /// Fullname or type.ToString
        /// </summary>
        public string Fullname { get; } 
        public string PropertyName { get; } 
        /// <summary>
        /// validation message
        /// </summary>
        public string Message { get;} 
        /// <summary>
        /// true if at least one attribute is used
        /// </summary>
        public bool HasRequiredAttribute { get; }
        /// <summary>
        /// Type 
        /// </summary>
        public Type Type { get; } 
```


# Use Case: Validate the types you want
The 'AttributeChecker' first visits types and their property types recursivly and checks afterwards which properties are violating your expected attribute list.
 
```cs
    public class AttributeCheckerTests
    {
        private readonly AttributeChecker _attributeChecker = new AttributeChecker();
        private readonly Type[] _attributes = new[] { typeof(JsonPropertyNameAttribute), typeof(JsonIgnoreAttribute) };

        [Fact]
        public void All_Properties_Should_Have_Attributes()
        {
            var results = _attributeChecker.CheckPropertiesForAttributes(typeof(TestClassWithAttributes), _attributes);
            var typesWithMissingAttributes = results.Where(w => !w.HasRequiredAttribute);
            typesWithMissingAttributes.Should().BeEmpty();
        }

```

# Use Case: Validate OpenApi contract
ASP.NET Core offers an easy way to create integration tests. See also [Link](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0)

With the 'ApiContractModelsAttributeChecker' class you can search for all used response and request types and check if they use the specified attributes on every property.

The code for this is as simply as this:
```cs
 public class ValidateContractModelsAttributesTest: IClassFixture<WebApplicationFactory<SampleWebApp.Startup>>
    {
        private readonly WebApplicationFactory<SampleWebApp.Startup> _factory;
        private readonly Type[] _attributes = new[] { typeof(JsonPropertyNameAttribute), typeof(JsonIgnoreAttribute) };

        public ValidateContractModelsAttributesTest(WebApplicationFactory<SampleWebApp.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void V2Models_Have_Attributes()
        {
            // Arrange
            var apiProvider = _factory.Services.GetService<IApiDescriptionGroupCollectionProvider>();
            var apiInfoForVersion = apiProvider.ApiDescriptionGroups.Items.FirstOrDefault(w => w.GroupName == "v2");
            var modelFinder = new ApiContractModelsAttributeChecker();
            // Act
            var validationResults = modelFinder.CheckAttributesOfApiContractTypes(apiInfoForVersion, _attributes, "application/json");

            // Assert
            var typesWithMissingAttributes = validationResults.Where(w => !w.HasRequiredAttribute);
            typesWithMissingAttributes.Should().BeEmpty();
        }
```
By combining it with FluentAssertions you will get useful messages on failing tests.
