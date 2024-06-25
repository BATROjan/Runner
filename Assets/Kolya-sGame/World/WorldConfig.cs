using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

namespace Kolya_sGame.World
{
    [CreateAssetMenu(fileName = "WorldConfig", menuName = "Configs/WorldConfig")]
    public class WorldConfig : ScriptableObject
    {
        public WorldModel[] WorldModels => worldModels;
        [SerializeField] private WorldModel[] worldModels;

        public WorldModel GetModelByName(WorldName name)
        {
            WorldModel worldModel = new WorldModel();
            foreach (var model in worldModels)
            {
                if (model.Name == name)
                {
                    worldModel = model;
                }
            }
            return worldModel;
        }
    }

    [Serializable]
    public struct WorldModel
    {
        public WorldName Name;
        public LayerWorld[] LayersWorld;

        public LayerWorld GetLayerWorldByNumber(int layer)
        {
            foreach (var layerWorld in LayersWorld)
            {
                if (layerWorld.NumberLayer == layer)
                {
                    return layerWorld;
                }
            }
            
            Debug.LogError($"There no such layer with number: {layer}");
            
            return new LayerWorld();
        }
    }
    
    [Serializable]
    public struct LayerWorld
    {
        public int NumberLayer;
        public float SpeedMove;
        public Sprite[] SpritesForReality;
        public Sprite[] SpritesForVR;
        public int SortingOrderReality;
        public int SortingOrderVr;
        
        [Header("For Banner")]
        public BannerModel[] BannerModels;
    } 
    
    [Serializable]
    public struct BannerModel
    {
        public BannerName BannerName;
        public VideoClip Clip;
        public Vector3 BannerPosition;
        public int SortingOrder;
    }

    public enum WorldName
    {
        Cartoon,
        Real
    }
    public enum BannerName
    {
        VRGO
    }
}


