using Const;
using Core;
using ECS.Components;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(EntityInstaller), 
        menuName = MenuName.Installers + nameof(EntityInstaller))]
    public class EntityInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            // ecs
            Container.Bind<IEntity>().FromComponentSibling();
            
            // misc
            Container.Bind<SpriteRenderer>().FromComponentInChildren().WhenInjectedInto<TerrainComponent>();
            Container.Bind<SpriteRenderer>().FromComponentInChildren().WhenInjectedInto<ArchetypeComponent>();
        }
    }
}