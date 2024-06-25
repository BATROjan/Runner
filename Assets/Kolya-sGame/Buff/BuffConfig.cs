using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kolya_sGame.Buff
{
    [CreateAssetMenu(fileName = "BuffConfig", menuName = "Configs/BuffConfig")]

    public class BuffConfig : ScriptableObject
    {
        [SerializeField] private BuffModel[] BuffModel;
        [NonSerialized] private bool _inited;

        private Dictionary<BuffName, BuffModel> _buffModelsDictionary = new Dictionary<BuffName, BuffModel>();
       
        public BuffModel GetBuffModelByName(BuffName name)
        {
            if (!_inited)
            {
                Init();
            }
            
            if (_buffModelsDictionary.ContainsKey(name))
            {
                return _buffModelsDictionary[name];
            }

            Debug.LogError($"There no such world with type: {name}");
            
            return new BuffModel();
        }
        
        private void Init()
        {
            foreach (var model in BuffModel)
            {
                _buffModelsDictionary.Add(model.Name, model);
            }

            _inited = true;
        }
    }

    [Serializable]
    public struct BuffModel
    {
        public BuffName Name;
        public Vector2 Size;
        public Sprite ItemSprite;
        public Sprite StarSprite;
    }

    public enum BuffName
    {
        VRGlasses,
        Exosceleton
    }
}