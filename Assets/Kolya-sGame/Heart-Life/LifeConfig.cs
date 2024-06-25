using UnityEngine;

namespace Kolya_sGame.Heart_Life
{
    [CreateAssetMenu(fileName = "LifeConfig", menuName = "Configs/LifeConfig")]

    public class LifeConfig : ScriptableObject
    {
        [SerializeField] private int lifeCount;


        public int GetLifeCount()
        {
            return lifeCount;
        }
    }
}