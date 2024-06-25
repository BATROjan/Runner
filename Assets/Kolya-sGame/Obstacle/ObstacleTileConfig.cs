using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kolya_sGame.Obstacle
{
    [CreateAssetMenu(fileName = "ObstacleTileConfig", menuName = "Configs/ObstacleTileConfig")]

    public class ObstacleTileConfig : ScriptableObject
    {
        [SerializeField] private TileModel[] tileModels;
        [NonSerialized] private bool _inited;
        
        private List<TileModel> _startTileModels = new List<TileModel>();
        private List<TileModel> _obstacleTileModels = new List<TileModel>();
        public ObstacleTileView GetObstacleTileView(TileType type, int id)
        {
            if (!_inited)
            {
                Init();
            }

            ObstacleTileView currentTile;
            if (type == TileType.Start)
            {
                currentTile = _startTileModels[0].View;
            }
            else
            {
                currentTile = _obstacleTileModels[id].View;
            }

            return currentTile;
        }

        public int GetObstacleCount()
        {
            return _obstacleTileModels.Count;
        }
        
        private void Init()
        {
            _startTileModels.Clear();
            _obstacleTileModels.Clear();
            
            foreach (var model in tileModels)
            {
                if (model.Type == TileType.Start)
                {
                    _startTileModels.Add(model);
                }
                else
                {
                    _obstacleTileModels.Add(model);
                }
            }
            _inited = true;
        }
    }

    [Serializable]
    public struct TileModel
    {
        public TileType Type;
        public ObstacleTileView View;
    }

    public enum PointPosition
    {
        Top,
        TopWithoutStars,
        Bottom,
        BottomWithoutStars,
        ForBuff,
        TopBig,
        BottomBig,
        Truck
    }

    public enum TileType
    {
        Start,
        Obstacle
    }
}