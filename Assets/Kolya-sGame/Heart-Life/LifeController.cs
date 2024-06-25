using System;
using Kolya_sGame.Timer;
using ModestTree.Util;
using UnityEngine;
using VGUIService;

namespace Kolya_sGame.Heart_Life
{
    public class LifeController
    {
        public Action IsDead;

        private readonly TimerController _timerController;
        private readonly LifeConfig _lifeConfig;

        private int currentLifeCount;
        
        public LifeController(
            TimerController timerController,
            LifeConfig lifeConfig)
        {
            _timerController = timerController;
            _lifeConfig = lifeConfig;
        }

        public void SetDefaulLifeCount()
        {
            currentLifeCount = _lifeConfig.GetLifeCount();
            _timerController.VRTimerIsOver += MinusLife;
            _timerController.ExoskeletonTimerIsOver += MinusLife;
        }

        public void UnSubscribeAction()
        {
            _timerController.VRTimerIsOver -= MinusLife;
            _timerController.ExoskeletonTimerIsOver -= MinusLife;
        }

        public void PlusLife()
        {
            currentLifeCount += 1;
        }     
        
        public void MinusLife()
        {
            currentLifeCount -= 1;
        }

        public int GetCurrentLifeCount()
        {
            return currentLifeCount;
        }

        public bool CheckLifeCount()
        {
            bool isDead;
            if (currentLifeCount <= 0)
            {
                isDead = true;
                UnSubscribeAction();
            }
            else
            {
                isDead = false; 
            }
            return isDead;
        }
    }
}