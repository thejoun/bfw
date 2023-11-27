using Core;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform entityParent;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(ID.EntityParentTransform).FromInstance(entityParent);
        }
    }
}