﻿using System;
using UniRx;
using VGCore;

namespace VGBootstrapService
{
    public class Bootstrap : ProcessingCommand, IBootstrap
    {
        public event EventHandler<float> ProgressUpdate;

        private ICommand _currentCommand;
        private int _executedCommandsCount;
        private bool _canExecute = true;
        
        public override void StartExecute()
        {
            _executedCommandsCount = Count;
            UpdateProgress(0);
            
            // run command execution
            Execute();
        }

        public void PauseExecution()
        {
            if (!IsExecuting || _currentCommand == null)
            {
                return;
            }
            
            Stop();
        }

        public void ResumeExecution()
        {
            if (IsExecuting || _currentCommand == null)
            {
                return;
            }
            
            _canExecute = true;
            IsExecuting = true;
            
            _currentCommand.Done += CurrentCommandOnDone;
            _currentCommand.Execute();
        }

        public void Stop()
        {
            _canExecute = false;
            IsExecuting = false;

            if (_currentCommand != null)
            {
                _currentCommand.Done -= CurrentCommandOnDone;
                _currentCommand.Cancel();
            }
        }
        
        protected override void Execute()
        {
            if (!_canExecute)
            {
                return;
            }
            
            IsExecuting = true;
            _canExecute = false;
            
            _currentCommand = Dequeue();
            if (_currentCommand == null)
            {
                IsExecuting = false;
                _canExecute = true;
                OnComplete();
            }
            else
            {
                _currentCommand.Done += CurrentCommandOnDone;
                _currentCommand.Execute();
            }
        }

        private void CurrentCommandOnDone(object sender, EventArgs e)
        {
            _currentCommand.Done -= CurrentCommandOnDone;
            UpdateProgress(_executedCommandsCount == 0 ? 1 : (1 - (float)Count / _executedCommandsCount));
            _canExecute = true;
            
            // start next command on next frame
            Observable.NextFrame().Subscribe(_ =>
            {
                Execute();
            });
        }

        private void UpdateProgress(float value)
        {
            ProgressUpdate?.Invoke(this, value);
        }
    }
}