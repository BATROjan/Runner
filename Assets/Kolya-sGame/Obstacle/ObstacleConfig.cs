using System;
using System.Collections.Generic;
using Kolya_sGame.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kolya_sGame.Obstacle
{
    [CreateAssetMenu(fileName = "ObstacleConfig", menuName = "Configs/ObstacleConfig")]

    public class ObstacleConfig : ScriptableObject
    {
        public ObstacleModelByWorld[] ObstacleModelsByWorld;
        
        [NonSerialized] private bool _inited;
        private WorldName currentWorldName;

        private Dictionary<PointPosition, List<ObstacleModel>> _obstacleModelsDictionary = 
            new Dictionary<PointPosition, List<ObstacleModel>>();
        private Dictionary<WorldName, Dictionary<PointPosition, List<ObstacleModel>>> _obstacleModelsByWorldDictionary = 
            new Dictionary<WorldName, Dictionary<PointPosition, List<ObstacleModel>>>();
        
        public ObstacleModel GetRandomObstacleByType(WorldName name, PointPosition type)
        {
            if (!_inited)
            {
                Init();
                _obstacleModelsDictionary = _obstacleModelsByWorldDictionary[name];
            }

            if (currentWorldName != name)
            {
                currentWorldName = name;
                _obstacleModelsDictionary = _obstacleModelsByWorldDictionary[name];
            }
            
            if (_obstacleModelsDictionary.ContainsKey(type))
            {
                if (_obstacleModelsDictionary[type].Count != 0)
                {
                    return _obstacleModelsDictionary[type][Random.Range(0, _obstacleModelsDictionary[type].Count)];
                }
                Debug.LogError($"There no such obstacle with type: {type}");
                return new ObstacleModel();
            }

            Debug.LogError($"There no such obstacle list with type: {type}");
            
            return new ObstacleModel();
        }
        
        private void Init()
        {
            foreach (var modelByWorld in ObstacleModelsByWorld)
            {
                List<ObstacleModel> topList = new List<ObstacleModel>();
                List<ObstacleModel> bottomList = new List<ObstacleModel>();
                List<ObstacleModel> topListWhitoutStars = new List<ObstacleModel>();
                List<ObstacleModel> topBigListWhitoutStars = new List<ObstacleModel>();
                List<ObstacleModel> bottomListWhitoutStars = new List<ObstacleModel>();
                List<ObstacleModel> bottomBigListWhitoutStars = new List<ObstacleModel>();
                List<ObstacleModel> truck = new List<ObstacleModel>();
                
                Dictionary<PointPosition, List<ObstacleModel>> obstacleModelsDictionary = 
                    new Dictionary<PointPosition, List<ObstacleModel>>();
                
                foreach (var model in modelByWorld.ObstacleModel)
                {
                    if (model.ObstaclePosition == PointPosition.Top)
                    {
                        topList.Add(model);
                    }

                    if (model.ObstaclePosition == PointPosition.TopWithoutStars)
                    {
                        topListWhitoutStars.Add(model);
                    }

                    if (model.ObstaclePosition == PointPosition.TopBig)
                    {
                        topBigListWhitoutStars.Add(model);
                    }

                    if (model.ObstaclePosition == PointPosition.Bottom)
                    {
                        bottomList.Add(model);
                    }

                    if (model.ObstaclePosition == PointPosition.BottomWithoutStars)
                    {
                        bottomListWhitoutStars.Add(model);
                    }

                    if (model.ObstaclePosition == PointPosition.BottomBig)
                    {
                        bottomBigListWhitoutStars.Add(model);
                    }

                    if (model.ObstaclePosition == PointPosition.Truck)
                    {
                        truck.Add(model);
                    }
                }

                obstacleModelsDictionary.Add(PointPosition.Top, topList);
                obstacleModelsDictionary.Add(PointPosition.Bottom, bottomList);
                obstacleModelsDictionary.Add(PointPosition.TopBig, topBigListWhitoutStars);
                obstacleModelsDictionary.Add(PointPosition.TopWithoutStars, topListWhitoutStars);
                obstacleModelsDictionary.Add(PointPosition.BottomWithoutStars, bottomListWhitoutStars);
                obstacleModelsDictionary.Add(PointPosition.BottomBig, bottomBigListWhitoutStars);
                obstacleModelsDictionary.Add(PointPosition.Truck, truck);
                
                _obstacleModelsByWorldDictionary.Add(modelByWorld.WorldName, obstacleModelsDictionary);
            }

            _inited = true;
        }
    }
    
    [Serializable]
    public struct ObstacleModelByWorld
    {
        public WorldName WorldName;
        public ObstacleModel[] ObstacleModel;
    }
    
    [Serializable]
    public struct ObstacleModel
    {
        public PointPosition ObstaclePosition;
        public String ObstacleName;
        public Sprite SpriteForReality;
        public Sprite SpriteForVR;
        public Sprite SpriteForVRBack;
        
        [Header("Parameters for Star")] 
        public bool IsSpawnStar;
        public float Length;
        public float Height;
        public float AmountStar;
        public int Probability;
        public float Offset;
        public Vector3[] PositionStar;
        
        [Header("Parameters for Star")] 
        public AnimState AnimState;
        public Sprite SpriteFoAnim;
    }

    public enum AnimState
    {
        Truck,
        Drone,
        None
    }
}