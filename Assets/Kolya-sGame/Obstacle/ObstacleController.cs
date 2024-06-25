using System.Collections.Generic;
using Kolya_sGame.World;
using Unity.VisualScripting;
using UnityEngine;

namespace Kolya_sGame.Obstacle
{
    public class ObstacleController
    {
        private readonly ObstacleAnimationController obstacleAnimationController;
        private readonly ObstacleColliderConfig obstacleColliderConfig;
        private readonly ObstacleConfig _obstacleConfig;
        private readonly ObstacleView.Pool _obstacleController;
        
        private ObstacleView _obstacleView;
        
        public ObstacleController(
            ObstacleAnimationController obstacleAnimationController,
            ObstacleColliderConfig obstacleColliderConfig,
            ObstacleConfig obstacleConfig,
            ObstacleView.Pool obstacleController)
        {
            this.obstacleAnimationController = obstacleAnimationController;
            this.obstacleColliderConfig = obstacleColliderConfig;
            _obstacleConfig = obstacleConfig;
            _obstacleController = obstacleController;
        }

        public ObstacleView Spawn(WorldName name,
            PointPosition pointType, 
            PointView point)
        {
            var obstacleModel = _obstacleConfig.GetRandomObstacleByType(name, pointType);
            _obstacleView = _obstacleController.Spawn(obstacleModel);
            var colliders = obstacleColliderConfig.GetPolygonCollider2D(_obstacleView.ObstacleName);
            _obstacleView.PolygonCollider2DForReality = _obstacleView.SpriteRendererReality.AddComponent<PolygonCollider2D>();
            _obstacleView.PolygonCollider2DForReality.isTrigger = true;

            _obstacleView.PolygonCollider2DForVR = _obstacleView.SpriteRendererVR.AddComponent<PolygonCollider2D>();
            _obstacleView.PolygonCollider2DForVR.isTrigger = true;
            _obstacleView.PolygonCollider2DForVR.enabled = false;

            if (colliders != null)
            {
                Vector2[] tempArray = (Vector2[])colliders[0].points.Clone();
                _obstacleView.PolygonCollider2DForReality.SetPath(0, tempArray);
                tempArray = (Vector2[])colliders[1].points.Clone();
                _obstacleView.PolygonCollider2DForVR.SetPath(0, tempArray);
                
                _obstacleView.Ground.gameObject.SetActive(true);
                _obstacleView.Ground.PolygonCollider2DForGround =  _obstacleView.Ground.AddComponent<PolygonCollider2D>();

                tempArray = (Vector2[])colliders[2].points.Clone();
                _obstacleView.Ground.PolygonCollider2DForGround.SetPath(0, tempArray);
                
                _obstacleView.PolygonCollider2DForVR.isTrigger = false;
            }

            if (_obstacleView.AnimState != AnimState.None)
            {
                obstacleAnimationController.AddObstacleAnimation(_obstacleView);
            }

            _obstacleView.gameObject.transform.SetParent(point.transform, false);
            
            return _obstacleView;
        }

        public void DespawnView(ObstacleView view)
        {
            if (_obstacleView.AnimState != AnimState.None)
            {
                obstacleAnimationController.DeleteAnimation(view.GetInstanceID());
            }
        }
    }
}