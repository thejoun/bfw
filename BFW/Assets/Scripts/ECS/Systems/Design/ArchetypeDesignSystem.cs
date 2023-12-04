namespace ECS.Systems
{
    public class ArchetypeDesignSystem : EntityCreationSystem<int>
    {
        protected override object[] GetRemoteArguments(int value) => new object[] { value };
    }
}