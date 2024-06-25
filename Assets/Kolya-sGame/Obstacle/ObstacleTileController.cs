using System;
using System.Collections.Generic;
using System.Linq;
using Kolya_sGame.Buff;
using Kolya_sGame.World;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Kolya_sGame.Obstacle
{
    public class ObstacleTileController
    {
        public Action<ObstacleTileView> OnSpawnTile;
        public Action<ObstacleTileView> OnDespawnTile;
        
        public bool NeedVRGlasses;
        public bool NeedExoskelet;
        public List<ObstacleTileView> ObstacleTileViews => _obstacleTileViews;

        private readonly BuffController _buffController;
        private readonly ObstacleController _obstacleController;
        private readonly ObstacleTileConfig _obstacleTileConfig;

        private List<ObstacleTileView> _obstacleTileViews = new List<ObstacleTileView>();
        private Dictionary<int,BuffView> _buffViews = new Dictionary<int, BuffView>();
        private Dictionary<int, ObstacleView> _obstaclViews = new Dictionary<int, ObstacleView>();
        private ObstacleTileView _currentTile;
        
        private bool _isChange;
        private string _worldStringName= String.Empty;
        private WorldName _worldName;
        
        public ObstacleTileController(
            BuffController buffController,
            ObstacleController obstacleController,
            ObstacleTileConfig obstacleTileConfig)
        {
            _buffController = buffController;
            _obstacleController = obstacleController;
            _obstacleTileConfig = obstacleTileConfig;
        }

        public void SpawnObstacleTileView(bool needBuff)
        {
            var currentView = _obstacleTileConfig.GetObstacleTileView(TileType.Obstacle,Random.Range(0,_obstacleTileConfig.GetObstacleCount()));
            _currentTile = Object.Instantiate(currentView);
            if (_worldStringName == WorldName.Cartoon.ToString())
            {
                _currentTile.transform.position = new Vector3(30,0,0);
            }
            else
            {
                _currentTile.transform.position = new Vector3(30,-0.7f,0);
            }
            _currentTile.OnBecameInvisible += DespawnTile;
            _currentTile.OnPassedSpawnPoint += SpawnNewTile;
            var points = _currentTile.PointViews.ToList();
            foreach (var point in points)
            {
                if (point.Type != PointPosition.ForBuff)
                {
                    var obstacleView = _obstacleController.Spawn(_worldName, point.Type, point);
                    point.ObstacleView = obstacleView;
                    _obstaclViews.Add(obstacleView.gameObject.GetInstanceID(), obstacleView);
                }
                
                if (needBuff)
                {
                   if (point.Type == PointPosition.ForBuff)
                   {
                       if (_buffViews.Count == 0 && !NeedExoskelet)
                       {
                           var buff = _buffController.SpawnBuff(BuffName.VRGlasses, point.transform);
                   
                           _buffViews.Add(buff.GetInstanceID(), buff);
                             buff.IsTaken += DespawnBuff;

                       }
                       else if (NeedExoskelet)
                       {
                           var buff = _buffController.SpawnBuff(BuffName.Exosceleton, point.transform); 
                           buff.IsTaken += DespawnBuff;
                           _buffViews.Add(buff.GetInstanceID(), buff);
                               
                       }
                   }
                }
            }
            _obstacleTileViews.Add(_currentTile);
            OnSpawnTile?.Invoke(_currentTile);
        } 
        
        public void SpawnStartTileView(WorldName name)
        {
            var currentView = _obstacleTileConfig.GetObstacleTileView(TileType.Start,0);
            _currentTile = Object.Instantiate(currentView);
            _currentTile.transform.position = new Vector3(0,0,0);
            
            _currentTile.OnBecameInvisible += DespawnTile;
            _currentTile.OnPassedSpawnPoint += SpawnNewTile;
            _obstacleTileViews.Add(_currentTile);
            NeedVRGlasses = true;
            NeedExoskelet = false;
            ChangeWorldName(name);
        }

        private void ChangeWorldName(WorldName name)
        {
            _worldStringName = name.ToString();
            _worldName = name;
        }

        public void DespawnAllTile()
        {
            foreach (var tileView in _obstacleTileViews)
            {
                OnDespawnTile.Invoke(tileView);
                ClearTile(tileView);
                Object.Destroy(tileView.gameObject);
                tileView.OnPassedSpawnPoint -= SpawnNewTile;
                tileView.OnBecameInvisible -= DespawnTile;
            }
            
            _obstacleTileViews.Clear();
            _buffViews.Clear();
            _isChange = false;
        }

        public void DespawnBuff(BuffView view)
        {
            _buffViews[view.GetInstanceID()].IsTaken -= DespawnBuff;
            _buffController.DespawnBuff(_buffViews[view.GetInstanceID()]);
            _buffViews.Remove(view.GetInstanceID());
        }

        public void RemoveAllBuffWithName(BuffName buffName)
        {
            foreach (var tile in _obstacleTileViews)
            {
                foreach (var point in tile.PointViews)
                {
                    if (point.Type == PointPosition.ForBuff)
                    {
                        var buffView = point.GetComponentInChildren<BuffView>();
                        if (buffView && buffView.BuffName == buffName)
                        {
                            _buffViews.Remove(buffView.GetInstanceID());
                            Object.Destroy(point.transform.GetChild(0).gameObject);
                        }
                        
                    }
                }
            }
        }

        public void ChangePolygonCollider()
        {
            foreach (var obstacleView in _obstaclViews.Values)
            {
                obstacleView.PolygonCollider2DForReality.enabled = _isChange;
                obstacleView.PolygonCollider2DForVR.enabled = !_isChange;
            }

            _isChange = !_isChange;
        }
        
        private void SpawnNewTile(ObstacleTileView tileView)
        {
             if (NeedVRGlasses || NeedExoskelet)
            {
                SpawnObstacleTileView(GetPropertyToBuff());
            }
            else
            {
                SpawnObstacleTileView(false);
            }
             
            _obstacleTileViews[_obstacleTileViews.IndexOf(tileView)].OnPassedSpawnPoint -= SpawnNewTile;
        }

        private bool GetPropertyToBuff()
        {
            var property = Random.Range(0, 2);
            bool needBuff = property <= 1;

            return needBuff;
        }

        private void DespawnTile(ObstacleTileView tileView)
        {
            OnDespawnTile.Invoke(tileView);
            ClearTile(tileView);
            _obstacleTileViews.Remove(tileView);
            Object.Destroy(tileView.gameObject);
            
            tileView.OnBecameInvisible -= DespawnTile;
            
        }

        private void ClearTile(ObstacleTileView obstacleTileView)
        {
            foreach (var point in obstacleTileView.PointViews)
            {
                if (point.transform.childCount>0)
                {
                    var buff = point.GetComponentInChildren<BuffView>();
                    if (buff)
                    {
                        _buffViews.Remove(buff.GetInstanceID());
                    }
                    Object.Destroy(point.transform.GetChild(0).gameObject);
                    var obstacle = point.transform.GetChild(0).gameObject;
                    if (_obstaclViews.ContainsKey(obstacle.GetInstanceID()))
                    {
                        _obstacleController.DespawnView(_obstaclViews[obstacle.GetInstanceID()]);
                        _obstaclViews.Remove(obstacle.GetInstanceID());
                    }
                    Object.Destroy(obstacle);
                }
            }
        }
    }
}