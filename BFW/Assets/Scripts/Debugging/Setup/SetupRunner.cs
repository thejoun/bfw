using ECS.Systems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class SetupRunner : MonoBehaviour
    {
        [SerializeField] private TerrainDesignSystem terrainDesignSystem;
        [SerializeField] private ArchetypeDesignSystem archetypeDesignSystem;
        [SerializeField] private TileSpawnSystem tileSpawnSystem;
        [SerializeField] private UnitSpawnSystem unitSpawnSystem;

        private int id;
        
        [Button] [HideInEditorMode]
        private void Run()
        {
            id = 1;
            
            terrainDesignSystem.Execute(id++, 1);
            terrainDesignSystem.Execute(id++, 2);
            terrainDesignSystem.Execute(id++, 3);
            
            archetypeDesignSystem.Execute(id++, 1);
            archetypeDesignSystem.Execute(id++, 2);
            archetypeDesignSystem.Execute(id++, 3);
            
            tileSpawnSystem.Execute(id++, (1, new Vector2Int(0, 0)));
            tileSpawnSystem.Execute(id++, (1, new Vector2Int(0, 1)));
            tileSpawnSystem.Execute(id++, (2, new Vector2Int(1, 0)));
            tileSpawnSystem.Execute(id++, (2, new Vector2Int(1, 1)));
            tileSpawnSystem.Execute(id++, (3, new Vector2Int(2, 0)));
            tileSpawnSystem.Execute(id++, (3, new Vector2Int(2, 1)));
            tileSpawnSystem.Execute(id++, (1, new Vector2Int(-1, 0)));
            tileSpawnSystem.Execute(id++, (1, new Vector2Int(-1, -1)));
            tileSpawnSystem.Execute(id++, (1, new Vector2Int(-1, -2)));
            tileSpawnSystem.Execute(id++, (2, new Vector2Int(-1, -2)));
            tileSpawnSystem.Execute(id++, (2, new Vector2Int(-2, 0)));
            tileSpawnSystem.Execute(id++, (2, new Vector2Int(-2, 1)));
            
            unitSpawnSystem.Execute(id++, (4, new Vector2Int(0, 0)));
            unitSpawnSystem.Execute(id++, (5, new Vector2Int(1, 0)));
            unitSpawnSystem.Execute(id++, (6, new Vector2Int(-1, 0)));
        }
    }
}