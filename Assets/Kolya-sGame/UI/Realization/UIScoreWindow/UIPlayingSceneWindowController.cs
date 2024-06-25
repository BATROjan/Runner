using System;
using VGUIService;

public class UIPlayingSceneWindowController
{
    private readonly IUIService _uiService;
    public Action OnPauseGame;
    
    private readonly UIPlayingSceneWindow _uiPlayingSceneWindow;

    public UIPlayingSceneWindowController(IUIService uiService)
    {
        _uiService = uiService;
        _uiPlayingSceneWindow = _uiService.Get<UIPlayingSceneWindow>();
        _uiPlayingSceneWindow.ShowEvent += ShowWindow;
        _uiPlayingSceneWindow.HideEvent += HideWindow;
    }
    
    public void ChangeScoreText(float score)
    {
        _uiPlayingSceneWindow.TmpTextScore.text = ((int)score).ToString();
    }
    

    public void ResetText()
    {
        _uiPlayingSceneWindow.TmpTextScore.text = "";
    }

    public void ShowButtonPause()
    {
        _uiPlayingSceneWindow.ButtonPause.gameObject.SetActive(true);
    }
    
    public void HideButtonPause()
    {
        _uiPlayingSceneWindow.ButtonPause.gameObject.SetActive(false);
    }
    
    private void PauseGameEvent()
    {
        _uiPlayingSceneWindow.ButtonPause.gameObject.SetActive(false);
        _uiService.Show<UIPauseWindow>();
        OnPauseGame.Invoke();
    }
    
    private void ShowWindow(object sender, EventArgs e)
    {
        _uiPlayingSceneWindow.ButtonPause.OnClickButton += PauseGameEvent;
    }
    
    private void HideWindow(object sender, EventArgs e)
    {
        ShowButtonPause();
        
        _uiPlayingSceneWindow.ButtonPause.OnClickButton -= PauseGameEvent;
    }
}
