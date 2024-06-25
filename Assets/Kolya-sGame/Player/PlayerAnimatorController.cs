using System;
using UnityEngine;

namespace Kolya_sGame.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        public Action OnDuck;
        public GameObject objectdsf;
        [SerializeField] private PolygonCollider2D currentColler2D;
        [SerializeField] private PolygonCollider2D playerCollider2D;
        public void ChangeDuckSprite()
        {
             OnDuck?.Invoke();
            objectdsf.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }       
        public void ChangeDuckCollier()
        {
            PolygonCollider2D next = currentColler2D;
            Vector2[] tempArray = (Vector2[])next.points.Clone();
            playerCollider2D.SetPath(0, tempArray);
        }
    }
}