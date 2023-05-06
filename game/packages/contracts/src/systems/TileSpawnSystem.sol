// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { System, IWorld } from "solecs/System.sol";
import { getAddressById } from "solecs/utils.sol";
import { Position } from "libraries/LibMath.sol";

import { PositionComponent, ID as PositionComponentID } from "components/PositionComponent.sol";
import { TerrainComponent, ID as TerrainComponentID } from "components/TerrainComponent.sol";

uint256 constant ID = uint256(keccak256("system.Spawn.Tile"));

// spawns tiles on the map
contract TileSpawnSystem is System
{
  constructor(IWorld _world, address _components) System(_world, _components) {}

  function execute(bytes memory arguments) public returns (bytes memory) 
  {
    this.executeCall(arguments);
  }

  function executeCall(bytes calldata arguments) public returns (bytes memory) 
  {
    // todo
  }

  function executeTyped(uint256 entity, uint256 terrain, int16 x, int16 y) public returns (bytes memory) 
  {
    PositionComponent pc = PositionComponent(getAddressById(components, PositionComponentID));
    TerrainComponent tc = TerrainComponent(getAddressById(components, TerrainComponentID));

    pc.set(entity, Position(x, y));
    tc.set(entity, terrain);
  }
}
