using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Kolya_sGame.Buff
{
    public class BuffView : MonoBehaviour
    {
        public Action<BuffView> IsTaken;
        public BuffName BuffName => _buffName;

        public SpriteRenderer StarSpriteRenderer => starSpriteRenderer;
        
        [SerializeField] private BoxCollider2D collider2D;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer itemSpriteRenderer;
        [SerializeField] private SpriteRenderer starSpriteRenderer;

        private BuffName _buffName;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                IsTaken?.Invoke(this);
            }
        }
        
        private void ReInit(BuffModel pLayerModel)
        {
            _buffName = pLayerModel.Name;
            itemSpriteRenderer.sprite = pLayerModel.ItemSprite;
            starSpriteRenderer.sprite = pLayerModel.StarSprite;
            collider2D.size = pLayerModel.Size;
        }
        
        public class Pool : MonoMemoryPool<BuffModel,BuffView>
        {
            protected override void Reinitialize(BuffModel buffModel, BuffView item)
            {
                item.ReInit(buffModel);
            }
        }
    }
}