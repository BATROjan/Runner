using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Kolya_sGame.Timer
{
    public class TimerView: MonoBehaviour
    {
        public Image Icon => icon;
        public Image BackGround => background;
        public float TimerTime => _time;
        public TimerType TimerType => _type;
        public Image[] Cells => cells;
        
        [SerializeField] private Image icon;
        [SerializeField] private Image background;
        [SerializeField] private Image[] cells;

        private float _time;
        private TimerType _type;
        private void ReInit(TimerModel timerModel)
        {
            icon.sprite = timerModel.Icon;
            _time = timerModel.Seconds;
            _type = timerModel.TimerType;
        }
        
        public class Pool : MonoMemoryPool<TimerModel,TimerView>
        {
            protected override void Reinitialize(TimerModel timerModel, TimerView item)
            {
                item.ReInit(timerModel);
            }
        }
    }
}