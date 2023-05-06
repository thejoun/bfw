// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;

import { System, IWorld } from "solecs/System.sol";
import { getAddressById } from "solecs/utils.sol";
import { Position } from "libraries/LibMath.sol";

import { PositionComponent, ID as PositionComponentID } from "components/PositionComponent.sol";
import { ArchetypeComponent, ID as ArchetypeComponentID } from "components/ArchetypeComponent.sol";
import { TerrainMovementCostComponent, ID as TerrainMovementCostComponentID } from "components/TerrainMovementCostComponent.sol";
import { TerrainComponent, ID as TerrainComponentID } from "components/TerrainComponent.sol";
import { MovementPointsComponent, ID as MovementPointsComponentID } from "components/MovementPointsComponent.sol";

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

    for (uint8 i = 0; i < moveBytes.length; i++)
    {
      moves[i] = uint8(moveBytes[i]);
    }

    this.executeTyped(entity, moves);
  }

  function executeTyped(uint256 entity, uint8[] calldata moves) public returns (bytes memory)
  {
    PositionComponent pc = PositionComponent(getAddressById(components, PositionComponentID));
    TerrainComponent tc = TerrainComponent(getAddressById(components, TerrainComponentID));
    ArchetypeComponent arc = ArchetypeComponent(getAddressById(components, ArchetypeComponentID));

    TerrainMovementCostComponent tmcc = TerrainMovementCostComponent(
      getAddressById(components, TerrainMovementCostComponentID)
    );

    MovementPointsComponent mpc = MovementPointsComponent(
      getAddressById(components, MovementPointsComponentID)
    );

    uint256 archetypeEntity = arc.getValue(entity);

    uint24 cost = 0;

    (int16 x, int16 y) = pc.has(entity) ? pc.getValue(entity) : (int16(0), int16(0));

    for (uint8 i = 0; i < moves.length; i++)
    {
      uint8 direction = moves[i];

      (int8 deltaX, int8 deltaY) = getMove(direction);

      x += deltaX;
      y += deltaY;

      uint256[] memory entities = pc.getEntitiesWithValue(Position(x, y));
      uint16 terrainCost = 1;

      for (uint8 i = 0; i < entities.length; i++)
      {
        if (tc.has(entities[i]))
        {
          uint256 terrain = tc.getValue(entities[i]);
          terrainCost = tmcc.getValue(archetypeEntity, terrain);
          break;
        }
      }

      cost += terrainCost;
    }

    // temporary, just for debug. should subtract instead
    if (mpc.has(entity)){
      mpc.change(entity, int32(uint32(cost)));
    }

    pc.set(entity, Position(x, y));
  }

  function getMove(uint8 direction) internal returns (int8 x, int8 y)
  {
    if (direction == 1) {
      return (0, 1);
    } else if (direction == 2) {
      if (x % 2 == 0) {
        return (1, 1);
      } else {
        return (1, 0);
      }
    } else if (direction == 3) {
      if (x % 2 == 1) {
        return (1, -1);
      } else{
        return (1, 0);
      }
    } else if (direction == 4) {
      return (0, -1);
    } else if (direction == 5) {
      if (x % 2 == 1) {
        return (-1, -1);
      } else {
        return (-1, 0);
      }
    } else if (direction == 6) {
      if (x % 2 == 0) {
        return (-1, 1);
      } else {
        return (-1, 0);
      }
    }
  }
}
