using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kolya_sGame.Player
{
    [CreateAssetMenu(fileName = "PlayerColliderConfig", menuName = "Configs/PlayerColliderConfig")]

    public class PlayerColliderConfig: ScriptableObject
    {
        [SerializeField] private PlayerColliderModel[] playerColliderModel;
        [NonSerialized] private bool _inited;

        private Dictionary<PlayerState, Dictionary<ColliderState, PolygonCollider2D>> _playerStateColliders =
            new Dictionary<PlayerState, Dictionary<ColliderState, PolygonCollider2D>>();
        private Dictionary<ColliderState, PolygonCollider2D> _sitColliderModels =
            new Dictionary<ColliderState, PolygonCollider2D>();    
        private Dictionary<ColliderState, PolygonCollider2D> _stateColliderModels =
            new Dictionary<ColliderState, PolygonCollider2D>();
        
        public PolygonCollider2D GetPolygonCollider2D(PlayerState playerState, ColliderState colliderstate)
        {
            if (!_inited)
            {
                Init();
            }
            
            if (_playerStateColliders.ContainsKey(playerState))
            {
                return _playerStateColliders[playerState][colliderstate];
            }
            
            return null;
        }
        
        private void Init()
        {
            foreach (var model in playerColliderModel)
            {
                if (model.PlayerState == PlayerState.Sit)
                {
                    foreach (var state in model.ColliderModel)
                    {
                        _sitColliderModels.Add(state.State, state.PolygonCollider2D);
                    }
                }
                else
                {
                    foreach (var state in model.ColliderModel)
                    {
                        _stateColliderModels.Add(state.State, state.PolygonCollider2D);
                    }
                }
            }
            _playerStateColliders.Add(PlayerState.Sit, _sitColliderModels);
            _playerStateColliders.Add(PlayerState.State, _stateColliderModels);
            
            _inited = true;
        }
    }

    [Serializable]
    public struct PlayerColliderModel
    {
        public PlayerState PlayerState;
        public StateColliderModel[] ColliderModel;
    }
    [Serializable]
    public struct StateColliderModel
    {
         public ColliderState State;
         public PolygonCollider2D PolygonCollider2D;
    }

    public enum PlayerState
    {
      Sit,
      State
    }  
    public enum ColliderState
    {
        Run,
        Duck,
        JumpUp,
        JumpDown,
        Death
    }
}