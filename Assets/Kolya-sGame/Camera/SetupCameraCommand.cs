using System;
using Kolya_sGame.Application.Consts;
using Kolya_sGame.SceneObjectStorage;
using VGCore;

namespace Kolya_sGame.Camera
{
    public class SetupCameraCommand : Command
    {
        private readonly ISceneObjectsStorage _sceneObjectsStorage;

        public SetupCameraCommand(
            ISceneObjectsStorage sceneObjectsStorage,
            CommandStorage commandStorage) 
            : base(commandStorage)
        {
            _sceneObjectsStorage = sceneObjectsStorage;
        }

        public override CommandResult Execute()
        {
            _sceneObjectsStorage.CreateFromResourcesAndAdd<CameraView>(ResourcesConsts.CameraViewSource);

            Done?.Invoke(this,EventArgs.Empty);
            
            return base.Execute();
        }
    }
}