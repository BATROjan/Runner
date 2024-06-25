using System;
using Kolya_sGame.Camera;
using Kolya_sGame.SceneObjectStorage;
using VGCore;
using VGUIService;
using Command = VGCore.Command;

namespace HabilectMoveOut.UI.InitCommands
{
    public class SetupUIRootCommand : Command
    {
        private readonly ISceneObjectsStorage _sceneObjectsStorage;
        private readonly IUIRoot _uiRoot;

        public SetupUIRootCommand(
            ISceneObjectsStorage sceneObjectsStorage,
            IUIRoot uiRoot,
            CommandStorage commandStorage) 
            : base(commandStorage)
        {
            _sceneObjectsStorage = sceneObjectsStorage;
            _uiRoot = uiRoot;
        }

        public override CommandResult Execute()
        {
            _sceneObjectsStorage.Add<Kolya_sGame.UI.UIRoot>((Kolya_sGame.UI.UIRoot)_uiRoot);

            var camera = _sceneObjectsStorage.Get<CameraView>().Camera;
            _uiRoot.Camera = camera;
            _uiRoot.Canvas.worldCamera = camera;
            _uiRoot.Canvas.planeDistance = 1;
            
            Done?.Invoke(this, EventArgs.Empty);
            
            return base.Execute();
        }
    }
}