using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kolya_sGame.UI
{
    public class UIbutton : MonoBehaviour, IPointerClickHandler
    {
        public Action OnClickButton;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickButton?.Invoke();    
        }
    }
}