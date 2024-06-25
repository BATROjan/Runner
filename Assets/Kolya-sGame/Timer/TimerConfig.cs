using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kolya_sGame.Timer
{
    [CreateAssetMenu(fileName = "TimerConfig", menuName = "Configs/TimerConfig")]

    public class TimerConfig : ScriptableObject
    {
        [SerializeField] private TimerModel[] timerModels;
        [NonSerialized] private bool _inited;

        private Dictionary<TimerType, TimerModel> _timerModelsDictionary = new Dictionary<TimerType, TimerModel> ();

        public TimerModel GetTimerModelByType(TimerType type)
        {
            if (!_inited)
            {
                Init();
            }
            
            if (_timerModelsDictionary.ContainsKey(type))
            {
                return _timerModelsDictionary[type];
            }

            Debug.LogError($"There no such world with type: {type}");
            
            return new TimerModel();
        }
        
        private void Init()
        {
            foreach (var model in timerModels)
            {
                _timerModelsDictionary.Add(model.TimerType, model);
            }

            _inited = true;
        }
    }

    [Serializable]
    public struct TimerModel
    {
        public TimerType TimerType;
        public Sprite Icon;
        public Sprite Scale;
        public int Seconds;
    }

    public enum TimerType
    {
        TimerVRWorld,
        TimerExsosceleton,
        TimerToDelayVR
    }
}