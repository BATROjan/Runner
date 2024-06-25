using System;
using VGUIService;

public class UIPauseWindowController
{
    public Action OnReturnGame;
    
    private readonly IUIService _uiService;
    private readonly UIPauseWindow _uiPauseWindow;
    
    public UIPauseWindowController(
        IUIService uiService)
    {
        _uiService = uiService;
        _uiPauseWindow = _uiService.Get<UIPauseWindow>();
        _uiPauseWindow.ShowEvent += ShowWindow;
        _uiPauseWindow.HideEvent += HideWindow;
    }
    
    private void ShowWindow(object sender, EventArgs e)
    {
        _uiPauseWindow.ButtonReturnGame.OnClickButton += ReturnGameEvent;
        _uiPauseWindow.ButtonReturnMenu.OnClickButton += ReturnMenuEvent;
    }
    
    private void HideWindow(object sender, EventArgs e)
    {
        _uiPauseWindow.ButtonReturnGame.OnClickButton -= ReturnGameEvent;
        _uiPauseWindow.ButtonReturnMenu.OnClickButton -= ReturnMenuEvent;
    }
    
    private void ReturnGameEvent()
    {
        _uiService.Hide<UIPauseWindow>();
        OnReturnGame.Invoke();
    }

    private void ReturnMenuEvent()
    {
        _uiService.Hide<UIPauseWindow>();
        _uiService.Show<UIReturnWindow>();
    }
}
