using System;
using System.Collections.Generic;
using Kolya_sGame.Player;
using UnityEngine;

namespace Kolya_sGame.Obstacle
{
    [CreateAssetMenu(fileName = "ObstacleColliderConfig", menuName = "Configs/ObstacleColliderConfig")]

    public class ObstacleColliderConfig : ScriptableObject
    {
        [SerializeField] private ObstacleColliderModel[] obstacleColliderModels;
        [NonSerialized] private bool _inited;

        private Dictionary<String, PolygonCollider2D[]> _obstacleColliderModels =
            new Dictionary<String, PolygonCollider2D[]>();
        
        public PolygonCollider2D[] GetPolygonCollider2D(String polygonName)
        {
            if (!_inited)
            {
                Init();
            }
            
            if (_obstacleColliderModels.ContainsKey(polygonName))
            {
                return _obstacleColliderModels[polygonName];
            }
            
            return null;
        }
        
        private void Init()
        {
            foreach (var model in obstacleColliderModels)
            {
               _obstacleColliderModels.Add(model.ObstacleName, model.PolygonCollider2D);
            }
            
            _inited = true;
        }
    }
    
    [Serializable]
    public struct ObstacleColliderModel
    {
        public String ObstacleName;
        public PolygonCollider2D[] PolygonCollider2D;
    }
}