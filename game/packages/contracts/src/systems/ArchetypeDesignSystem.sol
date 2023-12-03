// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { System, IWorld } from "solecs/System.sol";
import { getAddressById } from "solecs/utils.sol";
import { Position } from "libraries/LibMath.sol";

import { MovementPointsComponent, ID as MovementPointsComponentID } from "components/MovementPointsComponent.sol";

uint256 constant ID = uint256(keccak256("system.Design.Archetype"));

contract ArchetypeDesignSystem is System
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

  function executeTyped(uint256 entity, uint32 movementPoints) public returns (bytes memory) 
  {
    MovementPointsComponent mpc = MovementPointsComponent(getAddressById(components, MovementPointsComponentID));

    mpc.set(entity, movementPoints);
  }
}
