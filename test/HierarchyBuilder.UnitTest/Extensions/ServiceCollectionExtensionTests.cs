using FluentAssertions;
using HierarchyBuilder.Abstractions;
using HierarchyBuilder.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace HierarchyBuilder.UnitTest.Extensions;

public class ServiceCollectionExtensionTests
{
    private readonly IServiceProvider _provider;

    public ServiceCollectionExtensionTests()
    {
        var collection = new ServiceCollection();
        collection.AddHierarchyBuilder();
        _provider = collection.BuildServiceProvider();
    }
    
    [Fact]
    public void AddHierarchyBuilder_Should_Resolve_IHierarchyTransformer()
    {
        // Arrange
        // Act
        var transformer = _provider.GetService<IHierarchyTransformer>();
        
        // Assert
        transformer.Should().NotBeNull();
        transformer.Should().BeAssignableTo<Transformer>();
    }
}