// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { Uint16Component } from "components/Uint16Component.sol";
import { LibTypes } from "solecs/LibTypes.sol";

uint256 constant ID = uint256(keccak256("components.MovementCost"));

contract MovementCostComponent is Uint16Component
{
  constructor(address world) Uint16Component(world, ID) {}
}
