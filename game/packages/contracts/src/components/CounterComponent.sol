// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import "std-contracts/components/Uint32Component.sol";

uint256 constant ID = uint256(keccak256("component.Counter"));

contract CounterComponent is Uint32Component 
{
  constructor(address world) Uint32Component(world, ID) 
  {

  }

  function test(uint256 entity) public view virtual returns (string memory) {
    string memory value = "abcd";
    return value;
  }

  function test2(uint256 entity) public virtual returns (uint32) {
    uint32 current = has(entity) ? getValue(entity) : 0;
    set(entity, current + 1);
    uint32 value = getValue(entity);
    return value;
  }
}
