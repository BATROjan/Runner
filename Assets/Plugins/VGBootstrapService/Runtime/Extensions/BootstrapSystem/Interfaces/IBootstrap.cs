using System;
using VGCore;

namespace VGBootstrapService
{
    public interface IBootstrap
    {
        /// <summary>
        /// 0...1
        /// Where 1 is completed 
        /// </summary>
        event EventHandler<float> ProgressUpdate;

        void StartExecute();
        void PauseExecution();
        void ResumeExecution();
        void AddCommand(ICommand cmd);
    }
}