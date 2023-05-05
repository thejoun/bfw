// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import { System, IWorld } from "solecs/System.sol";
import { getAddressById } from "solecs/utils.sol";
import { CounterComponent, ID as CounterComponentID } from "components/CounterComponent.sol";
import { LibMath } from "libraries/LibMath.sol";

uint256 constant ID = uint256(keccak256("system.Increment"));

contract IncrementSystem is System
{
  string public message;

  constructor(IWorld _world, address _components) System(_world, _components) 
  {

  }

  function execute(bytes memory arguments) public returns (bytes memory) 
  {
    uint256 entity = abi.decode(arguments, (uint256));
    CounterComponent c = CounterComponent(getAddressById(components, CounterComponentID));
    LibMath.increment(c, entity);
  }

  function executeTyped(uint256 entity) public returns (bytes memory) 
  {
    return execute(abi.encode(entity));
  }

  function executeMy(uint256 entity) public
  {
    CounterComponent c = CounterComponent(getAddressById(components, CounterComponentID));
    uint32 current = c.has(entity) ? c.getValue(entity) : 0;
    c.set(entity, current + 1);
  }

  function executeError(uint256 entity, address component) public
  {
    message = "start";
    CounterComponent c = CounterComponent(component);
    message = "step 1";
    uint32 current = c.has(entity) ? c.getValue(entity) : 0;
    // c.set(entity, current + 1);
    message = "step 2";

    try c.set(entity, current + 1) {
      message = "success";
    } catch Error(string memory reason) {
      message = string.concat("error: ", reason);
    }
  }

  function setNumber(uint256 entity, address component, uint32 number) public
  {
    CounterComponent c = CounterComponent(component);
    c.set(entity, number);

    // try c.set(entity, number) {
    //   message = "success";
    // } catch Error(string memory reason) {
    //   message = string.concat("error: ", reason);
    // } catch Panic(uint errorCode) {
    //   message = "panic";
    // } catch (bytes memory lowLevelData) {
    //   message = "low";
    // }
  }

  function getMessage() public view returns (string memory) 
  {
    return message;
  }
}
