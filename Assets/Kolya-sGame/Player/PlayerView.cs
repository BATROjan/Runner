using System;
using System.Collections.Generic;
using DG.Tweening;
using Kolya_sGame.Buff;
using Kolya_sGame.Player;
using Kolya_sGame.World;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;
using Zenject;

public class PlayerView : MonoBehaviour
{
    public SpriteLibrary[] spriteLibrary;
    public SpriteLibraryAsset[] exoskeletonLibrary;
    public SpriteLibraryAsset[] realLibrary;
    
    public GameObject[] PlayerStates;
    public Action OnTakeVRGlass;
    public Action<BuffView> OnTakeExoskeleton;
    public Action OnDuck;
    public Action OnTriggerObstacle;
    public PolygonCollider2D PolygonCollider2D => polygonCollider2D;
    public WheelView[] BaseWheelView;
    public WheelView[] RunWheelView;

    public bool IsGrounded;
    public Rigidbody2D Rigidbody2D => rigidbody2D;
    public Animator[] PlayerAnimator => animator;

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Animator[] animator;
    [SerializeField] private PolygonCollider2D polygonCollider2D;
    
    private bool _isReady = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Buff"))
        {
            var buff = other.GetComponentInChildren<BuffView>();
            if (buff.BuffName == BuffName.VRGlasses)
            {
                OnTakeVRGlass.Invoke();
            }
            else if(buff.BuffName == BuffName.Exosceleton)
            {
                OnTakeExoskeleton?.Invoke(buff);
            }
        }
        
        if (other.CompareTag("Obstacle") && _isReady)
        {
            _isReady = false;
            DOVirtual.DelayedCall(0.2f, () => _isReady = true);
            OnTriggerObstacle.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

    private void ReInit(PLayerModel pLayerModel)
    {
        transform.position = pLayerModel.SpawnPosition;
    }

    public class Pool : MonoMemoryPool<PLayerModel,PlayerView>
    {
        protected override void Reinitialize(PLayerModel pLayerModel, PlayerView item)
        {
            item.ReInit(pLayerModel);
        }
    }
}