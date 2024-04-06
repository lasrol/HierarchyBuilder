using FluentAssertions;
using HierarchyBuilder.UnitTest.FakeModels;

namespace HierarchyBuilder.UnitTest;

public class TransformerTests
{
    [Fact]
    public void Transform_GivenBasicListWithGuidIds_ReturnsListTransformedToAHierarchy()
    {
        //Arrange
        var sut = new Transformer();
        var input = new List<BasicGuidKeyHierarchyModel>
        {
            new() { Id = Guid.Parse("94A42E40-0C76-4599-B05B-CD67F5C66B2C") },
            new() { Id = Guid.NewGuid(), ParentId = Guid.Parse("94A42E40-0C76-4599-B05B-CD67F5C66B2C")}
        };
        
        //Act
        var result = sut.Transform(input, cfg =>
        {
            cfg.UseId(i => i.Id);
            cfg.UseParentId(i => i.ParentId);
            cfg.UseChildren(i => i.Children);
        }).ToList();
        
        result.Should().NotBeEmpty().And.HaveCount(1);
        result.Should().Contain(item => item.Children.Count == 1);
    }
    
    [Fact]
    public void Transform_GivenEmptyList_ReturnsEmptyList()
    {
        //Arrange
        var sut = new Transformer();
        var input = new List<BasicGuidKeyHierarchyModel>();
        
        //Act
        var result = sut.Transform(input, cfg =>
        {
            cfg.UseId(i => i.Id);
            cfg.UseParentId(i => i.ParentId);
        });
        
        //Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void Transform_GivenNull_ThrowArgumentNullException()
    {
        //Arrange
        var sut = new Transformer();
        
        //Act
        Action act = () => sut.Transform((List<BasicGuidKeyHierarchyModel>)null, cfg =>
        {
            cfg.UseId(i => i.Id);
            cfg.UseParentId(i => i.ParentId);
        });
        
        //Assert
        act.Should().Throw<ArgumentNullException>();
    }
}