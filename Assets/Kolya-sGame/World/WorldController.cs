using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Kolya_sGame.Buff;
using Kolya_sGame.Camera;
using Kolya_sGame.Obstacle;
using Kolya_sGame.Player;
using Kolya_sGame.SceneObjectStorage;
using Kolya_sGame.Timer;
using UnityEngine;
using UnityEngine.Tilemaps;
using VGUIService;
using Zenject;

namespace Kolya_sGame.World
{
    public class WorldController : IWorldController, ITickable
    {
        public bool GameIsStop => _gameIsStop;

        private readonly TimerController _timerController;
        private readonly ObstacleTileController _obstacleTileController;
        private readonly WorldConfig _worldConfig;
        private readonly WorldView.Pool _worldViewPool;
        private readonly TickableManager _tickableManager;
        private readonly PlayerController _playerController;
        private readonly MaskController _maskController;
        private readonly ParametersConfig _parametersConfig;
        private readonly IAudioController _audioController;

        private Dictionary<int, Queue<WorldView>> _dictQueueWorldView = new Dictionary<int, Queue<WorldView>>();

        private Dictionary<LayerWorld, Dictionary<WorldType, List<Sprite>>> _dictTypeWorldSprite =
            new Dictionary<LayerWorld, Dictionary<WorldType, List<Sprite>>>();

        private Dictionary<LayerWorld, List<WorldView>> _dictListWorldView =
            new Dictionary<LayerWorld, List<WorldView>>();

        private Tween _tween;
        private bool _gameIsStop = true;
        private bool _isChangeWorld;
        private bool isVRWorld;
        private bool isExoskeleton;
        private float _ratioSpeed = 1f;
        private float VrWolrdTimer;
        private float exoskeletonTimer;

        private WorldModel _currenWorldModel;
        
        public WorldController(
            TimerController timerController,
            ObstacleTileController obstacleTileController,
            WorldConfig worldConfig,
            WorldView.Pool worldViewPool,
            TickableManager tickableManager,
            PlayerController playerController,
            MaskController maskController,
            ParametersConfig parametersConfig,
            IAudioController audioController)
        {
            _timerController = timerController;
            _obstacleTileController = obstacleTileController;
            _worldConfig = worldConfig;
            _worldViewPool = worldViewPool;
            _tickableManager = tickableManager;
            _playerController = playerController;
            _maskController = maskController;
            _parametersConfig = parametersConfig;
            _audioController = audioController;
            
            _tickableManager.Add(this);
        }

        public void StartSpawn(WorldName name)
        {
            Init(name);
            SpawnLayerWorld();
            _maskController.Spawn();
        }
        
        public void StartMove()
        {
            _gameIsStop = false;

            _timerController.VRTimerIsOver += ChangeWorld;
            _playerController.ReturnToReality += ChangeWorld;
            _playerController.GetPlayerView().OnTakeVRGlass += ChangeWorld;
            _playerController.OnEndGame += RevertWorld;
            _tween.Kill();
            _tween = null;
            _tween = DOTween.To(RiseSpeed, _parametersConfig.StartRatioSpeed, _parametersConfig.EndRatioSpeed,
                _parametersConfig.DurationRiseSpeed).SetEase(Ease.Linear);
        }

        public void StopMove()
        {
            if (_playerController.GetPlayerLifeCount() > 0)
            {
                _gameIsStop = true;
            }
            
            _playerController.ReturnToReality -= ChangeWorld;
            _playerController.OnEndGame -= RevertWorld;
            _timerController.VRTimerIsOver -= ChangeWorld;
            _playerController.GetPlayerView().OnTakeVRGlass -= ChangeWorld;
        }

        public void ReturnRealityWorld()
        {
            _maskController.ChangeToRealityWithoutGlitch();
            _isChangeWorld = false;
        }

        public void ChangeWorld()
        {
            if (!_isChangeWorld)
            {
                _maskController.ChangeToVr();
                _obstacleTileController.ChangePolygonCollider();

                isVRWorld = true;
            }
            else
            {
                _maskController.ChangeToReality();

                _obstacleTileController.ChangePolygonCollider();
                _obstacleTileController.RemoveAllBuffWithName(BuffName.Exosceleton);
                SetTimerToNull();
            }

            _audioController.Play(AudioType.ChangeWord);
            _playerController.ChangePLayerSprite(_isChangeWorld);

            _isChangeWorld = !_isChangeWorld;
            _obstacleTileController.NeedExoskelet = _isChangeWorld;
        }

        public void DespawnAll()
        {
            foreach (var listWorldView in _dictListWorldView.Values)
            {
                foreach (var worldView in listWorldView)
                {
                    worldView.gameObject.transform.position = Vector3.zero;
                    worldView.OnBecameInvisibleEvent -= BecameInvisible;
                    _worldViewPool.Despawn(worldView);
                }
            }
            
            _dictListWorldView.Clear();
            _dictTypeWorldSprite.Clear();
            _dictQueueWorldView.Clear();
        }

        public void Tick()
        {
            if (_gameIsStop)
            {
                return;
            }

            foreach (var tile in _obstacleTileController.ObstacleTileViews)
            {
                if (tile)
                {
                    tile.gameObject.transform.position -=
                        new Vector3(_dictListWorldView.Values.ToList()[0][0].Speed * Time.deltaTime * _ratioSpeed, 0f,
                            0f);
                }
            }

            foreach (var listView in _dictListWorldView.Values)
            {
                foreach (var view in listView)
                {
                    view.gameObject.transform.position -=
                        new Vector3(view.Speed * Time.deltaTime * _ratioSpeed, 0f, 0f);
                }
            }
        }

        private void RevertWorld()
        {
            _ratioSpeed *= -1;
            _tween = DOTween.To(DeclineWorld, _ratioSpeed, 0,
                2).OnComplete(() =>  _gameIsStop = true);
            _playerController.RevertWheelAnimation();
        }
        
        private void DeclineWorld(float value)
        {
            _ratioSpeed = value;
        }

        private void SetTimerToNull()
        {
            _obstacleTileController.NeedVRGlasses = true;
            isVRWorld = false;
        }

        private void Init( WorldName name)
        {
            if (_currenWorldModel.Name != name || _currenWorldModel.LayersWorld == null)
            {
                _currenWorldModel = _worldConfig.GetModelByName(name);
            }
            foreach (var layer in _currenWorldModel.LayersWorld)
            {
                var dictTypeListView = new Dictionary<WorldType, List<Sprite>>();
                var listView = new List<Sprite>();
                foreach (var spriteReality in layer.SpritesForReality)
                {
                    listView.Add(spriteReality);
                }
                dictTypeListView.Add(WorldType.Reality, listView);
                listView = new List<Sprite>();;
                
                foreach (var spriteVR in layer.SpritesForVR)
                {
                    listView.Add(spriteVR);
                }
                dictTypeListView.Add(WorldType.VR, listView);
                
                _dictTypeWorldSprite.Add(layer, dictTypeListView);
            }
        }
        private void SpawnLayerWorld()
        {
            foreach (var keyValue in _dictTypeWorldSprite)
            {
                var listWorldView = new List<WorldView>();
                var queueWorldView = new Queue<WorldView>();
                for (int i = 0; i < keyValue.Value.Values.Count; ++i)
                {
                    var tile = _worldViewPool.Spawn(new WorldTileProtocol(
                        keyValue.Value[WorldType.Reality][i],
                        keyValue.Value[WorldType.VR][i],
                        keyValue.Key.NumberLayer,
                        keyValue.Key.SpeedMove,
                        keyValue.Key.SortingOrderReality,
                        keyValue.Key.SortingOrderVr,
                        keyValue.Key.BannerModels));

                    tile.transform.position += new Vector3(0, 1f, keyValue.Key.NumberLayer * 10);
                    listWorldView.Add(tile);
                    queueWorldView.Enqueue(tile);
                }
                
                _dictQueueWorldView.Add(keyValue.Key.NumberLayer, queueWorldView);
                _dictListWorldView.Add(keyValue.Key, listWorldView);
            }
            
            foreach (var listView in _dictListWorldView.Values)
            {
                float offsetView = 0;
                WorldView lastView = null;
                foreach (var view in listView)
                {
                    if (lastView != null)
                    {
                        offsetView += lastView.RightBoardSprite + view.RightBoardSprite;
                        
                    }
                    view.transform.position += new Vector3(offsetView, 0, 0);
                    view.OnBecameInvisibleEvent += BecameInvisible;
                    lastView = view;
                }
            }
        }
        
        private void BecameInvisible(int layer, WorldView worldView)
        {
            worldView.gameObject.SetActive(false);

            _dictQueueWorldView[layer].Dequeue();
            var list = _dictQueueWorldView[layer].ToList();

            var offset = list[^1].RightBoardSprite + worldView.RightBoardSprite;

            worldView.transform.position = list[^1].transform.position + new Vector3(offset, 0f, 0f);

            _dictQueueWorldView[layer].Enqueue(worldView);
            
            DOVirtual.DelayedCall(0.1f, () => worldView.gameObject.SetActive(true));
        }
        
        private void RiseSpeed(float value)
        {
            _ratioSpeed = value;
        }
    }
    
    public enum WorldType
    {
        Reality,
        VR
    }
}