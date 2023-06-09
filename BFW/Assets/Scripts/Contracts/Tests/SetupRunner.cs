﻿using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class SetupRunner : MonoBehaviour
    {
        [SerializeField] private TerrainTest terrain;
        [SerializeField] private TileTest tile;
        [SerializeField] private ArchetypeTest archetype;
        [SerializeField] private UnitTest unit;
        [SerializeField] private MovementTest movement;

        [Button]
        private async void RunSetup()
        {
            await tile.SpawnTile(100, 0, new Vector2Int(0, 0));
            await tile.SpawnTile(101, 0, new Vector2Int(0, 1));
            await tile.SpawnTile(102, 1, new Vector2Int(1, 0));
            await tile.SpawnTile(103, 2, new Vector2Int(1, -1));
            await tile.SpawnTile(104, 0, new Vector2Int(0, -1));
            
            await terrain.CheckTerrain(101);
            await terrain.CheckTerrain(103);
            
            await archetype.CreateArchetype(1000, 
                new Vector2Int(0, 1),
                new Vector2Int(1, 3), 
                new Vector2Int(2, 5));
            
            await unit.SpawnUnit(2000, 1000, new Vector2Int(0, 0));

            await movement.GetPosition(2000);
            await movement.CheckMovementPoints(2000);
            await movement.MoveUnit(2000, HexDirection.Up);
            await movement.GetPosition(2000);
            await movement.CheckMovementPoints(2000);
            await movement.MoveUnit(2000, HexDirection.DownRight);
            await movement.GetPosition(2000);
            await movement.CheckMovementPoints(2000);
            await movement.MoveUnit(2000, HexDirection.Down);
            await movement.GetPosition(2000);
            await movement.CheckMovementPoints(2000);
            await movement.MoveUnit(2000, HexDirection.DownLeft);
            await movement.GetPosition(2000);
            await movement.CheckMovementPoints(2000);
            
        }
    }
}