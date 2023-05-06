// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { Component } from "solecs/Component.sol";
import { LibTypes } from "solecs/LibTypes.sol";
import { Position } from "libraries/LibMath.sol";

uint256 constant ID = uint256(keccak256("components.Position"));

contract PositionComponent is Component
{
  constructor(address world) Component(world, ID) {}

  function getSchema() public pure override returns (string[] memory keys, LibTypes.SchemaValue[] memory values)
  {
    keys[0] = "x";
    values[0] = LibTypes.SchemaValue.INT16;

    keys[1] = "y";
    values[1] = LibTypes.SchemaValue.INT16;
  }

  function set(uint256 entity, Position calldata value) public
  {
    set(entity, abi.encode(value));
  }

  function getValue(uint256 entity) public view returns (int16 x, int16 y) 
  {
    (int16 x, int16 y) = abi.decode(getRawValue(entity), (int16, int16));
    return (x, y);
  }

  function getEntitiesWithValue(Position calldata value) public view returns (uint256[] memory) 
  {
    return getEntitiesWithValue(abi.encode(value));
  }
}
