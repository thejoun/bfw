// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

contract MyComponent
{
  uint32 public value;

  function change(uint32 delta) public {
    value += delta;
  }

  function check() public view returns (uint32) {
    return value;
  }
}
