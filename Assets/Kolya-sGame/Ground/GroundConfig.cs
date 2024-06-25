using System;
using Kolya_sGame.World;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kolya_sGame.Ground
{
    [CreateAssetMenu(fileName = "GroundConfig", menuName = "Configs/GroundConfig")]

    public class GroundConfig : ScriptableObject
    {
        [SerializeField] private GroundModel[] groundModels;

        public Vector3 GetPositionByName(WorldName name)
        {
            Vector3 position = Vector3.zero;
            foreach (var model in groundModels)
            {
                if (model.Name == name)
                {
                    position = model.Position;
                }
            }

            return position;
        }
    }
    
    [Serializable]
    public struct GroundModel
    {
        public WorldName Name;
        public Vector3 Position;
    }
}