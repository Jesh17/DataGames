using Unity.Entities;
// This is the entity object and data container. All effective data(unique to object) is stored here. 
[GenerateAuthoringComponent]
public struct BlockData : IComponentData{
    public int xGrid;
    public int yGrid;
    public float offset;
    public float xPadding;
    public float yPadding;
    public Entity prefabToSpawn;

}