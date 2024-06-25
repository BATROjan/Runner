using System;
using DG.Tweening;
using Kolya_sGame.Player;
using Lean.Touch;
using UnityEngine;
using VGUIService;
using Zenject;

namespace Kolya_sGame.Interaction
{
    public class BaseInputController : IInputController, ITickable
    {
        public event Action OnJumpAction;
        public event Action OnDuckAction;
        
        private readonly PlayerController _playerController;
        private readonly TickableManager _tickableManager;
        private readonly SwipeDown _swipeDown;

        protected bool isDucking;
        protected bool interactionIsActive;
        protected bool isFall;
        private float _jumpingTime;
        
        #if UNITY_IPHONE || UNITY_ANDROID
        private float _jumpingTimeMax = 0.5f;
        #else
        private float _jumpingTimeMax = 0.2f;
        #endif
        
        protected PlayerView _playerView;

        private Vector3 _playerPosition;
        private Tween duckingTween;
        private Tween duckingDelayTween;

        private Animator _currentAnimator;

        private int _amountJump;

        protected BaseInputController(
            PlayerController playerController,
            TickableManager tickableManager,
            SwipeDown swipeDown)
        {
            _playerController = playerController;
            _tickableManager = tickableManager;
            _swipeDown = swipeDown;
            _tickableManager.Add(this);
        }
        
        public void StartInteraction()
        {
            SubscribeLeanTouch();
            
            interactionIsActive = true;
            if (!_playerView)
            {
                _playerView = _playerController.GetPlayerView();
                _currentAnimator = _playerController.GetCurrentAnimator();
            }
        }
        
        public void EndInteraction()
        {
            interactionIsActive = false;
            _playerView = null;

            UnSubscribeLeanTouch();
        }

        public void SubscribeLeanTouch()
        {
            DOVirtual.DelayedCall(0.1f, () =>
            {
                LeanTouch.OnFingerTap += FingerTap;
                _swipeDown.LeanFingerSwipe.onFinger.AddListener(FingerSwipe);
            });
        }

        public void UnSubscribeLeanTouch()
        {
            LeanTouch.OnFingerTap -= FingerTap;
            _swipeDown.LeanFingerSwipe.onFinger.RemoveListener(FingerSwipe);
        }
        
        public void JumpLogic(Rigidbody2D rigidbody2D, float jumpPower, bool isJump)
        {
            if (_playerView.IsGrounded)
            {
                isFall = false;
                _amountJump = 0;
            }

            if (!isFall && _amountJump != 2)
            {
                 _amountJump++;
                if (isDucking)
                {
                    if (_playerView.PlayerStates[2].gameObject.activeSelf)
                    {
                        _playerView.PlayerStates[2].gameObject.SetActive(false);
                        _playerController.ChangePLayerSprite(false);
                    }
                    
                    duckingTween.Kill();
                    duckingDelayTween.Kill();
                        
                    isDucking = false;
                }

                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
                OnJumpAction?.Invoke(); 
            }
        }

        public void DuckLogic(Transform playerTrasform)
        {
            if (!isDucking)
            {
                if (!_playerView.IsGrounded)
                {
                    _playerView.Rigidbody2D.velocity = new Vector2(_playerView.Rigidbody2D.velocity.x, -10);
                }
                
                _playerController.GetCurrentAnimator().SetBool("IsDucking", true);
                _playerController.ChangeCollider(ColliderState.Duck);
                isDucking = true;
                isFall = true;
                OnDuckAction?.Invoke();
                duckingDelayTween = DOVirtual.DelayedCall(1.2f, () =>
                {
                    _playerController.GetCurrentAnimator().SetBool("IsDucking", false);
                    _playerController.ChangeCollider(ColliderState.Run);
                     isDucking = false;
                }).OnKill(() => {
                    _playerController.GetCurrentAnimator().SetBool("IsDucking", false);
                    _playerController.ChangeCollider(ColliderState.Run);
                    isDucking = false;
                    isFall = false;
                });
            }
        }

        public virtual void Tick()
        {
            if (_playerView)
            {
                _playerController.GetCurrentAnimator().SetFloat("Velocity", _playerView.Rigidbody2D.velocity.y);
                _playerController.GetCurrentAnimator().SetBool("IsRunning", _playerView.IsGrounded);
                if (_playerView.Rigidbody2D.velocity.y > 0.5f )
                {
                    _playerController.ChangeCollider(ColliderState.JumpUp);
                }
                if (_playerView.Rigidbody2D.velocity.y < 0.5f &&  !_playerView.IsGrounded && !isDucking)
                {
                    _playerController.ChangeCollider(ColliderState.JumpDown);
                }
                if(_playerView.Rigidbody2D.velocity.y == 0 && !isDucking)
                {
                    _playerController.ChangeCollider(ColliderState.Run);
                }
            }
        }
        
        private void FingerSwipe(LeanFinger obj)
        {
            DuckLogic(_playerView.transform);
        }

        private void FingerTap(LeanFinger obj)
        {
            JumpLogic(_playerView.Rigidbody2D, 8, true);
        }
    }
}