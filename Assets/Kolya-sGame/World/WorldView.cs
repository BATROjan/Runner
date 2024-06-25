using System;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace Kolya_sGame.World
{
    public class WorldView : MonoBehaviour, IPoolable<WorldTileProtocol, IMemoryPool>
    {
        public Action<int, WorldView> OnBecameInvisibleEvent;
        
        public SpriteRenderer SpriteRendererReality => _spriteRendererReality;
        public SpriteRenderer SpriteRendererVR => _spriteRendererVR;
        public int NumberLayer => _numberLayer;
        public float Speed => _speed;
        public float RightBoardSprite => _rightBoardSprite;
        public VideoPlayer BannerClip => bannerClip;
        
        [SerializeField] private SpriteRenderer _spriteRendererReality;
        [SerializeField] private SpriteRenderer _spriteRendererVR;
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private VideoPlayer bannerClip;
        
        private int _numberLayer;
        private float _speed;
        private float _rightBoardSprite;
        public void OnDespawned()
        {
        }

        public void OnSpawned(WorldTileProtocol protocol, IMemoryPool pool)
        {
            _spriteRendererReality.sprite = protocol.SpriteForReality;
            _spriteRendererVR.sprite = protocol.SpriteForVR;
            _spriteRendererReality.sortingOrder = protocol.SortingOrderReality;
            _spriteRendererVR.sortingOrder = protocol.SortingOrderVr;
            _numberLayer = protocol.NumberLayer;
            _speed = protocol.Speed;
            _rightBoardSprite = _spriteRendererReality.sprite.bounds.size.x / 2;
            _collider.transform.position = new Vector3(_rightBoardSprite, 0f, 0f);
            if (protocol.BannerModel.Length > 0)
            {
                bannerClip.gameObject.SetActive(true);
                bannerClip.clip = protocol.BannerModel[0].Clip;
                bannerClip.transform.position = protocol.BannerModel[0].BannerPosition;
                //bannerClip.transform.position = protocol.BannerModel[0].BannerPosition;
            }
        }
    
        private void ReInit(WorldTileProtocol protocol)
        {
            _spriteRendererReality.sprite = protocol.SpriteForReality;
            _spriteRendererVR.sprite = protocol.SpriteForVR;
            _numberLayer = protocol.NumberLayer;
            _speed = protocol.Speed;
            _rightBoardSprite = _spriteRendererReality.sprite.bounds.size.x / 2;
            _collider.transform.position = new Vector3(_rightBoardSprite, 0f, 0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EndVisible"))
            {
                OnBecameInvisibleEvent.Invoke(_numberLayer, this);
            }
        }

        public class Pool : MonoMemoryPool<WorldTileProtocol, WorldView>
        {
            protected override void Reinitialize(WorldTileProtocol protocol, WorldView item)
            {
                item.ReInit(protocol);
                item.OnSpawned(protocol, this);
            }

            protected override void OnDespawned(WorldView item)
            {
                base.OnDespawned(item);
                item.OnDespawned();
            }
        }
    }

    [Serializable]
    public struct WorldTileProtocol
    {
        public Sprite SpriteForReality;
        public Sprite SpriteForVR;
        public int NumberLayer;
        public float Speed;
        public int SortingOrderReality;
        public int SortingOrderVr;
        public BannerModel[] BannerModel;
        public WorldTileProtocol(
            Sprite spriteForReality,
            Sprite spriteForVR,
            int numberLayer,
            float speed,
            int sortingOrderReality,
            int sortingOrderVr, 
            BannerModel[] bannerModel)
        {
            SpriteForReality = spriteForReality;
            SpriteForVR = spriteForVR;
            NumberLayer = numberLayer;
            Speed = speed;
            SortingOrderReality = sortingOrderReality;
            SortingOrderVr = sortingOrderVr;
            BannerModel = bannerModel;
        }
    }
}