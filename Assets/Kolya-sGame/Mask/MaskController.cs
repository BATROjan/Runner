using DG.Tweening;
using Kolya_sGame.Camera;
using Kolya_sGame.SceneObjectStorage;
using UnityEngine;

public class MaskController
{
    private readonly MaskRatioConfig _maskRatioConfig;
    private readonly ISceneObjectsStorage _sceneObjectsStorage;
    private readonly MaskView.Pool _maskPool;

    private CameraView _cameraView;
    private MaskView _currentMaskView;

    private Tween _tweenForDelay;
    private Tween _tweenForMoveMask;
    private Tween _tweenForRatio;

    public MaskController(
        MaskView.Pool maskPool,
        MaskRatioConfig maskRatioConfig,
        ISceneObjectsStorage sceneObjectsStorage)
    {
        _maskPool = maskPool;
        _maskRatioConfig = maskRatioConfig;
        _sceneObjectsStorage = sceneObjectsStorage;
    }

    public void Spawn()
    {
        if (_currentMaskView)
        {
            return;
        }

        _currentMaskView = _maskPool.Spawn(
            new MaskProtocol(new Vector3(30f, 0f, 0f)));
        _cameraView = _sceneObjectsStorage.Get<CameraView>();
    }

    public void ChangeToReality()
    {
        _cameraView.AnalogGlitch.enabled = true;
        _tweenForRatio = DOTween.To(ChangeRatioGlitch, 0f, 1f, _maskRatioConfig.DurationForChangeVrToReality).SetEase(Ease.Linear).OnComplete(() =>
        {
            _cameraView.AnalogGlitch.enabled = false;
        });
        _tweenForDelay = DOVirtual.DelayedCall(_maskRatioConfig.DurationForChangeVrToReality / 2, () =>
        {
            _tweenForMoveMask = _currentMaskView.transform.DOMoveX(30f, 0);
        });
    }

    public void ChangeToVr()
    {
        _cameraView.AnalogGlitch.enabled = true;
        _tweenForRatio = DOTween.To(ChangeRatioGlitch, 1f, 0f, _maskRatioConfig.DurationForChangeRealityToVr).SetEase(Ease.Linear);
        _tweenForDelay = DOVirtual.DelayedCall(_maskRatioConfig.DurationForChangeVrToReality / 2, () =>
        {
            _cameraView.AnalogGlitch.enabled = false;
            _tweenForMoveMask = _currentMaskView.transform.DOMoveX(0f, 0);
        });
    }

    public void ChangeToRealityWithoutGlitch()
    {
        KillAllTween();
        _cameraView.AnalogGlitch.enabled = false;
        _currentMaskView.transform.DOMoveX(30f, 0);
    }
    
    private void ChangeRatioGlitch(float value)
    {
        _cameraView.AnalogGlitch.scanLineJitter = 0.2f;
        _cameraView.AnalogGlitch.colorDrift = value;
    }

    private void KillAllTween()
    {
        _tweenForRatio.Kill();
        _tweenForRatio = null;
        
        _tweenForDelay.Kill();
        _tweenForDelay = null;
        
        _tweenForMoveMask.Kill();
        _tweenForMoveMask = null;
        
    }
}
