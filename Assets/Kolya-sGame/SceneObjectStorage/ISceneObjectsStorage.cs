using System;
using UnityEngine;

namespace Kolya_sGame.SceneObjectStorage
{
    public interface ISceneObjectsStorage : IDisposable
    {
        void Add<T>(SceneObject sceneObject) where T : SceneObject;
        T CreateFromResourcesAndAdd<T>(string source, Transform parent = null) where T : SceneObject; 
        void Destroy<T>() where T : SceneObject;
        T Get<T>() where T : SceneObject;
    }
}