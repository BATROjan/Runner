using System;
using HabilectMoveOut.Application;
using HabilectMoveOut.UI.InitCommands;
using Kolya_sGame.Camera;
using VGBootstrapService;
using Zenject;

namespace Kolya_sGame.Application
{
    public class ApplicationLaunchBootstrap
    {
        public ApplicationLaunchBootstrap(
            IInstantiator instantiator)
        {

            var bootstrap = new Bootstrap();
            
            bootstrap.AddCommand(instantiator.Instantiate<SetupCameraCommand>());
            bootstrap.AddCommand(instantiator.Instantiate<SetupUIRootCommand>());
            bootstrap.AddCommand(instantiator.Instantiate<ApplicationLaunchCommand>());
            
            bootstrap.AllCommandsDone += OnAllCommandsDone;
            
            bootstrap.StartExecute();
        }

        private void OnAllCommandsDone(object sender, EventArgs e)
        {
        }
    }
}