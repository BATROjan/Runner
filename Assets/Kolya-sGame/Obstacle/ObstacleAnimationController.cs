using System.Collections.Generic;
using DG.Tweening;
using Kolya_sGame.Buff;
using Kolya_sGame.Camera;
using UnityEngine;
using Zenject;

namespace Kolya_sGame.Obstacle
{
    public class ObstacleAnimationController
    {
        private readonly CameraController _cameraController;
        private Dictionary<int, List<Tween>> animations = new Dictionary<int,List<Tween>>();
        private CameraView _cameraView;
        
        private float _obstacleAnimationDuration = 1f;
        private float _obstacleEndValue = 0.5f;
        private float _starAnimationDuration = 3f;
        
        private Vector3 _rotationValue = new Vector3(0, 0, 360);
        private Tween _cameraTween;
        
        public ObstacleAnimationController(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        public void AddObstacleAnimation(ObstacleView view)
        {
            List<Tween> list = new List<Tween>();
            if (view.AnimState == AnimState.Truck)
            {
               Tween tween = view.SpriteForAnim.DOFade(0, _obstacleAnimationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
               list.Add(tween);
               animations.Add(view.GetInstanceID(), list);
            }
            else if(view.AnimState == AnimState.Drone)
            {
                var value = Random.Range(0.0f, 1f);
                DOVirtual.DelayedCall(value, () =>
                {
                    Tween tweenReal = view.SpriteRendererReality.transform
                        .DOLocalMoveY(_obstacleEndValue, _obstacleAnimationDuration).SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.Linear);
                    Tween tweenVR = view.SpriteRendererVR.transform
                        .DOLocalMoveY(_obstacleEndValue, _obstacleAnimationDuration).SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.Linear);
                    Tween tweenBack = view.SpriteVRBack.transform
                        .DOLocalMoveY(_obstacleEndValue, _obstacleAnimationDuration).SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.Linear);
                    list.Add(tweenReal);
                    list.Add(tweenVR);
                    list.Add(tweenBack);
                    animations.Add(view.GetInstanceID(), list);
                    ;
                });
            }
        }

        public void AddStarAnimation(BuffView view)
        {
            List<Tween> list = new List<Tween>();
            Tween tween = view.StarSpriteRenderer.transform.DORotate(_rotationValue, _starAnimationDuration, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            list.Add(tween);
            animations.Add(view.GetInstanceID(), list);
        }

        public void ShakeCamera()
        {
            if (!_cameraView)
            {
                _cameraView = _cameraController.GetCameraView();
            }

            /*if(_cameraTween.)
            {
                 _cameraTween.Kill();
                 _cameraTween = null;
            }*/
            
            Vector3 cameraPosition = _cameraView.transform.position;
            
            _cameraTween = _cameraView.transform.DOShakePosition(0.5f, 0.5f)
                .OnComplete(() =>
                {
                    _cameraView.transform.position = cameraPosition;
                    _cameraTween.Kill();
                    _cameraTween = null;
                })
                .OnKill(() =>
                {
                    _cameraView.transform.position = cameraPosition;
                    _cameraTween.Kill();
                    _cameraTween = null;
                });
        }

        public void DeleteAnimation(int id)
        {
            if (animations.ContainsKey(id))
            {
                foreach (var tween in animations[id])
                {
                    tween.Kill();
                }
                animations.Remove(id);
            }
        }
    }
}