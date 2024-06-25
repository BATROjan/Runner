using System;
using VGCore;

namespace VGBootstrapService
{
    public interface IProcessingCommand
    {
        event EventHandler AllCommandsDone;

        bool IsExecuting { get; }

        void AddCommand(ICommand cmd);
        void StartExecute();
        void Clear();

        bool Any();
    }
}