// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { System, IWorld } from "solecs/System.sol";
import { getAddressById } from "solecs/utils.sol";
import { PositionComponent, ID as PositionComponentID } from "components/PositionComponent.sol";
import { Position } from "libraries/LibMath.sol";

uint256 constant ID = uint256(keccak256("system.Movement"));

contract MovementSystem is System
{
  constructor(IWorld _world, address _components) System(_world, _components) {}

  // 256 (2^8) moves max

  // implements ISystem, has memory args unfortunately
  function execute(bytes memory arguments) public returns (bytes memory) 
  {
    // "this" keyword allows passing memory to calldata
    this.executeCall(arguments);
  }

  // workaround function with calldata as argument
  function executeCall(bytes calldata arguments) public returns (bytes memory) 
  {
    bytes calldata entityCalldata = arguments[:256];
    uint256 entity = abi.decode(entityCalldata, (uint256));

    uint16 argsLength = (uint16)(arguments.length);
    bytes calldata moveBytes = arguments[256:argsLength];

    uint8[] memory moves;

    for(uint8 i=0; i<moveBytes.length; i++){
      moves[i] = uint8(moveBytes[i]);
    }

    this.executeTyped(entity, moves);
  }

  function executeTyped(uint256 entity, uint8[] calldata moves) public returns (bytes memory) 
  {
    PositionComponent c = PositionComponent(getAddressById(components, PositionComponentID));
    
    (int16 x, int16 y) = c.has(entity) ? c.getValue(entity) : (int16(0), int16(0));

    for(uint8 i=0; i<moves.length; i++)
    {
      uint8 direction = moves[i];

      if (direction == 1){  // N
        y++;
      } else if (direction == 2){ // NE
        x++;
        if(x % 2 == 0){
          y++;
        }
      } else if (direction == 3){ // SE
        x++;
        if(x % 2 == 1){
          y--;
        }
      } else if (direction == 4){ // S
        y--;
      } else if (direction == 5){ // SW
        x--;
        if(x % 2 == 1){
          y--;
        }
      } else if (direction == 6){ // NW
        x--;
        if(x % 2 == 0){
          y++;
        }
      }
    }

    c.set(entity, Position(x, y));
  }
}
