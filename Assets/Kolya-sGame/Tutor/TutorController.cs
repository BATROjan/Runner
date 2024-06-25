using DG.Tweening;
using Kolya_sGame.GameController;
using Kolya_sGame.Interaction;
using VGUIService;

public class TutorController
{
    private readonly BaseInputController _baseInputController;
    private readonly IUIService _uiService;
    private readonly GameController _gameController;
    private readonly UITutorialWindowController _uiTutorialWindowController;
    private readonly TutorTextConfig _tutorTextConfig;
    private readonly UIPlayingSceneWindowController _uiPlayingSceneWindowController;
    private readonly SaveDataSystem _saveDataSystem;
    private readonly UIPlayingSceneWindow _uiPlayingSceneWindow;


    private int _scoreText;

    public TutorController(
        IUIService uiService,
        IInputController baseInputController,
        GameController gameController,
        UITutorialWindowController uiTutorialWindowController,
        TutorTextConfig tutorTextConfig,
        UIPlayingSceneWindowController uiPlayingSceneWindowController,
        SaveDataSystem saveDataSystem)
    {
        _baseInputController = (BaseInputController)baseInputController;
        _uiService = uiService;
        _gameController = gameController;
        _uiTutorialWindowController = uiTutorialWindowController;
        _tutorTextConfig = tutorTextConfig;
        _uiPlayingSceneWindowController = uiPlayingSceneWindowController;
        _saveDataSystem = saveDataSystem;
    }

    public void StartTutor()
    {
        _uiService.Show<UITutorialWindow>();
        _uiService.Show<UIPlayingSceneWindow>();
        _uiPlayingSceneWindowController.HideButtonPause();
        
        _gameController.StartGameForTutor();

        DOVirtual.DelayedCall(_tutorTextConfig.TutorTextModels[_scoreText].Duration + _tutorTextConfig.TutorTextModels[_scoreText + 1].Delay,
            () => _baseInputController.StartInteraction());
        SetText(
            _tutorTextConfig.TutorTextModels[_scoreText].Text, 
            _tutorTextConfig.TutorTextModels[_scoreText].Duration,
            _tutorTextConfig.TutorTextModels[_scoreText].Delay);
    }

    private void SetText(string text, float duration, float delay)
    {
        DOVirtual.DelayedCall(delay, () =>
        {
            _uiTutorialWindowController.SetText(text, duration).OnComplete(() =>
            {
                _scoreText++;
                if (_scoreText != _tutorTextConfig.TutorTextModels.Length)
                {
                    SetText(
                        _tutorTextConfig.TutorTextModels[_scoreText].Text, 
                        _tutorTextConfig.TutorTextModels[_scoreText].Duration,
                        _tutorTextConfig.TutorTextModels[_scoreText].Delay);
                }
                else
                {
                    DOVirtual.DelayedCall(_tutorTextConfig.TutorTextModels[_scoreText - 1].Delay, () =>
                    {
                        _uiService.Hide<UITutorialWindow>();
                        _saveDataSystem.SaveStateTutor(1);
                        _uiPlayingSceneWindowController.ShowButtonPause();
                    });
                }
            });
        });
    }
}
