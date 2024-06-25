using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ParametersConfig", menuName = "Configs/ParametersConfig")]
public class ParametersConfig : ScriptableObject
{
    public float StartRatioSpeed => startRatioSpeed;
    public float EndRatioSpeed => endRatioSpeed;
    public float DurationRiseSpeed => durationRiseSpeed;
    public float StartRatioScore => startRatioScore;
    public float EndRatioScore => endRatioScore;
    public float DurationRiseScore => durationRiseScore;
    public float StartRatioStar => startRatioStar;
    public float EndRatioStar => endRatioStar;
    public float DurationRiseStar => durationRiseStar;
    public float StartSpeedPlayerSit => startSpeedPlayerSit;
    public float EndSpeedPlayerSit => endSpeedPlayerSit;
    public float DurationSpeedPlayerSit => durationSpeedPlayerSit;
    public float StartSpeedPlayerStand => startSpeedPlayerStand;
    public float EndSpeedPlayerStand => endSpeedPlayerStand;
    public float DurationSpeedPlayerStand => durationSpeedPlayerStand;
    public float StartSpeedWheel => startSpeedWheel;
    public float EndSpeedWheel => endSpeedWheel;
    public float DurationSpeedWheel => durationSpeedWheel;

    [Header("For speed")] 
    [SerializeField] private float startRatioSpeed;
    [SerializeField] private float endRatioSpeed;
    [SerializeField] private float durationRiseSpeed;
    [Header("For score")]
    [SerializeField] private float startRatioScore;
    [SerializeField] private float endRatioScore;
    [SerializeField] private float durationRiseScore;
    [Header("For star")]
    [SerializeField] private float startRatioStar;
    [SerializeField] private float endRatioStar;
    [SerializeField] private float durationRiseStar;
    [Header("For player")]
    [SerializeField] private float startSpeedPlayerSit;
    [SerializeField] private float endSpeedPlayerSit;
    [SerializeField] private float durationSpeedPlayerSit;
    [SerializeField] private float startSpeedPlayerStand;
    [SerializeField] private float endSpeedPlayerStand;
    [SerializeField] private float durationSpeedPlayerStand;
    [Header("For wheel")]
    [SerializeField] private float startSpeedWheel; 
    [SerializeField] private float endSpeedWheel; 
    [SerializeField] private float durationSpeedWheel;
}
