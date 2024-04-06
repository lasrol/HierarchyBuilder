namespace HierarchyBuilder.UnitTest.FakeModels;

public class BasicGuidKeyHierarchyModel
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    
    public List<BasicGuidKeyHierarchyModel> Children { get; set; }
}