// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { BareComponent } from "solecs/BareComponent.sol";
import { LibTypes } from "solecs/LibTypes.sol";

uint256 constant ID = uint256(keccak256("components.TerrainMovementCost"));

contract TerrainMovementCostComponent is BareComponent
{
  mapping(uint256 => mapping(uint256 => uint16)) internal values;

  constructor(address world) BareComponent(world, ID) {}

  function getSchema() public pure override returns (string[] memory keys, LibTypes.SchemaValue[] memory values)
  {
    keys[0] = "value";
    values[0] = LibTypes.SchemaValue.UINT256_ARRAY;
  }

  function set(uint256 entity, uint256 terrain, uint16 value) public virtual
  {
    values[entity][terrain] = value;
  }

  function getValue(uint256 entity, uint256 terrain) public view virtual returns (uint16)
  {
    return values[entity][terrain];
  }
}
