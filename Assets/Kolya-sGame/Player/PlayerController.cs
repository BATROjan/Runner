using System;
using DG.Tweening;
using Kolya_sGame.Buff;
using Kolya_sGame.Heart_Life;
using Kolya_sGame.Obstacle;
using Kolya_sGame.Timer;
using Unity.Mathematics;
using UnityEngine;

namespace Kolya_sGame.Player
{
    public class PlayerController
    {
        public Action OnEndGame;

        public Action ReturnToReality;
        public Action TakeExoskeleton;

        private readonly ObstacleAnimationController obstacleAnimationController;
        private readonly PlayerColliderConfig _playerColliderConfig;
        private readonly TimerController _timerController;
        private readonly LifeController _lifeController;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerView.Pool _playerPool;
        private readonly ParametersConfig _parametersConfig;

        private PLayerModel currentModel;
        private PlayerState _playerState;
        private PlayerView _playerView;
        private Animator _currentAnimator;
        private bool isExoskeleton;

        private Tween _tweenForPlayerSit;
        private Tween _tweenForPlayerStand;
        private Tween _tweenForPlayerWheelBig;

        public PlayerController(
            ObstacleAnimationController obstacleAnimationController,
            PlayerColliderConfig playerColliderConfig,
            TimerController timerController,
            LifeController lifeController,
            PlayerConfig playerConfig,
            PlayerView.Pool playerPool,
            ParametersConfig parametersConfig)
        {
            this.obstacleAnimationController = obstacleAnimationController;
            _playerColliderConfig = playerColliderConfig;
            _timerController = timerController;
            _lifeController = lifeController;
            _playerConfig = playerConfig;
            _playerPool = playerPool;
            _parametersConfig = parametersConfig;
        }

        public PlayerView GetPlayerView()
        {
            return _playerView;
        }

        public PlayerState GetPlayerState()
        {
            return _playerState;
        }

        public void SpawnPlayer()
        {
            if (_playerView)
            {
                return;
            }

            currentModel = _playerConfig.GetPlayerModelByType(PlayerType.Standart);
            var player = _playerPool.Spawn(currentModel);
            player.OnTakeExoskeleton += GetExoskeleton;
            player.OnTakeVRGlass += GetVRGlasses;
            player.OnDuck += DuckingAnimation;
            _lifeController.SetDefaulLifeCount();

            _playerView = player;
            _playerState = PlayerState.Sit;
            isExoskeleton = false;
            SetDefaultSprite(0);
            _currentAnimator = _playerView.PlayerAnimator[0];
            _playerView.OnTriggerObstacle += OnTriggerObstacleEvent;
        }

        public void RevertWheelAnimation()
        {
            _tweenForPlayerWheelBig.Kill();
            _tweenForPlayerWheelBig = null;
            _playerView.PlayerAnimator[3].SetBool("IsRotate", false) ;
            _playerView.PlayerAnimator[4].SetBool("IsRotate", false) ;
            _playerView.PlayerAnimator[5].SetBool("IsRotate", false) ;
        }

        public void ChangePLayerSprite(bool isRealWorld)
        {
            if (isRealWorld)
            {
                _playerView.PlayerStates[2].gameObject.SetActive(false);
                _playerView.PlayerStates[1].gameObject.SetActive(false);
                _playerView.PlayerStates[0].gameObject.SetActive(true);
                _currentAnimator = _playerView.PlayerAnimator[0];
                _currentAnimator.SetBool("IsRunning", true);
                _playerState = PlayerState.Sit;
            }
            else
            {
                _playerView.PlayerStates[0].gameObject.SetActive(false);
                _playerView.PlayerStates[1].gameObject.SetActive(true);
                _currentAnimator = _playerView.PlayerAnimator[1];
                _playerState = PlayerState.State;
            }
            ChangeWheel(isRealWorld);
        }

        public void PlayerAnimationState()
        {
            _currentAnimator.SetBool("IsRunning", true);
            ChangeWheel(true);
            StartTween();
            ChangeCollider(ColliderState.Run);
        }

        public void ChangeCollider(ColliderState state)
        {
            PolygonCollider2D next = _playerColliderConfig.GetPolygonCollider2D(_playerState, state);
            Vector2[] tempArray = (Vector2[])next.points.Clone();
            _playerView.PolygonCollider2D.SetPath(0, tempArray);
        }
        
        public Animator GetCurrentAnimator()
        {
            return _currentAnimator;
        }

        public void DespawnPlayer()
        {
            StopTween();
            ChangeWheel(false);
            _playerView.OnTriggerObstacle -= OnTriggerObstacleEvent;
            _playerView.OnTakeExoskeleton -= GetExoskeleton;
            _playerView.OnTakeVRGlass -= GetVRGlasses;
            _lifeController.UnSubscribeAction();

            _playerPool.Despawn(_playerView);
            _playerView = null;
        }

        public int GetPlayerLifeCount()
        {
            return _lifeController.GetCurrentLifeCount();
        }

        private void StartTween()
        {
            _tweenForPlayerSit = DOTween.To(OnRiseSpeedPlayerSit, _parametersConfig.StartSpeedPlayerSit,
                _parametersConfig.EndSpeedPlayerSit, _parametersConfig.DurationSpeedPlayerSit);
            
            _tweenForPlayerStand = DOTween.To(OnRiseSpeedPlayerStand, _parametersConfig.StartSpeedPlayerStand,
                _parametersConfig.EndSpeedPlayerStand, _parametersConfig.DurationSpeedPlayerStand);
            
            _tweenForPlayerWheelBig = DOTween.To(OnRiseSpeedWheelBig, _parametersConfig.StartSpeedWheel,
                _parametersConfig.EndSpeedWheel, _parametersConfig.DurationSpeedWheel);
        }

        private void StopTween()
        {
            _tweenForPlayerSit.Kill();
            _tweenForPlayerSit = null;
            
            _tweenForPlayerStand.Kill();
            _tweenForPlayerStand = null;
            
            _tweenForPlayerWheelBig.Kill();
            _tweenForPlayerWheelBig = null;
        }
        
        private void OnRiseSpeedPlayerSit(float value)
        {
            _playerView.PlayerAnimator[0].speed = value;
        }
        
        private void OnRiseSpeedPlayerStand(float value)
        {
            _playerView.PlayerAnimator[1].speed = value;
        }
        
        private void OnRiseSpeedWheelBig(float value)
        {
            _playerView.PlayerAnimator[3].speed = value;
            _playerView.PlayerAnimator[4].speed = value;
            _playerView.PlayerAnimator[5].speed = value;
        }

        private void SetDefaultSprite(int id)
        {
            for (int i = 0; i < _playerView.PlayerStates.Length; i++)
            {
                if (i == id)
                {
                    _playerView.PlayerStates[i].gameObject.SetActive(true);
                }
                else
                {
                    _playerView.PlayerStates[i].gameObject.SetActive(false);
                }
            }
            ChangeLibrary(false);
        }

        private void DuckingAnimation()
        {
            _playerView.PlayerStates[1].gameObject.SetActive(false);
            _playerView.PlayerStates[2].gameObject.SetActive(true);
        }

        private void GetVRGlasses()
        {
            _lifeController.PlusLife();
            _timerController.CreatNewTimer(TimerType.TimerVRWorld);
        }

        private void GetExoskeleton(BuffView buffView)
        {
            if (!isExoskeleton)
            {
                _lifeController.PlusLife();
                _timerController.ExoskeletonTimerIsOver += ChangeIsExoskelet;
                ChangeLibrary(true);
            }
            _timerController.CreatNewTimer(TimerType.TimerExsosceleton);
            isExoskeleton = true;
            TakeExoskeleton?.Invoke();
        }

        private void ChangeLibrary(bool value)
        {
            for (int i = 0; i < 2; i++)
            {
                if (value)
                {
                    _playerView.spriteLibrary[i].spriteLibraryAsset = _playerView.exoskeletonLibrary[i];
                }
                else
                {
                    _playerView.spriteLibrary[i].spriteLibraryAsset = _playerView.realLibrary[i];
                }
            }
        }

        public void ChangeWheel(bool value)
        {
            foreach (var wheel in _playerView.BaseWheelView)
            {
                wheel.gameObject.SetActive(!value);
            }
            foreach (var wheel in _playerView.RunWheelView)
            {
                wheel.gameObject.SetActive(value);
                wheel.gameObject.transform.rotation =quaternion.identity;
                wheel.Animator.SetBool("IsRotate", value);
            }
        }
        
        private void OnTriggerObstacleEvent()
        {
            _lifeController.MinusLife();
            obstacleAnimationController.ShakeCamera();
            _timerController.RemoveTimer(isExoskeleton);
            if (_lifeController.CheckLifeCount())
            {
                _currentAnimator.SetBool("IsDead", true);
                OnEndGame?.Invoke();
            }
            else if(_lifeController.GetCurrentLifeCount() == 1)
            {
                ReturnToReality?.Invoke();
            }

            ChangeIsExoskelet();
        }

        private void ChangeIsExoskelet()
        {
            if (isExoskeleton)
            {
                ChangeLibrary(false);
                isExoskeleton = false;
                _timerController.ExoskeletonTimerIsOver -= ChangeIsExoskelet;
            }
        }
    }
}