using System;
using Kolya_sGame.Obstacle;
using UnityEngine;
using Zenject;

public class StarView : MonoBehaviour, IPoolable<StarProtocol, IMemoryPool>
{
    public Action<StarView> OnTriggerPlayer;
    
    [SerializeField] private SpriteRenderer _spriteRendererReality;
    [SerializeField] private SpriteRenderer _spriteRendererVR;
    
    private Vector3 _defaultScale = new Vector3(0.4f,0.4f,0.4f);
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerPlayer.Invoke(this);
        } 
    }

    public void OnDespawned()
    {
        transform.localScale = _defaultScale;
    }

    public void OnSpawned(StarProtocol protocol, IMemoryPool p2)
    {
        transform.localScale = _defaultScale;
    }
    
    private void ReInit(StarProtocol protocol)
    {
        transform.localScale = _defaultScale;
    }
    
    public class Pool : MonoMemoryPool<StarProtocol, StarView>
    {
        protected override void Reinitialize(StarProtocol protocol, StarView item)
        {
            item.ReInit(protocol);
            item.OnSpawned(protocol, this);
        }

        protected override void OnDespawned(StarView item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }
    }
}

[Serializable]
public struct StarProtocol
{
}

