using DG.Tweening;
using Kolya_sGame.Camera;
using Kolya_sGame.Ground;
using Kolya_sGame.Obstacle;
using Kolya_sGame.UI.Realization.UIStartWindow;
using Kolya_sGame.World;
using VGUIService;

namespace Kolya_sGame.Style
{
    public class ChangeStyleController
    {
        private readonly CameraController _cameraController;
        private readonly ObstacleTileController _obstacleTileController;
        private readonly GroundController _groundController;
        private readonly IWorldController _worldController;
        private readonly SaveDataSystem _saveDataSystem;
        private readonly UIBlackoutWindowController _uiBlackoutWindowController;
        private readonly UIStartWindow _uiStartWindow;

        public ChangeStyleController(
            CameraController cameraController,
            ObstacleTileController obstacleTileController,
            GroundController groundController,
            IWorldController worldController,
            SaveDataSystem saveDataSystem,
            UIBlackoutWindowController uiBlackoutWindowController,
            IUIService uiService)
        {
            _cameraController = cameraController;
            _obstacleTileController = obstacleTileController;
            _groundController = groundController;
            _worldController = worldController;
            _saveDataSystem = saveDataSystem;
            _uiBlackoutWindowController = uiBlackoutWindowController;
            _uiStartWindow = uiService.Get<UIStartWindow>();
        }

        public void ChangeStyle()
        {
            _uiStartWindow.UnSubscribeButton();
            _uiBlackoutWindowController.FadeIn(0.5f).OnComplete(() => 
            {
                if (_saveDataSystem.LoadStateStyleWorld() == 0)
                {
                    _worldController.DespawnAll();
                    _worldController.StartSpawn(WorldName.Cartoon);
                    _groundController.Despawn();
                    _groundController.Spawn(WorldName.Cartoon);
                    _obstacleTileController.DespawnAllTile();
                    _obstacleTileController.SpawnStartTileView(WorldName.Cartoon);
                    _cameraController.ChangeCameraPosition(WorldName.Cartoon);
                    _saveDataSystem.SaveStateStyleWorld(1);
                }
                else
                {
                    _worldController.DespawnAll();
                    _worldController.StartSpawn(WorldName.Real);
                    _groundController.Despawn();
                    _groundController.Spawn(WorldName.Real);
                    _obstacleTileController.DespawnAllTile();
                    _obstacleTileController.SpawnStartTileView(WorldName.Real);
                    _cameraController.ChangeCameraPosition(WorldName.Real);
                    _saveDataSystem.SaveStateStyleWorld(0);
                }

                DOVirtual.DelayedCall(0.4f, ()=>
                {
                    _uiBlackoutWindowController.FadeOut(0.5f).OnComplete(() => _uiStartWindow.SubscribeButton());
                });
            });

        }
    }
}