using System.Threading.Tasks;
using Extensions;
using Interfaces;
using UnityEngine;

namespace Objects
{
    public class LogEcs : Ecs
    {
        public LogEcs(IMyWeb web) : base(web)
        {
            
        }

        public override async Task<bool> HasAccess(IAddress who, IContract where)
        {
            return await base.HasAccess(who, where).WithCallback(result =>
            {
                Debug.Log(result
                    ? $"'{who.Head()}' has access to '{where.Title}'"
                    : $"'{who.Head()}' does not have access to '{where.Title}'");
            });;
        }
    }
}