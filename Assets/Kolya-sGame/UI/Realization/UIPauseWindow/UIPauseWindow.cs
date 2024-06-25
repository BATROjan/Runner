using Kolya_sGame.UI;
using UnityEngine;
using VGUIService;

public class UIPauseWindow : UIWindow
{
    public UIbutton ButtonReturnMenu => buttonReturnMenu;
    public UIbutton ButtonReturnGame => buttonReturnGame;
    [SerializeField] private UIbutton buttonReturnMenu;
    [SerializeField] private UIbutton buttonReturnGame;
    
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null, null);
    }
}