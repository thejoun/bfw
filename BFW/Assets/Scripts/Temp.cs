using System.Collections;
using Nethereum.Unity.Rpc;
using Sirenix.OdinInspector;
using UnityEngine;

public class BlockNumber : MonoBehaviour
{
    [SerializeField] private string nodeUrl;
    
    [Button]
    private void Test()
    {
        StartCoroutine(GetBlockNumber());
    }

    private IEnumerator GetBlockNumber()
    {
        var blockNumberRequest = new EthBlockNumberUnityRequest(nodeUrl);
        
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