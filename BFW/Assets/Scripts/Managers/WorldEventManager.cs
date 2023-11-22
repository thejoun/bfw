using System.Collections;
using System.Linq;
using Core;
using Dtos;
using Extensions;
using Interfaces;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Unity.Rpc;
using UnityEngine;
using Utils;
using Zenject;
using Event = Nethereum.Contracts.Event;

namespace Managers
{
    public class WorldEventManager : MonoBehaviour
    {
        [Inject] private IEcsWeb web;
        [Inject] private IContract worldContract;
        
        [Inject(Id = ConfigID.EventFetchTimeInterval)] private float fetchInterval;
        
        [Inject(Id = EventID.EntityComponentValueSet)] 
        private IFilteredRaisable<byte[], ComponentValueSetEventDto> valueSetEvent;

        private Timer timer;
        private Event eventLog;
        private HexBigInteger lastBlockNumber = new(0);
        private bool isFetching;

        private void Start()
        {
            Initialize();
            StartTimer();
        }

        private void Initialize()
        {
            var instance = worldContract.GetInstance(web);
            
            eventLog = instance.GetEvent("ComponentValueSet");
        }

        private void StartTimer()
        {
            timer = Timer.RunNew().WithInterval(fetchInterval).WithCallback(Refresh);
        }

        private void Refresh()
        {
            if (isFetching) return;
            
            isFetching = true;
            
            StartCoroutine(RefreshCoroutine());
        }

        private IEnumerator RefreshCoroutine()
        {
            var blockNumberRequest = new EthBlockNumberUnityRequest(web.Node.Url);
            
            yield return blockNumberRequest.SendRequest();

            var currentBlockNumber = blockNumberRequest.Result;
            
            DoRefresh(currentBlockNumber);
        }

        private async void DoRefresh(HexBigInteger currentBlockNumber)
        {
            var from = new BlockParameter(lastBlockNumber);
            var to = new BlockParameter(currentBlockNumber);
            
            var filter = await eventLog.CreateFilterBlockRangeAsync(from, to);
            var eventLogList = await eventLog.GetAllChangesAsync<ComponentValueSetEventDto>(filter);
            
            var count = eventLogList.Count;

            if (count > 0)
            {
                var blockRangeText = $"from {lastBlockNumber} to {currentBlockNumber}";
                
                var logTexts = eventLogList.Select(log => log.Event.ToString());
                var logsText = string.Join("\n", logTexts);
            
                Debug.Log($"{count} new event logs!\n{blockRangeText}\n\n{logsText}\n");

                foreach (var log in eventLogList)
                {
                    var data = log.Event;
                    
                    valueSetEvent.Raise(data.Data, data);
                }
            }
            else
            {
                Debug.Log("silence");
            }

            lastBlockNumber = new HexBigInteger(currentBlockNumber.Value + 1);
            isFetching = false;
        }
    }
}