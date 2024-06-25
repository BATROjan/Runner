using System.Collections;
using System.Collections.Generic;
using Kolya_sGame.UI;
using UnityEngine;
using VGUIService;

public class UIReturnWindow : UIWindow
{
    public UIbutton ReturnMenuButton => returnMenuButton;
    public UIbutton ReturnPauseButton => returnPauseButton;
    [SerializeField] private UIbutton returnMenuButton;
    [SerializeField] private UIbutton returnPauseButton;

    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null, null);
    }
}
