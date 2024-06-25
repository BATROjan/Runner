using Kolya_sGame.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VGUIService;

public class UIPlayingSceneWindow : UIWindow
{
    public Transform[] TimerSpawnPositions => timerSpawnPositions;
    public TMP_Text TmpTextScore => tmpTextScore;
    public UIbutton ButtonPause => buttonPause;
    [SerializeField] private TMP_Text tmpTextScore;
    [SerializeField] private UIbutton buttonPause;
    [SerializeField] private Transform[] timerSpawnPositions;
    
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null, null);
    }
}
