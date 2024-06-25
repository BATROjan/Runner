using System;
using DG.Tweening;
using Kolya_sGame.Camera;
using Kolya_sGame.Style;
using Kolya_sGame.World;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using VGUIService;

namespace Kolya_sGame.UI.Realization.UIStartWindow
{
    public class UIStartWindowController
    {
        private readonly CameraController _cameraController;
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;
        private readonly IWorldController _worldController;
        private readonly UIBlackoutWindowController _uiBlackoutWindowController;
        private readonly SaveDataSystem _saveDataSystem;
        private readonly ChangeStyleController _changeStyleController;
        private readonly AudioController _audioController;
        private readonly SystemScore _systemScore;
        private global::UIStartWindow _startWindow;

        private const string UrlBoosty = "https://boosty.to/vrgorehab";

        private bool _isMute;
        
        public UIStartWindowController(
            CameraController cameraController,
            GameController.GameController gameController,
            IUIService uiService,
            IWorldController worldController,
            ISystemScore systemScore,
            UIBlackoutWindowController uiBlackoutWindowController,
            IAudioController audioController,
            SaveDataSystem saveDataSystem,
            ChangeStyleController changeStyleController)
        {
            _cameraController = cameraController;
            _gameController = gameController;
            _uiService = uiService;
            _worldController = worldController;
            _uiBlackoutWindowController = uiBlackoutWindowController;
            _saveDataSystem = saveDataSystem;
            _changeStyleController = changeStyleController;
            _audioController = (AudioController) audioController;
            _systemScore = (SystemScore)systemScore;
            _startWindow = _uiService.Get<global::UIStartWindow>();
            _startWindow.ShowEvent += InitButton;
            _startWindow.HideEvent +=  UnSubscribeButtons;
            _startWindow.OnSubscribeButton += SubscribeButton;
            _startWindow.OnUnsubscribeButton += UnSubscribeButton;
        }

        private void SubscribeButton()
        {
            _startWindow.PlayButton.OnClickButton += OnStartButtonClickEvent;
            _startWindow.ExitButton.OnClickButton += ShowExitGameWindow;
            _startWindow.SettingsButton.OnClickButton += ShowSettingsWindow;
            _startWindow.VolumeButton.OnClickButton += SetVolume;
            _startWindow.BoostyButton.OnClickButton += OpenBoosty;
            _startWindow.ChangeStyleButton.OnClickButton += ChangeStyle;
        }
        
        private void UnSubscribeButton()
        {
            _startWindow.PlayButton.OnClickButton -= OnStartButtonClickEvent;
            _startWindow.ExitButton.OnClickButton -= ShowExitGameWindow;
            _startWindow.SettingsButton.OnClickButton -= ShowSettingsWindow;
            _startWindow.VolumeButton.OnClickButton -= SetVolume;
            _startWindow.BoostyButton.OnClickButton -= OpenBoosty;
            _startWindow.ChangeStyleButton.OnClickButton -= ChangeStyle;
        }
        
        private void InitButton(object sender, EventArgs e)
        {
            if (_saveDataSystem.LoadStateStyleWorld() == 0)
            {
                _cameraController.SetStartPosition(WorldName.Real);
            }
            else
            {
                _cameraController.SetStartPosition(WorldName.Cartoon);
            }
            _startWindow.TmpBestScore.text = _systemScore.GetBestScore().ToString();
            DOVirtual.DelayedCall(0.3f, () => _startWindow.PlayButton.OnClickButton += OnStartButtonClickEvent);
            _startWindow.ExitButton.OnClickButton += ShowExitGameWindow;
            _startWindow.SettingsButton.OnClickButton += ShowSettingsWindow;
            _startWindow.VolumeButton.OnClickButton += SetVolume;
            _startWindow.BoostyButton.OnClickButton += OpenBoosty;
            _startWindow.ChangeStyleButton.OnClickButton += ChangeStyle;
            
            _gameController.SpawnPlayer();

            CheckAudioSave();
        }
        
        private void UnSubscribeButtons(object sender, EventArgs e)
        { 
            _startWindow.PlayButton.OnClickButton -= OnStartButtonClickEvent;
            _startWindow.ExitButton.OnClickButton -= ShowExitGameWindow;
            _startWindow.SettingsButton.OnClickButton -= ShowSettingsWindow;
            _startWindow.VolumeButton.OnClickButton -= SetVolume;
            _startWindow.BoostyButton.OnClickButton -= OpenBoosty;
            _startWindow.ChangeStyleButton.OnClickButton -= ChangeStyle;
        }
        
        private void OnStartButtonClickEvent()
        {
            _uiBlackoutWindowController.FadeOut(0);
            _uiService.Hide<global::UIStartWindow>();
            _uiService.Show<UIPlayingSceneWindow>();
            _gameController.StartGame();
            _worldController.StartMove();
            _systemScore.StartScore();
            _cameraController.SetGamePosition();
        }
        
        private void ShowExitGameWindow()
        {
            _uiService.Hide<global::UIStartWindow>();
            _uiService.Show<UIExitWindow>();
        }
        
        private void ShowSettingsWindow()
        {
            _uiService.Hide<global::UIStartWindow>();
            _uiService.Show<UISettingsWindow>();
        }
        
        private void OpenBoosty()
        {
            UnityEngine.Application.OpenURL(UrlBoosty);
        }
        
        private void SetVolume()
        {
            if (!_isMute)
            {
                _audioController.MuteAll();
                _startWindow.VolumeButton.GetComponent<Image>().sprite = _startWindow.StateMuteButton[1];
                _saveDataSystem.SaveStateSound(0);
            }
            else
            {
                _audioController.UnMuteAll();
                _startWindow.VolumeButton.GetComponent<Image>().sprite = _startWindow.StateMuteButton[0];
                _saveDataSystem.SaveStateSound(1);
            }

            _isMute = !_isMute;
        }

        private void ChangeStyle()
        {
            _changeStyleController.ChangeStyle();
        }

        private void CheckAudioSave()
        {
            if (_saveDataSystem.LoadStateSound() == 1)
            {
                _startWindow.VolumeButton.GetComponent<Image>().sprite = _startWindow.StateMuteButton[0];
                _isMute = false;
            }
            else
            {
                _startWindow.VolumeButton.GetComponent<Image>().sprite = _startWindow.StateMuteButton[1];
                _isMute = true;
            }
        }
    }
}