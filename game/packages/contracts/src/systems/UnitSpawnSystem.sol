// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { System, IWorld } from "solecs/System.sol";
import { getAddressById } from "solecs/utils.sol";
import { Position } from "libraries/LibMath.sol";

import { ArchetypeComponent, ID as ArchetypeComponentID } from "components/ArchetypeComponent.sol";
import { PositionComponent, ID as PositionComponentID } from "components/PositionComponent.sol";
import { MovementPointsComponent, ID as MovementPointsComponentID } from "components/MovementPointsComponent.sol";

uint256 constant ID = uint256(keccak256("system.Spawn.Unit"));

// spawns units based on a specified archetype
contract UnitSpawnSystem is System
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

  function executeTyped(uint256 entity, uint256 archetype, int8 x, int8 y) public returns (bytes memory) 
  {
    ArchetypeComponent ac = ArchetypeComponent(getAddressById(components, ArchetypeComponentID));
    PositionComponent pc = PositionComponent(getAddressById(components, PositionComponentID));
    MovementPointsComponent mpc = MovementPointsComponent(getAddressById(components, MovementPointsComponentID));

    ac.set(entity, archetype);
    pc.set(entity, Position(x, y));
    
    mpc.set(entity, uint32(100));

    // todo set health and other starting stats
    // or maybe in other, reactive systems? or in components?
  }
}
