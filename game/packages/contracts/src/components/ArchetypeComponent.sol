// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { Uint256Component } from "solecs/components/Uint256Component.sol";

uint256 constant ID = uint256(keccak256("components.Reference.Archetype"));

contract ArchetypeComponent is Uint256Component
{
  constructor(address world) Uint256Component(world, ID) {}
}
