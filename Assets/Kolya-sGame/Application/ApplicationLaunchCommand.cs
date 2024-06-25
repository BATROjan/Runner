using System;
using DG.Tweening;
using HabilectMoveOut.Application.Consts;
using Kolya_sGame.Ground;
using Kolya_sGame.World;
using VGCore;
using VGUIService;
using Zenject;
using Command = VGCore.Command;

namespace HabilectMoveOut.Application
{
    public class ApplicationLaunchCommand : Command
    {
        private readonly GroundController _groundController;
        private readonly IUIService _uiService;
        private readonly IWorldController _worldController;
        private readonly SaveDataSystem _saveDataSystem;
        private readonly UIBlackoutWindowController _blackoutWindowController;
        private readonly TutorController _tutorController;
        private readonly UISettingsWindowController _uiSettingsWindowController;
        private readonly AudioController _audioController;

        public ApplicationLaunchCommand(
            GroundController groundController,
            CommandStorage commandStorage,
            IUIService uiService,
            IWorldController worldController,
            IAudioController audioController,
            SaveDataSystem saveDataSystem,
            UIBlackoutWindowController blackoutWindowController,
            TutorController tutorController,
            UISettingsWindowController uiSettingsWindowController) 
            : base(commandStorage)
        {
            _groundController = groundController;
            _uiService = uiService;
            _worldController = worldController;
            _audioController = (AudioController)audioController;
            _saveDataSystem = saveDataSystem;
            _blackoutWindowController = blackoutWindowController;
            _tutorController = tutorController;
            _uiSettingsWindowController = uiSettingsWindowController;
        }

        public override CommandResult Execute()
        {
            //TODO стартовая команда
            _uiService.Show<UIBlackoutWindow>(UIServiceLayerConst.UIWindowLayer);
            _blackoutWindowController.FadeOut(1f);
            
            if (_saveDataSystem.LoadStateTutor())
            {
                _uiService.Show<UIStartWindow>(UIServiceLayerConst.UIWindowLayer);
            }
            else
            {
                _tutorController.StartTutor();
            }
            
            _audioController.Play(AudioType.Background, true);
            
            _uiSettingsWindowController.CheckLanguage();
            _audioController.CheckAudioSave();
            if (_saveDataSystem.LoadStateStyleWorld() == 0)
            {
                _worldController.StartSpawn(WorldName.Real);
                _groundController.Spawn(WorldName.Real);
            }
            else
            {
                _worldController.StartSpawn(WorldName.Cartoon);
                _groundController.Spawn(WorldName.Cartoon);
            }
            
            Done?.Invoke(this,EventArgs.Empty);
            
            return base.Execute();
        }
    }
}