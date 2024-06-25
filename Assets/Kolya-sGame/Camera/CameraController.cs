using DG.Tweening;
using Kolya_sGame.SceneObjectStorage;
using Kolya_sGame.World;
using UnityEngine;

namespace Kolya_sGame.Camera
{
    public class CameraController
    {
        private readonly ISceneObjectsStorage _sceneObjectsStorage;
        
        private CameraView _cameraView;

        private Sequence _sequence;
        private Vector3 _startCartoonPosition = new Vector3(-2.5f, -1, -10);
        private Vector3 _startRealPosition = new Vector3(-2.5f, -1.7f, -10);
        private Vector3 _gamePosition = new Vector3(0, 1, -10);
        private float _startCameraSize = 3.2f;
        private float _gameCameraSize = 6f;
        private float _animationDuration = 2f;

        public CameraController(ISceneObjectsStorage sceneObjectsStorage)
        {
            _sceneObjectsStorage = sceneObjectsStorage;
        }

        public CameraView GetCameraView()
        {
            return _cameraView;
        }

        public void SetStartPosition(WorldName name)
        {
            _cameraView = _sceneObjectsStorage.Get<CameraView>();
            
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            
            Vector3 position = Vector3.one;
            
            if (name == WorldName.Cartoon)
            {
                position = _startCartoonPosition;
            }
            else
            {
                position = _startRealPosition;
            }
            _sequence.Append(_cameraView.transform.DOMove(position, _animationDuration)).
                Join(_cameraView.Camera.DOOrthoSize(_startCameraSize,_animationDuration));
        }

        public void ChangeCameraPosition(WorldName name)
        {
            if (name == WorldName.Cartoon)
            {
                _cameraView.transform.position = _startCartoonPosition;
            }
            else
            {
                _cameraView.transform.position = _startRealPosition;
            }
        }

        public void SetGamePosition()
        {
            _cameraView = _sceneObjectsStorage.Get<CameraView>();
            
            _sequence.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(_cameraView.transform.DOMove(_gamePosition,_animationDuration)).
                Join(_cameraView.Camera.DOOrthoSize(_gameCameraSize,_animationDuration));
        }
    }
}