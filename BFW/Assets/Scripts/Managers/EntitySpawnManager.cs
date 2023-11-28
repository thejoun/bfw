using System;
using System.Collections.Generic;
using System.Linq;
using Const;
using Core;
using Dtos;
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

        // todo change this
        private IEnumerable<IEntity> Entities => entityParent.GetComponentsInChildren<IEntity>();

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
            var entity = Entities.FirstOrDefault(entity => entity.Id == entityId);

            if (entity == null)
            {
                var id = (int)entityId;
                
                entity = instantiator.InstantiateComponentOnNewGameObject<Entity>().WithId(id);

                entity.GameObject.name = $"Entity {entityId}";
                entity.GameObject.transform.SetParent(entityParent);
                
                Debug.Log($"Spawned entity {entityId}");
            }
            
            var entityInstance = entity.GameObject;

            if (!entityInstance.TryGetComponent(componentType, out _))
            {
                using (new Inactive(entityInstance))
                {
                    instantiator.InstantiateComponent(componentType, entityInstance);

                    Debug.Log($"Added component {componentType.Name} to entity {entityId}");
                }
            }
        }
    }
}