using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VGUIService;

public class UIBlackoutWindowController
{
   private readonly IUIService _uiService;
   private readonly UIBlackoutWindow _uiBlackoutWindow;
   
   public UIBlackoutWindowController(
      IUIService uiService)
   {
      _uiService = uiService;
      _uiBlackoutWindow = _uiService.Show<UIBlackoutWindow>();
   }

   public Tween FadeIn(float duration)
   {
      _uiService.Show<UIBlackoutWindow>();
      return _uiBlackoutWindow.BlackoutSprite.DOFade(1f, duration).SetEase(Ease.Linear);
   }
   
   public Tween FadeOut(float duration)
   {
      return _uiBlackoutWindow.BlackoutSprite.DOFade(0f, duration).SetEase(Ease.Linear).OnComplete(() =>
      {
         _uiService.Hide<UIBlackoutWindow>();
      });
   }
}
