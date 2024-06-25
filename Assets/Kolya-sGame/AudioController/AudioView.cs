using System;
using UnityEngine;
using Zenject;

public class AudioView : MonoBehaviour, IPoolable<AudioProtocol, IMemoryPool>
{
    public AudioSource AudioSource => audioSource;
    [SerializeField] private AudioSource audioSource;
    
    public void OnDespawned()
    {
    }

    public void OnSpawned(AudioProtocol protocol, IMemoryPool item)
    {
        audioSource.clip = protocol.AudioModel.AudioClip;
        audioSource.loop = protocol.AudioModel.Loop;
        audioSource.volume = protocol.AudioModel.Volume;
    }
    
    private void ReInit(AudioProtocol protocol)
    {
        audioSource.clip = protocol.AudioModel.AudioClip;
        audioSource.loop = protocol.AudioModel.Loop;
        audioSource.volume = protocol.AudioModel.Volume;
    }
    
    public class Pool : MonoMemoryPool<AudioProtocol, AudioView>
    {
        protected override void Reinitialize(AudioProtocol protocol, AudioView item)
        {
            item.ReInit(protocol);
            item.OnSpawned(protocol, this);
        }

        protected override void OnDespawned(AudioView item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }
    }
}

[Serializable]
public struct AudioProtocol
{
    public AudioModel AudioModel;
    public AudioProtocol(AudioModel audioModel)
    {
        AudioModel = audioModel; 
    }
}