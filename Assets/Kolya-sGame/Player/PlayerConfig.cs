using System;
using System.Collections.Generic;
using Kolya_sGame.World;
using UnityEngine;

namespace Kolya_sGame.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private PLayerModel[] pLayerModel;
        [NonSerialized] private bool _inited;
        
        private Dictionary<PlayerType, PLayerModel> _pLayerModels = new Dictionary<PlayerType, PLayerModel>();
        public PLayerModel GetPlayerModelByType(PlayerType type)
        {
            if (!_inited)
            {
                Init();
            }
            
            if (_pLayerModels.ContainsKey(type))
            {
                return _pLayerModels[type];
            }

            Debug.LogError($"There no such world with type: {type}");
            
            return new PLayerModel();
        }
        
        private void Init()
        {
            foreach (var model in pLayerModel)
            {
                _pLayerModels.Add(model.Type, model);
            }

            _inited = true;
        }
    }
}

[Serializable]
public struct PLayerModel
{
    public PlayerType Type;
    public Vector2 Size;
    public Vector3 SpawnPosition;
    public Sprite Sprite;
}

public enum PlayerType
{
    Standart,
    VR
}
