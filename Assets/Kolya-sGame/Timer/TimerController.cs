using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VGUIService;

namespace Kolya_sGame.Timer
{
    public class TimerController
    {
        public Action VRTimerIsOver;
        public Action ExoskeletonTimerIsOver;

        private readonly IUIService _uiService;
        private readonly TimerView.Pool _timerPool;
        private readonly TimerConfig _timerConfig;
        
        private Dictionary<TimerType, TimerView> _timerModelDictionary = new Dictionary<TimerType, TimerView> ();
        private Dictionary<TimerType, IDisposable> _timeDisposablesDictionary = new Dictionary<TimerType, IDisposable>();
        private Dictionary<TimerType, Tween> _timerAnimationDictionary = new Dictionary<TimerType, Tween>();

        private Tween _celltween;
        private Tween _cellDelaytween;
        private bool _changeToNextCell;
        
        public TimerController(
            IUIService uiService,
            TimerView.Pool timerPool,
            TimerConfig timerConfig)
        {
            _uiService = uiService;
            _timerPool = timerPool;
            _timerConfig = timerConfig;
        }

        public void CreatNewTimer(TimerType type)
        {
            if (_timerModelDictionary.ContainsKey(type))
            {
                _timerPool.Despawn(_timerModelDictionary[type]);
                _timerModelDictionary.Remove(type);
            }
            
            if (_timeDisposablesDictionary.ContainsKey(type))
            {
                _timeDisposablesDictionary[type].Dispose();
                _timeDisposablesDictionary.Remove(type);
            }

            var timer = _timerPool.Spawn(_timerConfig.GetTimerModelByType(type));
            foreach (var cell in timer.Cells)
            {
                cell.DOFade(1, 0);
            }
            
            timer.gameObject.transform.SetParent(_uiService.Get<UIPlayingSceneWindow>().TimerSpawnPositions[_timerModelDictionary
                .Count], false);

            _timerModelDictionary.Add(type,timer);
            
            var disposable =  Observable.Interval(TimeSpan.FromSeconds(_timerModelDictionary[type].TimerTime))
                .Subscribe(next => TimerIsOverLogic(_timerModelDictionary[type].TimerType));

            _timeDisposablesDictionary.Add(type, disposable);
              ChangeTimerFillAmount(type);
        }

        public void RemoveTimer(bool exoskeleton)
        {
            if (_timeDisposablesDictionary.Count==0)
            {
                return;
            }

            if (exoskeleton)
            {
                RemoveFromDictionaries(TimerType.TimerExsosceleton);
                return;
            }
               
            RemoveFromDictionaries(TimerType.TimerVRWorld);
        }

        public void DespawnAllTimers()
        {
            foreach (var disposable in _timeDisposablesDictionary)
            {
                disposable.Value.Dispose();
            }
            foreach (var timer in _timerModelDictionary)
            {
                _timerPool.Despawn(timer.Value);
            }
        }

        public void ClearDictionaries()
        {
            _timerModelDictionary.Clear();
            _timeDisposablesDictionary.Clear();
            _timeDisposablesDictionary.Clear();
        }

        private void TimerIsOverLogic(TimerType type)
        {
              RemoveFromDictionaries(type);
            
            switch (type)
            {
                case TimerType.TimerVRWorld :
                    VRTimerIsOver?.Invoke();
                    break;
                
                case TimerType.TimerExsosceleton :
                    ExoskeletonTimerIsOver?.Invoke();
                    break;
                
                default:
                    Debug.Log("Timer Is Missing");
                    break;
            }
        }

        private void RemoveFromDictionaries(TimerType type)
        {
            _timerPool.Despawn(_timerModelDictionary[type]);
            _timerModelDictionary.Remove(type);
            _timeDisposablesDictionary[type].Dispose();
            _timeDisposablesDictionary.Remove(type);
            KillAnimation(type);

            if (type == TimerType.TimerVRWorld && _timerModelDictionary.Count == 1)
            {
                RemoveFromDictionaries(TimerType.TimerExsosceleton);
                ExoskeletonTimerIsOver?.Invoke();
            }
        }

        private void KillAnimation(TimerType type)
        {
            _timerAnimationDictionary[type].Kill();
            _timerAnimationDictionary.Remove(type);
        }

        private void ChangeTimerFillAmount(TimerType type)
        {
            if (_timerAnimationDictionary.ContainsKey(type))
            {
                KillAnimation(type);
            }

            var timePerCell = _timerModelDictionary[type].TimerTime / _timerModelDictionary[type].Cells.Length;
            
            Blinking(type,_timerModelDictionary[type].Cells.Length, timePerCell);
        }

        private void Blinking(TimerType type, int i, float delay)
        {
            var currentDelay = delay;
            int currentCell = i-1;
            TimerType currentType = type;
            
            Tween cellDelaytween = DOVirtual.DelayedCall(currentDelay, () =>
            {
                KillAnimation(currentType);
                if (currentCell >= 0)
                {
                    _timerModelDictionary[currentType].Cells[currentCell].DOFade(0, 0);
                    Blinking(currentType, currentCell--, currentDelay);
                }
            });
            _timerAnimationDictionary.Add(currentType, cellDelaytween);
        }
    }
}