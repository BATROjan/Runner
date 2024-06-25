using System;
using System.Collections.Generic;
using DG.Tweening;
using Kolya_sGame.Obstacle;
using UnityEngine;
using Random = UnityEngine.Random;

public class StarController
{
    private readonly StarView.Pool _starViewPool;
    private readonly ObstacleTileController _obstacleTileController;
    private readonly ParametersConfig _parametersConfig;
    private readonly SaveDataSystem _saveDataSystem;
    private readonly IAudioController _audioController;

    private const int RatioStar = 10;
    
    private Transform _zeroObstacleTransform;
    private Tween _tween;
    private Vector3 _zeroPointCircle;
    private Dictionary<int, List<StarView>> _dictStarView = new Dictionary<int, List<StarView>>();
    
    private float _zeroAngle;
    private int _scoreStar;
    private int _currentScoreStar;
    private float _ratioLenght;
    
    public StarController(
        StarView.Pool starViewPool,
        ObstacleTileController obstacleTileController,
        ParametersConfig parametersConfig,
        SaveDataSystem saveDataSystem,
        IAudioController audioController)
    {
        _starViewPool = starViewPool;
        _obstacleTileController = obstacleTileController;
        _parametersConfig = parametersConfig;
        _saveDataSystem = saveDataSystem;
        _audioController = audioController;
        _obstacleTileController.OnSpawnTile += SpawnStar;
        _obstacleTileController.OnDespawnTile += DespawnStarForObstacle;
    }
    
    public int GetScoreStar()
    {
        return _scoreStar;
    }
    
    public int GetCurrentScoreStar()
    {
        return _currentScoreStar;
    }

    public int GetCurrentScoreStarForScore()
    {
        return _currentScoreStar * GetRatioStar();
    }

    public int GetRatioStar()
    {
        return RatioStar;
    }
    
    public void AddCurrentScoreToAllScore()
    {
        _scoreStar += _currentScoreStar;
    }
    
    public void StartStarController()
    {
        _currentScoreStar = 0;
        _scoreStar = _saveDataSystem.LoadStarScore();
        _tween = DOTween.To(OnRise, _parametersConfig.StartRatioStar, _parametersConfig.EndRatioStar, _parametersConfig.DurationRiseStar).SetEase(Ease.Linear);
    }

    public void StopStarController()
    {
        _tween.Kill();
        _tween = null;
        AddCurrentScoreToAllScore();
        _saveDataSystem.SaveStarScore(GetScoreStar());
    }
    
    private void SpawnStar(ObstacleTileView currentTile)
    {
        foreach (var point in currentTile.PointViews)
        {
            if (point.ObstacleView && point.ObstacleView.IsSpawnStar)
            {
                var randomProbability = Random.Range(0, 100);
                if (randomProbability <= point.ObstacleView.Probability)
                {
                    _zeroObstacleTransform = point.ObstacleView.transform;
                    var listView = new List<StarView>();
                    
                    foreach (var starPosition in point.ObstacleView.PositionStar)
                    {
                        var starViewOne = _starViewPool.Spawn(new StarProtocol());
                        starViewOne.transform.SetParent(_zeroObstacleTransform);
                        
                        starViewOne.transform.localPosition = starPosition;
                        starViewOne.OnTriggerPlayer += DespawnForPlayer;
                        
                        listView.Add(starViewOne);
                    }
                    _dictStarView.Add(point.ObstacleView.GetInstanceID(), listView);
                }
            }
        }
    }

    private void OnRise(float value)
    {
        _ratioLenght = value;
    }
    
    private Vector2 GetRatioParabola(float lenght, float height)
    {
        var c = height;
        var a = c / (lenght * lenght);
        return new Vector2(a, c);
    }
    
    private void DespawnStarForObstacle(ObstacleTileView currentTile)
    {
        foreach (var point in currentTile.PointViews)
        {
            if (point.ObstacleView && _dictStarView.ContainsKey(point.ObstacleView.GetInstanceID()))
            {
                if (point.ObstacleView.IsSpawnStar)
                {
                    foreach (var view in _dictStarView[point.ObstacleView.GetInstanceID()])
                    {
                        view.OnTriggerPlayer -= DespawnForPlayer;
                        _starViewPool.Despawn(view);
                    }
                    _dictStarView[point.ObstacleView.GetInstanceID()].Clear();
                    _dictStarView.Remove(point.ObstacleView.GetInstanceID());
                }
            }
        }
    }

    private void DespawnForPlayer(StarView starView)
    {
        if (!starView.gameObject.activeSelf)
        {
             return;
        }
        starView.OnTriggerPlayer -= DespawnForPlayer;
        _starViewPool.Despawn(starView);
         
        foreach (var listView in _dictStarView.Values)
        {
            if (listView.Contains(starView))
            {
                listView.Remove(starView);
            }
        }
        _currentScoreStar++;
        
        _audioController.Play(AudioType.Coin);
    }
}