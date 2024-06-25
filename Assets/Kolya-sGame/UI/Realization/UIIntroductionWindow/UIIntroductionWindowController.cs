using System;
using VGUIService;

public class UIIntroductionWindowController
{
    private readonly IUIService _uiService;
    private readonly UIIntroductionWindow _uiIntroductionWindow;

    public UIIntroductionWindowController(IUIService uiService, UIIntroductionWindow uiIntroductionWindow)
    {
        _uiService = uiService;
        _uiIntroductionWindow = uiIntroductionWindow;

        _uiIntroductionWindow.ShowEvent += ShowWindow;
        _uiIntroductionWindow.HideEvent += HideWindow;
    }

    private void ShowWindow(object sender, EventArgs e)
    {
        
    }

    private void HideWindow(object sender, EventArgs e)
    {
        
    }
}
