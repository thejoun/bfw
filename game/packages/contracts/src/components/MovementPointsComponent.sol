// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import "std-contracts/components/Uint32Component.sol";

uint256 constant ID = uint256(keccak256("component.Points.Movement"));

contract MovementPointsComponent is Uint32Component 
{
  constructor(address world) Uint32Component(world, ID) {}

  function change(uint256 entity, int32 value) public
  {
    uint32 current = getValue(entity);
    set(entity, uint32(int32(current) + value));
  }
}
