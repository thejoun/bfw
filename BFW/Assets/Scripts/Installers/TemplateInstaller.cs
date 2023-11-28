using System;
using System.Collections.Generic;
using Const;
using Core;
using ECS.Components;
using ECS.Systems;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(TemplateInstaller), 
        menuName = MenuName.Installers + nameof(TemplateInstaller))]
    public class TemplateInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameObject unitTemplate;
        [SerializeField] private GameObject tileTemplate;

        [SerializeField] private GameObject terrainTemplate;

        public override void InstallBindings()
        {
            Container.Bind<GameObject>().FromInstance(unitTemplate).WhenInjectedInto<UnitSpawnSystem>();
            Container.Bind<GameObject>().FromInstance(tileTemplate).WhenInjectedInto<TileSpawnSystem>();

            using (var templates = new ComponentTemplateInstaller(Container))
            {
                templates.Bind<TerrainComponent>(terrainTemplate);
            }
        }

        private class ComponentTemplateInstaller : IDisposable
        {
            private DiContainer container;
            private Dictionary<Type, GameObject> dictionary = new();

            public ComponentTemplateInstaller(DiContainer container)
            {
                this.container = container;
            }

            public void Bind<T>(GameObject instance)
            {
                container.Bind<GameObject>().FromInstance(instance).WhenInjectedInto<T>();

                dictionary.Add(typeof(T), instance);
            }

            public void Dispose()
            {
                container
                    .Bind<IDictionary<Type, GameObject>>()
                    .WithId(ID.ComponentTemplateDictionary)
                    .FromInstance(dictionary);
            }
        }
    }
}