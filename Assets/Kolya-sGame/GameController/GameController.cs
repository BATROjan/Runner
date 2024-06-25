using DG.Tweening;
using Kolya_sGame.Interaction;
using Kolya_sGame.Obstacle;
using Kolya_sGame.Player;
using Kolya_sGame.Timer;
using Kolya_sGame.World;
using UnityEngine;
using VGUIService;

namespace Kolya_sGame.GameController
{
    public class GameController
    {
        private readonly BaseInputController _baseInputController;
        private readonly TimerController _timerController;
        private readonly ObstacleTileController _obstacleTileController;
        private readonly PlayerController _playerController;
        private readonly WorldController _worldController;
        private readonly IUIService _uiService;
        private readonly UIPlayingSceneWindowController _uiPlayingSceneWindowController;
        private readonly UIPauseWindowController _uiPauseWindowController;
        private readonly UIRestartWindowController _uiRestartWindowController;
        private readonly UIReturnWindowController _uiReturnWindowController;
        private readonly SaveDataSystem _saveDataSystem;
        private readonly ISystemScore _systemScore;
        private readonly StarController _starController;

        private bool _isPause;

        public GameController(
            TimerController timerController,
            ObstacleTileController obstacleTileController, 
            IInputController baseInputController,
            PlayerController playerController,
            IWorldController worldController,
            IUIService uiService,
            ISystemScore systemScore,
            StarController starController,
            UIPlayingSceneWindowController uiPlayingSceneWindowController,
            UIPauseWindowController uiPauseWindowController,
            UIRestartWindowController uiRestartWindowController,
            UIReturnWindowController uiReturnWindowController,
            SaveDataSystem saveDataSystem)
        {
            _baseInputController = (BaseInputController)baseInputController;
            _timerController = timerController;
            _obstacleTileController = obstacleTileController;
            _playerController = playerController;
            _worldController = (WorldController)worldController;
            _uiService = uiService;
            _uiPlayingSceneWindowController = uiPlayingSceneWindowController;
            _uiPauseWindowController = uiPauseWindowController;
            _uiRestartWindowController = uiRestartWindowController;
            _uiReturnWindowController = uiReturnWindowController;
            _saveDataSystem = saveDataSystem;
            _systemScore = systemScore;
            _starController = starController;

            _uiPlayingSceneWindowController.OnPauseGame += PauseGame;
            
            _uiPauseWindowController.OnReturnGame += ReturnPauseGame;

            _uiRestartWindowController.OnRestartGame += RestartGame;
            _uiRestartWindowController.OnReturnMenu += ReturnMenu;
            _uiRestartWindowController.OnStopGame += StopGame;

            _uiReturnWindowController.OnReturnMenu += ReturnMenuForPause;
        }

        public void StartGame()
        {
            _isPause = false;
            _playerController.OnEndGame += EndGame;
            _playerController.PlayerAnimationState();
            _baseInputController.StartInteraction();
            _timerController.DespawnAllTimers();
            _timerController.ClearDictionaries();
            _starController.StartStarController();
            if (!_playerController.GetPlayerView())
            {
                _playerController.SpawnPlayer();
            }

            if (_obstacleTileController.ObstacleTileViews.Count <= 0)
            {
                _obstacleTileController.SpawnObstacleTileView(true);
            }
        }

        public void StartGameForTutor()
        {
            _isPause = false;
            _playerController.OnEndGame += EndGame;
            _playerController.SpawnPlayer();
            _playerController.PlayerAnimationState();
            _worldController.StartMove();
            _timerController.DespawnAllTimers();
            _timerController.ClearDictionaries();
            _systemScore.StartScore();
            _starController.StartStarController();
            DOVirtual.DelayedCall(20, () =>
            {
                _obstacleTileController.SpawnObstacleTileView(true);
            });
        }
        
        public void SpawnPlayer()
        {
            if (_playerController.GetPlayerView())
            {
                return;
            }
            _playerController.SpawnPlayer();
            if (_saveDataSystem.LoadStateStyleWorld() == 0)
            {
                _obstacleTileController.SpawnStartTileView(WorldName.Real);
            }
            else
            {
                _obstacleTileController.SpawnStartTileView(WorldName.Cartoon);
            }
           
        }

        private void EndGame()
        {
            _uiService.Hide<UIPlayingSceneWindow>();
            if (!_isPause)
            {
                _uiService.Show<UIRestartWindow>();
            }

            _isPause = true;
        }

        private void StopGame()
        {
            Time.timeScale = 1;
            
            _timerController.DespawnAllTimers();
            _timerController.ClearDictionaries();
            
            _worldController.StopMove();
            _systemScore.StopScore();
            _baseInputController.EndInteraction();
            _playerController.OnEndGame -= EndGame;
            _starController.StopStarController();
        }
        
        private void RestartGame()
        {
            _playerController.DespawnPlayer();
            _obstacleTileController.DespawnAllTile();
            _worldController.ReturnRealityWorld();

            SpawnPlayer();
            StartGame();
            
            _worldController.StartMove();
            _systemScore.StartScore();
            _timerController.ClearDictionaries();
        }

        private void ReturnMenu()
        {
            _isPause = false;
            _obstacleTileController.DespawnAllTile();
            _worldController.ReturnRealityWorld();
            
            _playerController.DespawnPlayer();
            SpawnPlayer();

            _uiService.Show<UIStartWindow>();
        }
        
        private void ReturnMenuForPause()
        {
            _baseInputController.UnSubscribeLeanTouch();
            StopGame();
            ReturnMenu();
        }
        
        private void PauseGame()
        {
            _baseInputController.UnSubscribeLeanTouch();
            Time.timeScale = 0;
        }

        private void ReturnPauseGame()
        {
            _uiPlayingSceneWindowController.ShowButtonPause();
            Time.timeScale = 1;
            _baseInputController.SubscribeLeanTouch();
        }
    }
}