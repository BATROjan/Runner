using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioModelConfig", menuName = "Configs/AudioModelConfig")]
public class AudioModelConfig : ScriptableObject
{
    [SerializeField] private AudioModel[] audioModels;
    private Dictionary<AudioType, AudioModel> _dictAudioModels = new Dictionary<AudioType, AudioModel>();
    [NonSerialized] private bool _inited;

    public AudioModel GetAudioModel(AudioType type)
    {
        if (!_inited)
        {
            Init();
        }

        if (_dictAudioModels.ContainsKey(type))
        {
            return _dictAudioModels[type];
        }
        
        Debug.LogError($"There no such AudioModel with type: {type} ");
        
        return new AudioModel();
    }
    
    private void Init()
    {
        foreach (var model in audioModels)
        {
            _dictAudioModels.Add(model.Type, model);
        }

        _inited = !_inited;
    }
}

[Serializable]
public struct AudioModel
{
    public AudioType Type;
    public AudioClip AudioClip;
    public bool Loop;
    public float Volume;
}
public enum AudioType
{
    Background,
    Coin,
    ChangeWord
}
