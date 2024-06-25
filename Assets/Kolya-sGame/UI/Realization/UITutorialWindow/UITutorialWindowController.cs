
using System;
using DG.Tweening;
using TMPro;
using VGUIService;

public class UITutorialWindowController
{
    private readonly IUIService _uiService;

    private UITutorialWindow _uiTutorialWindow;

    public UITutorialWindowController(
        IUIService uiService)
    {
        _uiService = uiService;

        _uiTutorialWindow = _uiService.Get<UITutorialWindow>();
    }

    public Tween SetText(string text, float duration)
    {
        _uiTutorialWindow.Text.text = "";
        return _uiTutorialWindow.Text.DOText(text, duration).SetEase(Ease.Linear);
    }
}
