using VGUIService;

public class UIIntroductionWindow : UIWindow
{
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null, null);
    }
}
