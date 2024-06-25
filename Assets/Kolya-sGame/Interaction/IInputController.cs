using System;
using UnityEngine;

namespace Kolya_sGame.Interaction
{
    public interface IInputController
    {
        public event Action OnJumpAction;
        public event Action OnDuckAction;

        public void StartInteraction();
        public void EndInteraction();

        public void JumpLogic(Rigidbody2D rigidbody2D ,float jumpPower, bool isJump);
        public void DuckLogic(Transform playerTrasform);
    }
}