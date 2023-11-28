using ECS.Systems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class SetupRunner : MonoBehaviour
    {
        [SerializeField] private TileSpawnSystem tileSpawnSystem;
        [SerializeField] private UnitSpawnSystem unitSpawnSystem;

        [Button] [HideInEditorMode]
        private void Run()
        {
            tileSpawnSystem.Execute(1001, 2001, new Vector2Int(0, 0));
            tileSpawnSystem.Execute(1002, 2002, new Vector2Int(0, 1));
            tileSpawnSystem.Execute(1003, 2003, new Vector2Int(1, 0));
            tileSpawnSystem.Execute(1004, 2001, new Vector2Int(1, 1));
            tileSpawnSystem.Execute(1005, 2003, new Vector2Int(2, 0));
            tileSpawnSystem.Execute(1006, 2002, new Vector2Int(2, 1));
        }
    }
}