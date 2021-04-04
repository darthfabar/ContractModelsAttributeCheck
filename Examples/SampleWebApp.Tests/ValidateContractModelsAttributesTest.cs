using ContractModelsAttributeCheck;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SampleWebApp.Contract.V1;
using SampleWebApp.Contract.V2;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebApp.Tests
{
    public class ValidateContractModelsAttributesTest: IClassFixture<WebApplicationFactory<SampleWebApp.Startup>>
    {
        private readonly WebApplicationFactory<SampleWebApp.Startup> _factory;
        private readonly Type[] _attributes = new[] { typeof(JsonPropertyNameAttribute), typeof(JsonIgnoreAttribute) };

        public ValidateContractModelsAttributesTest(WebApplicationFactory<SampleWebApp.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("v1", typeof(PagingParameters))]
        [InlineData("v2", typeof(PagingParametersV2))]
        public void Query_V1_ContractModels(string apiVersion, Type typeNotInList)
        {
            // Arrange
            var apiProvider = _factory.Services.GetService<IApiDescriptionGroupCollectionProvider>();
            var apiInfoForVersion = apiProvider.ApiDescriptionGroups.Items.FirstOrDefault(w => w.GroupName == apiVersion);
            var modelFinder = new ApiDescriptionContractModelsFinder();
            // Act
            var contractTypes = modelFinder.GetUsedContractTypes(apiInfoForVersion, "application/json");

            // Assert
            contractTypes.GetAllTypes().Should().NotContain(typeNotInList);
        }

        [Fact]
        public void V1Models_Dont_Have_Attributes()
        {
            // Arrange
            var apiProvider = _factory.Services.GetService<IApiDescriptionGroupCollectionProvider>();
            var apiInfoForVersion = apiProvider.ApiDescriptionGroups.Items.FirstOrDefault(w => w.GroupName == "v1");
            var modelFinder = new ApiDescriptionContractModelsFinder();
            // Act
            var validationResults = modelFinder.CheckAttributesOfApiContractTypes(apiInfoForVersion, _attributes, "application/json");

            // Assert
            var missingAttributes = validationResults.Where(w => w.HasRequiredAttribute);
            missingAttributes.Should().BeEmpty();
        }

        [Fact]
        public void V2Models_Have_Attributes()
        {
            // Arrange
            var apiProvider = _factory.Services.GetService<IApiDescriptionGroupCollectionProvider>();
            var apiInfoForVersion = apiProvider.ApiDescriptionGroups.Items.FirstOrDefault(w => w.GroupName == "v2");
            var modelFinder = new ApiDescriptionContractModelsFinder();
            // Act
            var validationResults = modelFinder.CheckAttributesOfApiContractTypes(apiInfoForVersion, _attributes, "application/json");

            // Assert
            var missingAttributes = validationResults.Where(w => !w.HasRequiredAttribute);
            missingAttributes.Should().BeEmpty();
        }

    }
}
