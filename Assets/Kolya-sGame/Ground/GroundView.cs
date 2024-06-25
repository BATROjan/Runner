using UnityEngine;
using Zenject;

namespace Kolya_sGame.Ground
{
    public class GroundView : MonoBehaviour
    {
        public Transform GroundTransform;
        public PolygonCollider2D PolygonCollider2DForGround;
        public class Pool : MonoMemoryPool<GroundView>
        {
        }
    }
}