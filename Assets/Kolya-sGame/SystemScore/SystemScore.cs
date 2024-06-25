using System;
using DG.Tweening;
using UniRx;

public class SystemScore : ISystemScore
{
    private readonly UIPlayingSceneWindowController _uiPlayingSceneWindowController;
    private readonly ParametersConfig _parametersConfig;
    private readonly SaveDataSystem _saveDataSystem;
    private readonly StarController _starController;
    private readonly IWorldController _worldController;

    private IDisposable _subscription;
    private Tween _tween;
    private float _ratioRiseSpeedScore;
    private float _currentScore;
    private float _bestScore;

    public SystemScore(
        UIPlayingSceneWindowController uiPlayingSceneWindowController,
        ParametersConfig parametersConfig,
        SaveDataSystem saveDataSystem,
        StarController starController)
    {
        _uiPlayingSceneWindowController = uiPlayingSceneWindowController;
        _parametersConfig = parametersConfig;
        _saveDataSystem = saveDataSystem;
        _starController = starController;
    }
    
    public void StartScore()
    {
        _currentScore = 0;
        _bestScore = GetBestScore();
        _tween = DOTween.To(ChangeScore, _parametersConfig.StartRatioScore, _parametersConfig.EndRatioScore, _parametersConfig.DurationRiseScore).SetEase(Ease.Linear);
        _subscription  = Observable.Interval(TimeSpan.FromSeconds(0.2)).Subscribe(_ =>
        {
            _currentScore += _ratioRiseSpeedScore;
            _uiPlayingSceneWindowController.ChangeScoreText(_currentScore);
        });
    }

    private void ChangeScore(float value)
    {
        _ratioRiseSpeedScore = value;
    }
    
    public void StopScore()
    {
        _tween.Kill();
        _tween = null;
        
        _subscription.Dispose();

        _currentScore += _starController.GetCurrentScoreStarForScore();
        if (_currentScore > _bestScore)
        {
            _saveDataSystem.SaveBestScore(GetCurrentScore());
        }
    }

    public int GetCurrentScore()
    {
        return (int)_currentScore;
    }

    public int GetBestScore()
    {
        return _saveDataSystem.LoadBestScore();
    }
}