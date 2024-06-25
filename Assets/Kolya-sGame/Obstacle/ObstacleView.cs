using System;
using Kolya_sGame.Ground;
using UnityEngine;
using Zenject;

namespace Kolya_sGame.Obstacle
{
    public class ObstacleView : MonoBehaviour
    {
        public GroundView Ground;
        
        [HideInInspector] public bool IsSpawnStar;
        [HideInInspector] public float Length;
        [HideInInspector] public float Height;
        [HideInInspector] public float AmountStar;
        [HideInInspector] public int Probability;
        [HideInInspector] public float Offset;
        [HideInInspector] public PolygonCollider2D PolygonCollider2DForReality;
        [HideInInspector] public PolygonCollider2D PolygonCollider2DForVR;
        [HideInInspector] public Vector3[] PositionStar;
        [HideInInspector] public String ObstacleName;
        [HideInInspector] public AnimState AnimState;
        
        public SpriteRenderer SpriteRendererReality => _spriteRendererReality;
        public SpriteRenderer SpriteRendererVR => _spriteRendererVR;
        public SpriteRenderer SpriteVRBack => _spriteVRBack;
        public SpriteRenderer SpriteForAnim => _spriteForAnim;
        
        [SerializeField] private SpriteRenderer _spriteRendererReality;
        [SerializeField] private SpriteRenderer _spriteRendererVR;
        [SerializeField] private SpriteRenderer _spriteVRBack;
        [SerializeField] private SpriteRenderer _spriteForAnim;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<PlayerView>())
            {
                Ground.gameObject.SetActive(false);
            }
        }

        private void ReInit(ObstacleModel obstacleModel)
        {
            _spriteRendererReality.sprite = obstacleModel.SpriteForReality;
            _spriteRendererVR.sprite = obstacleModel.SpriteForVR;
            
            if (obstacleModel.SpriteForVRBack)
            {
                _spriteVRBack.sprite = obstacleModel.SpriteForVRBack;
            }
            if (obstacleModel.AnimState == AnimState.Truck)
            {
                _spriteForAnim.sprite = obstacleModel.SpriteFoAnim;
                AnimState = obstacleModel.AnimState;
            }
            /*else if (obstacleModel.AnimState == AnimState.Drone)
            {
                AnimState = obstacleModel.AnimState;
            }*/
            else
            {
                AnimState = obstacleModel.AnimState;
            }
            
            transform.position = Vector3.zero;
            PositionStar = obstacleModel.PositionStar;
            IsSpawnStar = obstacleModel.IsSpawnStar;
            Length = obstacleModel.Length;
            Height = obstacleModel.Height;
            AmountStar = obstacleModel.AmountStar;
            Probability = obstacleModel.Probability;
            Offset = obstacleModel.Offset;
            ObstacleName = obstacleModel.ObstacleName;
        }
        
        public class Pool : MonoMemoryPool<ObstacleModel, ObstacleView>
        {
            protected override void Reinitialize(ObstacleModel pLayerModel, ObstacleView item)
            {
                item.ReInit(pLayerModel);
            }
        }
    }
}