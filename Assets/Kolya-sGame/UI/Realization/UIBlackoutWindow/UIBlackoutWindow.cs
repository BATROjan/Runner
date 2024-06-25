using UnityEngine;
using UnityEngine.UI;
using VGUIService;

public class UIBlackoutWindow : UIWindow
{
    public Image BlackoutSprite => blackoutSprite;
    [SerializeField] private Image blackoutSprite;
    
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null, null);
    }
}
