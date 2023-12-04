namespace ECS.Systems
{
    public class TerrainDesignSystem : EntityCreationSystem<int>
    {
        protected override object[] GetRemoteArguments(int value) => new object[] { value };
    }
}