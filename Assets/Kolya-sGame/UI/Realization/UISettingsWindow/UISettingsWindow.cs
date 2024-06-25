using Kolya_sGame.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VGUIService;

public class UISettingsWindow : UIWindow
{
    public UIButtonSetting ButtonRus => buttonRus;
    public UIButtonSetting ButtonEng => buttonEng;
    public UIbutton ButtonReturn => buttonReturn;
    public Sprite[] SpriteStateLanguage => spriteStateLanguage;
    [SerializeField] private UIButtonSetting buttonRus;
    [SerializeField] private UIButtonSetting buttonEng;
    [SerializeField] private UIbutton buttonReturn;
    [SerializeField] private Sprite[] spriteStateLanguage;
    [SerializeField] private Image panelImage;
    
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public void ChangePanelSprite(Sprite sprite)
    {
        panelImage.sprite = sprite;
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null,null);
    }
}
