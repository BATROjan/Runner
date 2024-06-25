using System;
using Kolya_sGame.UI;
using TMPro;
using UnityEngine;
using VGUIService;

public class UIStartWindow : UIWindow
{
    public Action OnSubscribeButton;
    public Action OnUnsubscribeButton;
    public UIbutton PlayButton => playButton;
    public UIbutton ExitButton => exitButton;
    public UIbutton SettingsButton => settingsButton;
    public UIbutton VolumeButton => volumeButton;
    public UIbutton BoostyButton => boostyButton;
    public UIbutton ChangeStyleButton => changeStyleButton;
    public TMP_Text TmpBestScore => tmpBestScore;
    public Sprite[] StateMuteButton => stateMuteButton;
    
    [SerializeField] private UIbutton playButton;
    [SerializeField] private UIbutton exitButton;
    [SerializeField] private UIbutton settingsButton;
    [SerializeField] private UIbutton volumeButton;
    [SerializeField] private UIbutton boostyButton;
    [SerializeField] private UIbutton changeStyleButton;
    [SerializeField] private TMP_Text tmpBestScore;
    [SerializeField] private Sprite[] stateMuteButton; 
    
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null,null);
    }

    public void SubscribeButton()
    {
        OnSubscribeButton?.Invoke();
    }
    
    public void UnSubscribeButton()
    {
        OnUnsubscribeButton?.Invoke();
    }
}
