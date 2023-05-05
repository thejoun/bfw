using System.Collections;
using Context;
using Interfaces;
using Nethereum.Unity.Rpc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class BlockNumber : MonoBehaviour
    {
        private static INode Node => EditorContext.Instance.Node;
    
        [Button]
        private void Test()
        {
            StartCoroutine(GetBlockNumber());
        }

        private IEnumerator GetBlockNumber()
        {
            var blockNumberRequest = new EthBlockNumberUnityRequest(Node.Url);
        
            yield return blockNumberRequest.SendRequest();

            var result = blockNumberRequest.Result;

            if (result == null)
            {
                Debug.Log("Result is null");
                yield break;
            }

            var value = result.Value;
            
            Debug.Log($"Result: {value}");
        }
    }
}