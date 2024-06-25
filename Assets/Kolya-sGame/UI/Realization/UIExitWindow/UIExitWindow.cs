using System.Collections;
using System.Collections.Generic;
using Kolya_sGame.UI;
using UnityEngine;
using VGUIService;

public class UIExitWindow : UIWindow
{
    public UIbutton ButtonAccept => buttonAccept;
    public UIbutton ButtonUnaccept => buttonUnaccept;
    [SerializeField] private UIbutton buttonAccept;
    [SerializeField] private UIbutton buttonUnaccept;
    
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null, null);
    }
}
