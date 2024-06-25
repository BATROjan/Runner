using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Kolya_sGame.Obstacle
{
    public class ObstacleTileView : MonoBehaviour
    {
        public Action<ObstacleTileView> OnBecameInvisible;
        public Action<ObstacleTileView> OnPassedSpawnPoint;
        public PointView[] PointViews => pointViews;
         
        [SerializeField] private PointView[] pointViews;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EndVisibleObstacle"))
            {
                OnBecameInvisible.Invoke(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("TileSpawner"))
            {
                OnPassedSpawnPoint.Invoke(this);
            }
        }
    }
}