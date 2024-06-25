using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VGUIService;

public class UIReturnWindowController
{
    public Action OnReturnMenu;
    
    private readonly IUIService _uiService;
    private readonly UIBlackoutWindowController _uiBlackoutWindowController;
    private readonly UIReturnWindow _uiReturnWindow;
    
    public UIReturnWindowController(
        IUIService uiService,
        UIBlackoutWindowController uiBlackoutWindowController)
    {
        _uiService = uiService;
        _uiBlackoutWindowController = uiBlackoutWindowController;
        _uiReturnWindow = _uiService.Get<UIReturnWindow>();

        _uiReturnWindow.ShowEvent += ShowWindow;
        _uiReturnWindow.HideEvent += HideWindow;
    }
    
    private void ShowWindow(object sender, EventArgs e)
    {
        _uiReturnWindow.ReturnPauseButton.OnClickButton += ReturnPauseWindow;
        _uiReturnWindow.ReturnMenuButton.OnClickButton += ReturnMenuWindow;
    }
    
    private void HideWindow(object sender, EventArgs e)
    {
        _uiReturnWindow.ReturnPauseButton.OnClickButton -= ReturnPauseWindow;
        _uiReturnWindow.ReturnMenuButton.OnClickButton -= ReturnMenuWindow;
    }
    
    private void ReturnMenuWindow()
    {
        Time.timeScale = 1f;
        _uiService.Hide<UIReturnWindow>();
        
        _uiBlackoutWindowController.FadeIn(0f).OnComplete(() =>
        {
            OnReturnMenu.Invoke();
            _uiService.Hide<UIPlayingSceneWindow>();
            DOVirtual.DelayedCall(0.3f, () => _uiBlackoutWindowController.FadeOut(1f));
        });
    }
    
    private void ReturnPauseWindow()
    {
        _uiService.Hide<UIReturnWindow>();
        _uiService.Show<UIPauseWindow>();
    }
}
