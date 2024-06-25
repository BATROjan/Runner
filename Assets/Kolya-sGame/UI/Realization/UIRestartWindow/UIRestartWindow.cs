using Kolya_sGame.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using VGUIService;

public class UIRestartWindow : UIWindow
{
    public UIbutton ButtonRestartGame => buttonRestartGame;
    public UIbutton ButtonReturnMenu => buttonReturnMenu;
    public TMP_Text TMPTextCurrentScore => tmpTextCurrentScore;
    public TMP_Text TMPTextBestScore => tmpTextBestScore;
    public TMP_Text TmpTextAmountStar => tmpTextAmountStar;
    public Transform PanelNewRecord => panelNewRecord;
    [SerializeField] private UIbutton buttonRestartGame;
    [SerializeField] private UIbutton buttonReturnMenu; 
    [SerializeField] private TMP_Text tmpTextCurrentScore;
    [SerializeField] private TMP_Text tmpTextBestScore;
    [SerializeField] private TMP_Text tmpTextAmountStar;
    [SerializeField] private Transform panelNewRecord;
    
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null, null);
    }
}
