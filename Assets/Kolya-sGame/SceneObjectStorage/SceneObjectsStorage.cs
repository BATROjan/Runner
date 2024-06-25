using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kolya_sGame.SceneObjectStorage
{
    public sealed class SceneObjectsStorage : ISceneObjectsStorage
    {
        private Dictionary<Type, SceneObject> _sceneObjectsStorage = new Dictionary<Type, SceneObject>();

        public void Add<T>(SceneObject sceneObject) where T : SceneObject
        {
            var type = typeof(T);

            if (CheckToContainsForAdding(type))
            {
                _sceneObjectsStorage.Add(type, sceneObject);
            }
        }
        
        public T CreateFromResourcesAndAdd<T>(string source, Transform parent = null) where T : SceneObject
        {
            var type = typeof(T);

            if (CheckToContainsForAdding(type))
            {
                var prefab = Resources.Load<T>(source);
                if (prefab != null)
                {
                   var obj = Object.Instantiate(prefab);

                   if (parent != null)
                   {
                       obj.transform.SetParent(parent);
                   }
                   
                   _sceneObjectsStorage.Add(type,obj);
                   
                   return obj;
                }
                
                Debug.LogError($"Prefab from {source} not found!");
            }

            return null;
        }

        public void Destroy<T>() where T : SceneObject
        {
            var type = typeof(T);

            if (CheckToContainsForGetting(type))
            {
                Object.Destroy(_sceneObjectsStorage[type]);
                _sceneObjectsStorage.Remove(type);
            }
        }

        public T Get<T>() where T : SceneObject
        {
            var type = typeof(T);

            if (CheckToContainsForGetting(type))
            {
                return _sceneObjectsStorage[type].GetComponent<T>();
            }

            return null;
        }

        public void Dispose()
        {
            _sceneObjectsStorage.Clear();
            _sceneObjectsStorage = null;
        }
        
        private bool CheckToContainsForAdding(Type type)
        {
            if (!_sceneObjectsStorage.ContainsKey(type))
            {
                return true;
            }
            
            Debug.LogError(type + " has been added!");
            return false;
        }
        
        private bool CheckToContainsForGetting(Type type)
        {
            if (_sceneObjectsStorage.ContainsKey(type))
            {
                return true;
            }
            
            Debug.LogError(type + " was not found!");
            return false;
        }
    }
}