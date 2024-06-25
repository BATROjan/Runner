
using System.Collections.Generic;
using DG.Tweening;

public class AudioController : IAudioController
{
    private readonly AudioModelConfig _audioModelConfig;
    private readonly AudioView.Pool _audioPool;
    private readonly SaveDataSystem _saveDataSystem;

    private List<AudioView> _listAudioView = new List<AudioView>();
    private bool _isMute;
    public AudioController(
        AudioModelConfig audioModelConfig,
        AudioView.Pool audioPool,
        SaveDataSystem saveDataSystem)
    {
        _audioModelConfig = audioModelConfig;
        _audioPool = audioPool;
        _saveDataSystem = saveDataSystem;
    }
    
    public void Play(AudioType nameSound, bool isLoop = false)
    {
        if (_isMute)
        {
            return;
        }
        var audio = _audioPool.Spawn(new AudioProtocol(_audioModelConfig.GetAudioModel(nameSound)));
        
        audio.AudioSource.Play();
        
        _listAudioView.Add(audio);

        if (!isLoop)
        {
            DOVirtual.DelayedCall(audio.AudioSource.clip.length, () =>
            {
                audio.AudioSource.clip = null;
                _audioPool.Despawn(audio);
            });
        }
    }

    public void StopAll()
    {
        foreach (var audioView in _listAudioView)
        {
            audioView.AudioSource.clip = null;
            _audioPool.Despawn(audioView);
        }
        _listAudioView.Clear();
    }

    public void MuteAll()
    {
        foreach (var audioView in _listAudioView)
        {
            audioView.AudioSource.mute = true;
        }

        _isMute = true;
    }
    
    public void UnMuteAll()
    {
        foreach (var audioView in _listAudioView)
        {
            audioView.AudioSource.mute = false;
        }

        _isMute = false;
    }

    public void CheckAudioSave()
    {
        if (_saveDataSystem.LoadStateSound() == 1)
        {
            UnMuteAll();
        }
        else
        {
            MuteAll();
        }
    }
}