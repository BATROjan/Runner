using System;
using UnityEngine;
using VGUIService;

public class UIExitController
{
   private readonly IUIService _uiService;
   private readonly UIExitWindow _uiExitWindow;
   
   public UIExitController(IUIService uiService)
   {
      _uiService = uiService;
      _uiExitWindow = _uiService.Get<UIExitWindow>();

      _uiExitWindow.ShowEvent += ShowWindow;
      _uiExitWindow.HideEvent += HideWindow;
   }
    
   private void ShowWindow(object sender, EventArgs e)
   {
      _uiExitWindow.ButtonAccept.OnClickButton += ExitGame;
      _uiExitWindow.ButtonUnaccept.OnClickButton += CancelExit;
   }
    
   private void HideWindow(object sender, EventArgs e)
   {
      _uiExitWindow.ButtonAccept.OnClickButton -= ExitGame;
      _uiExitWindow.ButtonUnaccept.OnClickButton -= CancelExit;

   }
   
   private void CancelExit()
   {
      _uiService.Hide<UIExitWindow>();
      _uiService.Show<UIStartWindow>();
   }
   
   private void ExitGame()
   {
      Application.Quit();
   }
}
