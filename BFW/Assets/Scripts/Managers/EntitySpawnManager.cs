using System;
using System.Collections.Generic;
using Const;
using Dtos;
using ECS.Core;
using ECS.Entities;
using Interfaces;
using UnityEngine;
using Utilities;
using Zenject;

namespace Managers
{
    public class EntitySpawnManager : MonoBehaviour
    {
        [Inject] private IInstantiator instantiator;
        
        [Inject(Id = ID.ComponentValueSetEvent)] private IListenable<ComponentValueSetPayload> valueSetEvent;
        [Inject(Id = ID.EntityParentTransform)] private Transform entityParent;
        [Inject(Id = ID.AddressComponentDictionary)] private IDictionary<string, Type> components;
        [Inject(Id = ID.ComponentTemplateDictionary)] private IDictionary<Type, GameObject> templates;
        [Inject(Id = ID.EntityRegistry)] private IRegistry<IEntity> entities;

        private void OnEnable()
        {
            valueSetEvent.Register(this, OnValueChanged);
        }

        private void OnDisable()
        {
            valueSetEvent.Unregister(this);
        }
        
        private void OnValueChanged(ComponentValueSetPayload payload)
        {
            var address = payload.ComponentAddress;

            if (!components.TryGetValue(address, out var componentType))
            {
                return;
            }

            var entityId = payload.Entity;
            var id = (int)entityId;
            
            var entity = entities.Values.FirstWithKey(id);

            if (entity != null && !entity.GameObject)
            {
                entities.Remove(entity);
                entity = null;
            }
            
            entity ??= entities.Values.FirstWithKey(id);

            if (entity == null)
            {
                entity = instantiator.InstantiateComponentOnNewGameObject<Entity>().WithId(id);

                entity.GameObject.name = $"Entity {entityId}\t";
                entity.GameObject.transform.SetParent(entityParent);

                entities.Add(entity);

                // Debug.Log($"Spawned entity {entityId}");
            }
            
            var entityInstance = entity.GameObject;

            if (!entityInstance.TryGetComponent(componentType, out _))
            {
                using (new Inactive(entityInstance))
                {
                    var component = instantiator.InstantiateComponent(componentType, entityInstance);

                    if (component is EntityComponent entityComponent)
                    {
                        entityInstance.name += $" {entityComponent.ComponentName}";
                    }

                    // Debug.Log($"Added component {componentType.Name} to entity {entityId}");
                }
            }
        }
    }
}