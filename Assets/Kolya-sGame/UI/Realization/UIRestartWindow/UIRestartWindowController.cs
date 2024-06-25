using System;
using DG.Tweening;
using UniRx;
using VGUIService;

public class UIRestartWindowController
{
   public Action OnRestartGame;
   public Action OnReturnMenu;
   public Action OnStopGame;
   
   private readonly IUIService _uiService;
   private readonly StarController _starController;
   private readonly UIBlackoutWindowController _uiBlackoutWindowController;
   private readonly SystemScore _systemScore;
   private readonly UIRestartWindow _uiRestartWindow;

   private const float TimeLimitRiseStar = 2f;
   private const float TimeMinRiseStar = 0.01f;
   private IDisposable _subscription;
   private int _currentScore;
   private int _amountStar;
   private int _currentScoreWithoutStar;
   private float _timeRiseStar;
   
   public UIRestartWindowController(
      IUIService uiService,
      ISystemScore systemScore,
      StarController starController,
      UIBlackoutWindowController uiBlackoutWindowController)
   {
      _uiService = uiService;
      _starController = starController;
      _uiBlackoutWindowController = uiBlackoutWindowController;
      _systemScore = (SystemScore)systemScore;
      _uiRestartWindow = _uiService.Get<UIRestartWindow>();
      
      _uiRestartWindow.ShowEvent += ShowScoreWindow;
      _uiRestartWindow.HideEvent += HideScoreWindow;
   }
   
   private void ShowScoreWindow(object sender, EventArgs e)
   {
      _amountStar = _starController.GetCurrentScoreStar();
      _currentScoreWithoutStar = _systemScore.GetCurrentScore();

      var bestScore = _systemScore.GetBestScore().ToString();
      var currentScoreWithStar = (_systemScore.GetCurrentScore() + _starController.GetCurrentScoreStarForScore()).ToString();
      
      _uiRestartWindow.TMPTextCurrentScore.text = _currentScoreWithoutStar.ToString();
      _uiRestartWindow.TMPTextBestScore.text = bestScore;
      _uiRestartWindow.TmpTextAmountStar.text = "0";

      if (_amountStar != 0)
      {
         SetTimeRiseStar();
         DOVirtual.DelayedCall(0.6f, AddStarInScore);
      }
      
      if (int.Parse(currentScoreWithStar) > int.Parse(bestScore))
      {
         _uiRestartWindow.PanelNewRecord.gameObject.SetActive(true);
         _uiRestartWindow.TMPTextBestScore.text = currentScoreWithStar;
      }

      _uiRestartWindow.ButtonRestartGame.OnClickButton += RestartGame;
      _uiRestartWindow.ButtonReturnMenu.OnClickButton += ReturnMenu;
      
      OnStopGame.Invoke();
   }

   private void HideScoreWindow(object sender, EventArgs e)
   {
      _subscription?.Dispose();
      _uiRestartWindow.TMPTextCurrentScore.text = "";
      _uiRestartWindow.PanelNewRecord.gameObject.SetActive(false);
      
      _uiRestartWindow.ButtonRestartGame.OnClickButton -= RestartGame;
      _uiRestartWindow.ButtonReturnMenu.OnClickButton -= ReturnMenu;
   }
   
   private void RestartGame()
   {
      _uiBlackoutWindowController.FadeIn(0.5f).OnComplete(() =>
      {
         _uiBlackoutWindowController.FadeOut(0.5f);
         OnRestartGame.Invoke();
         _uiService.Hide<UIRestartWindow>();
         _uiService.Show<UIPlayingSceneWindow>();
      });
      
   }
   
   private void ReturnMenu()
   {
      _uiBlackoutWindowController.FadeIn(0.5f).OnComplete(() =>
      {
         _uiService.Hide<UIRestartWindow>();
         OnReturnMenu.Invoke();
         _uiBlackoutWindowController.FadeOut(1f);
      });
   }

   private void AddStarInScore()
   {
      int currentAmountStar = 0;
      _subscription  = Observable.Interval(TimeSpan.FromSeconds(_timeRiseStar)).Subscribe(_ =>
      {
         if (currentAmountStar != _amountStar)
         {
            currentAmountStar++;
            _currentScoreWithoutStar += _starController.GetRatioStar();
            _uiRestartWindow.TmpTextAmountStar.text = currentAmountStar.ToString();
            _uiRestartWindow.TMPTextCurrentScore.text = _currentScoreWithoutStar.ToString();
         }
         else
         {
            _subscription.Dispose();
         }
      });
   }

   private void SetTimeRiseStar()
   {
      if (TimeMinRiseStar * _amountStar > TimeLimitRiseStar)
      {
         _timeRiseStar = TimeLimitRiseStar / (_amountStar);
      }
      else
      {
         _timeRiseStar = TimeMinRiseStar;
      }
   }
}
