// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import "std-contracts/components/Uint32Component.sol";

uint256 constant ID = uint256(keccak256("component.My"));

contract MyComponent is Uint32Component
{
  uint32 public value;

  constructor(address world) Uint32Component(world, ID) 
  {

  }

  function change(uint32 delta) public {
    value += delta;
  }

  function check() public view returns (uint32) {
    return value;
  }
}
